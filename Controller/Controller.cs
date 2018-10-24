using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimensionalBinTimePacking.DataTypes;
using BinTime.Logger;

namespace ThreeDimensionalBinTimePacking.Controller
{
	/// <summary>
	/// class for managing containers and boxes
	/// </summary>
	/// <remarks>it takes containers and boxes and try to find best solution for them</remarks>
	public class Controller
	{
		/// <summary>
		/// pointer to ContainersStorage class
		/// </summary>
		private ContainersStorage containersStorage = null;

		/// <summary>
		/// pointer to BoxesStorage class
		/// </summary>
		private BoxesStorage boxesFactory = null;

		/// <summary>
		/// pointer to ContainersBoxesConsistence class
		/// </summary>
		private ContainersBoxesConsistence consistence = null;

		/// <summary>
		/// pointeer to Solution class
		/// </summary>
		private Solution solution = null;

		/// <summary>
		/// pointer to Logger class
		/// </summary>
		private Logger logger;
		
		/// <summary>
		/// public constructor
		/// </summary>
		/// <param name="newContainers">containers set</param>
		/// <param name="newProducts">boxes</param>
		public Controller(Container[] newContainers, Product[] newProducts)
		{
			logger = new Logger();
			solution = new Solution();

			containersStorage = new ContainersStorage(newContainers);
			boxesFactory = new BoxesStorage(newProducts);

			consistence = new ContainersBoxesConsistence(containersStorage.Containers, boxesFactory.Boxes);
		}

		/// <summary>
		/// accessor for boxes count
		/// </summary>
		public int ProductsCount
		{
			get { return consistence.BoxesCount; }
		}

		/// <summary>
		/// accessor for boxes volume
		/// </summary>
		public Int64 ProductsVolume
		{
			get { return consistence.BoxesVolume; } 
		}

		/// <summary>
		/// accessor for boxes weight
		/// </summary>
		public Int64 ProductsWeight
		{
			get { return consistence.BoxesWeight; }
		}

		/// <summary>
		/// calling containersScheduler and packer methods to find solution
		/// </summary>
		/// <returns>pointer to Solution object</returns>
		public Solution getSolution()
		{
			try
			{
				solution.RemovedBoxes = consistence.RemovedBoxes;
				solution.RemovedContainers = consistence.RemovedContainers;
				if (consistence.BoxesCount > 0 && consistence.ContainersCount > 0)
				{
					// create new containers scheduler object
					// initialize it with wanted weight, volume and containers limits
					ContainersScheduler containersScheduler = new ContainersScheduler(ProductsWeight, ProductsVolume, consistence.ContainersLimit, consistence.Containers, ProductsCount);

					Packer packer = new Packer();

					FilledContainersSet filledContainersSet = null;
					GroupedContainersSet[] groupedContainersSet = containersScheduler.getNextSolution();

					while (filledContainersSet == null && groupedContainersSet != null)
					{
						// get next set of containers from scheduler
						// try to pack boxes in it

						logger.Append("trying solution");

						foreach (GroupedContainersSet groupedContainer in groupedContainersSet)
						{
							logger.Append(groupedContainer.ToString());
						}

						filledContainersSet = packer.pack(groupedContainersSet, consistence.Boxes);
						groupedContainersSet = containersScheduler.getNextSolution();

					}

					solution.Containers = filledContainersSet;
				}

				return solution;
			}
			catch (Exception ex)
			{
				logger.Append("getContainersBoxesSet: " + ex.Message, Logger.ERROR);
			}
			return null;
		}

	}
}
