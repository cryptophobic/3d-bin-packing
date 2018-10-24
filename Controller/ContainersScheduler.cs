using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimensionalBinTimePacking.DataTypes;
using BinTime.Logger;

namespace ThreeDimensionalBinTimePacking.Controller
{
	/// <summary>
	/// class for calculating all possible sets of containers
	/// </summary>
	public class ContainersScheduler
	{
		/// <summary>
		/// weight restriction for containers set
		/// </summary>
		private Int64 weight = 0;

		/// <summary>
		/// volume restriction for containers set
		/// </summary>
		private Int64 volume = 0;

		/// <summary>
		/// count of containers restriction for containers set
		/// </summary>
		private Dictionary<int, int> containersLimit;

		/// <summary>
		/// all possible containers
		/// </summary>
		private Container[] containers;

		/// <summary>
		/// count of boxes (mmm, looks like one more restriction, maybe ;) )
		/// </summary>
		private int boxesCount = 0;

		/// <summary>
		/// last found max volume of containers (restriction to next searchs of solution)
		/// </summary>
		private Int64 maxContainersVolume = 0;

		/// <summary>
		/// pointer to Logger
		/// </summary>
		private Logger logger = null;

		/// <summary>
		/// array ffor storing possible sollutions
		/// </summary>
		private ContainersSet[] potentialSolutions = null;

		/// <summary>
		/// index of current solution in array
		/// </summary>
		private int currSolution = -1;

		/// <summary>
		/// public constructor
		/// </summary>
		/// <param name="newWeight">weight restriction</param>
		/// <param name="newVolume">volume restriction</param>
		/// <param name="newContainersLimit">container's count restriction</param>
		/// <param name="newContainers">containers set</param>
		/// <param name="newBoxesCount">boxes count</param>
		public ContainersScheduler(Int64 newWeight, Int64 newVolume, Dictionary<int, int> newContainersLimit, Container[] newContainers, int newBoxesCount)
		{
			logger = new Logger();
			weight = newWeight;
			volume = newVolume;
			containersLimit = newContainersLimit;
			containers = newContainers;
			boxesCount = newBoxesCount;
		}

		/// <summary>
		/// sorting solutions in order by cheapness
		/// </summary>
		private void sortSolutions()
		{
			if (potentialSolutions != null)
			{
				for (int i = 0; i + 1 < potentialSolutions.Length; i++)
				{
					for (int j = i; j < potentialSolutions.Length; j++)
					{
						if (potentialSolutions[i].volume > potentialSolutions[j].volume)
						{
							ContainersSet swap = potentialSolutions[i];
							potentialSolutions[i] = potentialSolutions[j];
							potentialSolutions[j] = swap;
						}
					}
				}
			}
		}

		/// <summary>
		/// get next solution
		/// </summary>
		/// <returns>next solution or null (must be impossible condition for returning null according to my tests)</returns>
		public GroupedContainersSet[] getNextSolution()
		{

			if (potentialSolutions == null || currSolution + 1 >= potentialSolutions.Length)
			{
				currSolution = 0;
				Int64 remainingVolume = maxContainersVolume == 0 ? ((Int64)(volume)) : maxContainersVolume + 1;
				maxContainersVolume = remainingVolume;
				potentialSolutions = null;
				getContainersSet(remainingVolume, weight, 0, new Dictionary<int, int>(), 0);
				sortSolutions();

				if (potentialSolutions == null || currSolution >= potentialSolutions.Length)
				{
					return null;
				}
			}
			else
			{
				currSolution++;
			}

			GroupedContainersSet[] groupedContainersSet = new GroupedContainersSet[potentialSolutions[currSolution].containersSet.Count];

			int n = 0;

			foreach (KeyValuePair<int, int> kvp in potentialSolutions[currSolution].containersSet)
			{
				if (kvp.Key < containers.Length)
				{
					groupedContainersSet[n] = new GroupedContainersSet() { Container = containers[kvp.Key], Count = kvp.Value };
					n++;
				}
			}

			Int64 currVolume = 0;

			for (int i = 0; i < groupedContainersSet.Length; i++)
			{
				for (int j = i; j < groupedContainersSet.Length; j++)
				{
					if (groupedContainersSet[i].Container.Volume < groupedContainersSet[j].Container.Volume)
					{
						GroupedContainersSet swap = groupedContainersSet[i];
						groupedContainersSet[i] = groupedContainersSet[j];
						groupedContainersSet[j] = swap;
					}
				}

				currVolume += (groupedContainersSet[i].Container.Volume * groupedContainersSet[i].Count);
			}

			if (currVolume > maxContainersVolume)
			{
				maxContainersVolume = currVolume;
			}

			return groupedContainersSet;
		}

		/// <summary>
		/// recursive method calculate solutions
		/// </summary>
		/// <param name="remainingVolume">current volume restriction</param>
		/// <param name="remainingWeight">current weight restriction</param>
		/// <param name="containerNumber">number of current container (we investigate it for capability)</param>
		/// <param name="containersSet">containers set</param>
		/// <param name="containersCount">containers count (one more restriction count of containers must be less or equal to count of boxes)</param>
		/// <returns>set of posssible solutions</returns>
		private ContainersSet getContainersSet(Int64 remainingVolume, Double remainingWeight, int containerNumber, Dictionary<int, int> containersSet, int containersCount)
		{
			try
			{
				ContainersSet workSet = null;
				if ((remainingVolume > 0 || remainingWeight > 0) && boxesCount > containersCount)
				{
					for (int i = containerNumber; i < containers.Length; i++)
					{
						if (containersSet.ContainsKey(i) && containersSet[i] == containersLimit[i]) continue;
						if (containersSet.ContainsKey(i) && containersSet[i] > containersLimit[i]) return null;
						ContainersSet tempSet = null;
						Int64 currVolume = containers[i].Volume;
						Double currWeight = containers[i].Weight;
						Dictionary<int, int> cloneSet = new Dictionary<int, int>(containersSet);

						if (cloneSet.ContainsKey(i))
							cloneSet[i] += 1;
						else
							cloneSet.Add(i, 1);

						tempSet = getContainersSet(remainingVolume - currVolume, remainingWeight - currWeight, i, cloneSet, containersCount + 1);
						if (tempSet == null) break;
						if (workSet == null || workSet.volume > tempSet.volume) workSet = tempSet;
					}
					return workSet;
				}
				else
				{
					if (remainingVolume <= 0 && remainingWeight <= 0)
					{
						ContainersSet tempSet = new ContainersSet() { containersSet = containersSet, volume = maxContainersVolume - remainingVolume };

						if (potentialSolutions == null)
						{
							potentialSolutions = new ContainersSet[1];
							potentialSolutions[0] = tempSet;
						}
						else
						{
							ContainersSet[] swap = new ContainersSet[potentialSolutions.Length + 1];
							potentialSolutions.CopyTo(swap, 0);
							swap[potentialSolutions.Length] = tempSet;
							potentialSolutions = swap;
						}
						return tempSet;
					}
					else
					{
						return null;
					}
				}
			}
			catch (Exception ex)
			{
				logger.Append("getContainersSet: " + ex.Message, Logger.ERROR);
			}
			return null;
		}

	}
}