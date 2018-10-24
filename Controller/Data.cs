using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimensionalBinTimePacking.DataTypes;
using BinTime.Logger;

namespace ThreeDimensionalBinTimePacking.Controller
{
	/// <summary>
	/// class wich stores parameters of container and count of containers
	/// </summary>
	public class GroupedContainersSet
	{
		/// <summary>
		/// count of countainers
		/// </summary>
		private int count;

		/// <summary>
		/// mutator and accessor for count of countainers
		/// </summary>
		public int Count
		{
			get { return count; }
			set { count = value; }
		}

		/// <summary>
		/// container
		/// </summary>
		private Container container;

		/// <summary>
		/// mutator and accessor for container
		/// </summary>
		public Container Container
		{
			get { return container; }
			set { container = value; }
		}

		/// <summary>
		/// overrided method ToString
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			if (Container != null)
				return Container.ToString() + ", count=" + Count.ToString();
			else
				return base.ToString();
		}
	}

	/// <summary>
	/// containers set
	/// </summary>
	public class ContainersSet
	{
		public Int64 volume;
		public Dictionary<int, int> containersSet;
	}

	/// <summary>
	/// Total solution 
	/// </summary>
	public class Solution
	{
		/// <summary>
		/// public constructor
		/// </summary>
		public Solution()
		{
			containers = new FilledContainersSet(0);
			removedBoxes = new Product[0];
			removedContainers = new Container[0];
		}

		/// <summary>
		/// free volume in containers
		/// </summary>
		public Int64 FreeVolume
		{
			get { return containers.FreeVolume; }
		}

		/// <summary>
		/// total packed volume
		/// </summary>
		public Int64 PackedVolume
		{
			get { return containers.PackedVolume; }
		}

		/// <summary>
		/// total containers volume
		/// </summary>
		public Int64 ContainersVolume
		{
			get { return containers.Volume; }
		}

		/// <summary>
		/// containers count
		/// </summary>
		public int ContainersCount
		{
			get { return containers.Count; }
		}

		/// <summary>
		/// acessor and mutator for removed containers due to sizes
		/// </summary>
		public Container[] RemovedContainers
		{
			set
			{
				if (value == null)
				{
					removedContainers = new Container[0];
				}
				else
				{
					removedContainers = value;
				}
			}
			get { return removedContainers; }
		}

		/// <summary>
		/// acessor and mutator for removed boxes due to sizes
		/// </summary>
		public Product[] RemovedBoxes
		{
			set
			{
				if (value == null)
				{
					removedBoxes = new Product[0];
				}
				else
				{
					removedBoxes = value;
				}
			}
			get { return removedBoxes; }
		}

		/// <summary>
		/// acessor and mutator for filled containers
		/// </summary>
		public FilledContainersSet Containers
		{
			set {
				if (value == null)
				{
					containers = new FilledContainersSet(0);
				}
				else
				{
					containers = value;
				}
			}
			get { return containers; }
		}

		/// <summary>
		/// removed containers due to sizes
		/// </summary>
		private Container[] removedContainers;

		/// <summary>
		/// removed boxes due to sizes
		/// </summary>
		private Product[] removedBoxes;

		/// <summary>
		/// filled containers
		/// </summary>
		private FilledContainersSet containers;

		/// <summary>
		/// overrided method ToString
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string output = "";
			if (RemovedBoxes.Length > 0)
			{
				output += "removed boxes due to sizes\n";
				foreach (Product removedBox in RemovedBoxes)
				{
					output += removedBox.ToString() + "\n";
				}
			}

			if (RemovedContainers.Length > 0)
			{
				output += "removed containers due to sizes\n";
				foreach (Container removedContainer in RemovedContainers)
				{
					output += removedContainer.ToString() + "\n";
				}
			}
			output += "Packed boxes = " + Containers.PackedCount.ToString() + ", packed volume = " + PackedVolume.ToString() + "\n";
			output += "Containers used = " + ContainersCount.ToString() + ", containers volume = " + ContainersVolume.ToString() + ", free volume = " + FreeVolume.ToString() + "\n";

			foreach (FilledContainer container in Containers.Containers)
			{
				output += "Container: " + container.Container.ToString() + "\n";
				output += "packed volume=" + container.PackedVolume.ToString() + ", packed count=" + container.PackedCount.ToString() + ", free volume=" + container.FreeVolume.ToString() + "\n";
				foreach (Pack product in container.Products)
				{
					output += product.Product.ToString() + " " + product.ToString() + "\n";
				}
			}
			return output;
		}

	}

	/// <summary>
	/// class for storing filled containers
	/// </summary>
	public class FilledContainersSet
	{
		/// <summary>
		/// packed volume
		/// </summary>
		private Int64 packedVolume = -1;

		/// <summary>
		/// free volume
		/// </summary>
		private Int64 freeVolume = -1;

		/// <summary>
		/// packed volume
		/// </summary>
		private Int64 volume = -1;

		/// <summary>
		/// count of packed boxes
		/// </summary>
		private int packedCount = -1;

		/// <summary>
		/// last index in set
		/// </summary>
		private int last;

		/// <summary>
		/// public constructor
		/// </summary>
		/// <param name="count">size of set</param>
		public FilledContainersSet(int count)
		{
			containers = new FilledContainer[count];
			last = -1;

		}

		/// <summary>
		/// calculate volumes, counts
		/// </summary>
		private void calculateTotals()
		{
			if (last >= 0)
			{

				packedVolume = 0;
				volume = 0;
				packedCount = 0;
				for (int i = 0; i < Count; i++)
				{
					packedVolume += containers[i].PackedVolume;
					volume += containers[i].Container.Volume;
					packedCount += containers[i].PackedCount;
				}
			}
		}

		/// <summary>
		/// size of set
		/// </summary>
		public int Size
		{
			get { return containers.Length; }
		}

		/// <summary>
		/// count of items in set
		/// </summary>
		public int Count
		{
			get { return last + 1; }
		}

		/// <summary>
		/// accessor for count of packeed boxes
		/// </summary>
		public int PackedCount
		{
			get
			{
				if (packedCount == -1)
				{
					calculateTotals();
				}
				return packedCount;
			}
		}

		/// <summary>
		/// accessor for total volume of packed boxes
		/// </summary>
		public Int64 PackedVolume
		{
			get
			{
				if (packedVolume == -1)
				{
					calculateTotals();
				}
				return packedVolume;
			}
		}

		/// <summary>
		/// accessor for total volume of containers
		/// </summary>
		public Int64 Volume
		{
			get
			{
				if (volume == -1)
				{
					calculateTotals();
				}
				return volume;
			}
		}

		/// <summary>
		/// accessor for free volume in containers
		/// </summary>
		public Int64 FreeVolume
		{
			get
			{
				if (freeVolume == -1 && (Volume != -1 && PackedVolume != -1))
				{
					freeVolume = Volume - PackedVolume;
				}
				return freeVolume;
			}
		}

		/// <summary>
		/// add container to set
		/// </summary>
		/// <param name="container">filled container</param>
		/// <returns>index of container in set</returns>
		public int addContainer(FilledContainer container)
		{
			if (container != null)
			{

				if (last + 1 >= Size)
				{
					FilledContainer[] swap = new FilledContainer[Size + 1];
					containers.CopyTo(swap, 0);
					Containers = swap;
				}
				else
				{
					last++;
				}

				freeVolume = -1;
				volume = -1;
				packedVolume = -1;
				packedCount = -1;

				containers[last] = container;
			}

			return last;

		}

		/// <summary>
		/// mutator and accessor for containers
		/// </summary>
		public FilledContainer[] Containers
		{
			set
			{
				containers = value; 
				freeVolume = -1; 
				volume = -1; 
				packedVolume = -1;
				last = containers.Length - 1; 
				packedCount = -1;
			}
			get
			{
				while (Size > Count)
				{

					FilledContainer[] swap = new FilledContainer[Count];
					int n = 0;
					for (int i = 0; i < containers.Length; i++)
					{
						if (containers[i] != null)
						{
							swap[n] = containers[i];
							n++;
						}
					}
					last = n - 1;

					containers = swap;

				}
				return containers;
			}
		}

		/// <summary>
		/// containers
		/// </summary>
		private FilledContainer[] containers;

		/// <summary>
		/// copy of object
		/// </summary>
		/// <returns>new copy of this object</returns>
		public FilledContainersSet copy()
		{
			FilledContainersSet swap = new FilledContainersSet(Size);
			foreach (FilledContainer container in containers)
			{
				swap.addContainer(container);
			}
			return swap;
		}
	}

}
