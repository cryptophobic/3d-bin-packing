using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimensionalBinTimePacking.DataTypes;
using BinTime.Logger;

namespace ThreeDimensionalBinTimePacking.Controller
{
	/// <summary>
	/// calculating boxes/containers consistence
	/// </summary>
	/// <remarks>	
	/// calculating consistence boxes to containers due to sizes or weight, 
	/// also calculating limits of containers for solution searching
	/// </remarks>
	public class ContainersBoxesConsistence
	{
		/// <summary>
		/// containers
		/// </summary>
		private Container[] containers = null;

		/// <summary>
		/// boxes
		/// </summary>
		private Product[] boxes = null;

		/// <summary>
		/// boxes volume
		/// </summary>
		private Int64 boxesVolume = -1;

		/// <summary>
		/// boxes weight
		/// </summary>
		private Int64 boxesWeight = -1;

		/// <summary>
		/// removed containers
		/// </summary>
		private Container[] notConsistentContainers = null;

		/// <summary>
		/// removed boxes
		/// </summary>
		private Product[] notConsistentBoxes = null;

		/// <summary>
		/// collection with limits of containers
		/// </summary>
		private Dictionary<int, int> containersLimit = null;

		/// <summary>
		/// check consistence boxes to containers
		/// </summary>
		/// <param name="newContainers">containers</param>
		/// <param name="newBoxes">boxes</param>
		public ContainersBoxesConsistence(Container[] newContainers, Product[] newBoxes)
		{
			if (newContainers == null)
				containers = new Container[0];
			else
				containers = newContainers;

			if (newBoxes == null)
				boxes = new Product[0];
			else
				boxes = newBoxes;

			containersLimit = new Dictionary<int, int>();

			containersBoxesConsistence();
		}

		/// <summary>
		/// accessor for containers
		/// </summary>
		public Container[] Containers
		{
			get { return containers; }
		}

		/// <summary>
		/// accessor for containers count
		/// </summary>
		public int ContainersCount
		{
			get { return containers.Length; }
		}

		/// <summary>
		/// accessor for removed containers
		/// </summary>
		public Container[] RemovedContainers
		{
			get { return notConsistentContainers; }
		}

		/// <summary>
		/// accessor for removed containers count
		/// </summary>
		public int RemovedContainersCount
		{
			get { return notConsistentContainers.Length; }
		}

		/// <summary>
		/// accessor for boxes
		/// </summary>
		public Product[] Boxes
		{
			get { return boxes; }
		}

		/// <summary>
		/// accessor for boxes count
		/// </summary>
		public int BoxesCount
		{
			get { return boxes.Length; }
		}

		/// <summary>
		/// accessor for removed boxes
		/// </summary>
		public Product[] RemovedBoxes
		{
			get { return notConsistentBoxes; }
		}

		/// <summary>
		/// accessor for removed boxes count
		/// </summary>
		public int RemovedBoxesCount
		{
			get { return notConsistentBoxes.Length; }
		}

		/// <summary>
		/// boxes volume
		/// </summary>
		public Int64 BoxesVolume
		{
			get
			{
				if (boxesVolume == -1)
				{
					boxesVolume = 0;
					foreach (Product box in boxes)
					{
						boxesVolume += box.Volume;
					}
				}
				return boxesVolume;
			}
		}

		/// <summary>
		/// boxes weight
		/// </summary>
		public Int64 BoxesWeight
		{
			get
			{
				if (boxesWeight == -1)
				{
					boxesWeight = 0;
					foreach (Product box in boxes)
					{
						boxesWeight += box.Weight;
					}
				}
				return boxesWeight;
			}
		}

		/// <summary>
		/// accessor for containers limit
		/// </summary>
		public Dictionary<int, int> ContainersLimit
		{
			get { return containersLimit; }
			set { containersLimit = value; }
		}

		/// <summary>
		/// check consistence boxes to containers
		/// </summary>
		private void containersBoxesConsistence()
		{
			int removedProductsCount = 0;
			int removedContainersCount = 0;

			Container[] removedContainers = new Container[containers.Length];
			Product[] removedProducts = new Product[boxes.Length];

			int[] fitProducts = new int[boxes.Length];

			for (int i = 0; i < fitProducts.Length; i++)
			{
				fitProducts[i] = 0;
			}

			for (int i = 0; i < containers.Length; i++)
			{
				containersLimit.Add(i, 0);
				for (int j = 0; j < boxes.Length; j++)
				{
					if (containers[i].isFit(boxes[j]))
					{
						containersLimit[i]++;
						fitProducts[j]++;
					}
					else if (i + 1 >= containers.Length && fitProducts[j] == 0)
					{
						removedProducts[j] = boxes[j];
						boxes[j] = null;
						removedProductsCount++;
					}
				}
				if (containersLimit[i] == 0)
				{
					removedContainers[i] = containers[i];
					containers[i] = null;
					removedContainersCount++;
				}
			}

			if (removedContainersCount > 0)
			{
				Container[] swapRem = new Container[removedContainersCount];
				Container[] swap = new Container[containers.Length - removedContainersCount];
				Dictionary<int, int> swapLimit = new Dictionary<int, int>();

				int del = 0;
				int left = 0;

				for (int i = 0; i < containers.Length; i++)
				{
					if (containers[i] == null)
					{
						swapRem[del] = removedContainers[i];
						del++;
					}
					else
					{
						swapLimit.Add(left, containersLimit[i]);
						swap[left] = containers[i];
						left++;
					}
				}
				notConsistentContainers = swapRem;
				containersLimit = swapLimit;
				containers = swap;
			}
			else
			{
				notConsistentContainers = new Container[0];
			}

			if (removedProductsCount > 0)
			{
				Product[] swapRem = new Product[removedProductsCount];
				Product[] swap = new Product[boxes.Length - removedProductsCount];

				int del = 0;
				int left = 0;

				for (int i = 0; i < boxes.Length; i++)
				{
					if (boxes[i] == null)
					{
						swapRem[del] = removedProducts[i];
						del++;
					}
					else
					{
						swap[left] = boxes[i];
						left++;
					}
				}
				notConsistentBoxes = swapRem;
				boxes = swap;
			}
			else
			{
				notConsistentBoxes = new Product[0];
			}

		}
		
	}
}
