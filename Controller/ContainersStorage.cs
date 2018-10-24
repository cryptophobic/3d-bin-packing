using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimensionalBinTimePacking.DataTypes;
using BinTime.Logger;

namespace ThreeDimensionalBinTimePacking.Controller
{
	/// <summary>
	/// class which store containers
	/// </summary>
	public class ContainersStorage
	{
		/// <summary>
		/// containers
		/// </summary>
		private Container[] containers = null;

		/// <summary>
		/// pointer to Logger object
		/// </summary>
		private Logger logger = null;

		/// <summary>
		/// public constructor
		/// </summary>
		/// <param name="newContainers">containers</param>
		public ContainersStorage(Container[] newContainers)
		{
			logger = new Logger();
			try
			{
				if (newContainers == null)
					containers = new Container[0];
				else
					containers = newContainers;
				sortContainers();
			}
			catch (Exception ex)
			{
				logger.Append("ContainersScheduler: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// sorting containers in order of volumes
		/// </summary>
		private void sortContainers()
		{
			try
			{
				Container swap = null;
				for (int i = 0; i + 1 < containers.Length; i++)
				{
					for (int j = i; j < containers.Length; j++)
					{
						if (containers[i].Volume < containers[j].Volume)
						{
							swap = containers[i];
							containers[i] = containers[j];
							containers[j] = swap;
						}
					}
				}
			}
			catch (Exception ex)
			{
				logger.Append("sortContainers: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// accessor for containeers
		/// </summary>
		public Container[] Containers
		{
			get { return containers; }
		}
	}
}
