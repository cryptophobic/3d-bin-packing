using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimensionalBinTimePacking.DataTypes;
using ThreeDimensionalBinTimePacking.Genetic;
using BinTime.Logger;

namespace ThreeDimensionalBinTimePacking.Controller
{
	/// <summary>
	/// Pack boxes from boxes set into containers from container set
	/// </summary>
	/// <remarks>
	/// This class uses Genetic class for packing boxes in containers.
	/// It also stores solutions in a cache to speed up calculation of next solutions
	/// </remarks>
	public class Packer
	{
		/// <summary>
		/// pointer to Logger class
		/// </summary>
		private Logger logger = null;

		/// <summary>
		/// cache of solutions
		/// </summary>
		private Tree cache = null;

		/// <summary>
		/// volume of containers in container set
		/// </summary>
		private Int64 containersVolume = -1;

		/// <summary>
		/// volume of boxes in boxes set
		/// </summary>
		private Int64 boxesVolume = -1;

		/// <summary>
		/// containers set
		/// </summary>
		private GroupedContainersSet[] containersSet;

		/// <summary>
		/// boxes set
		/// </summary>
		private Product[] productsSet;

		/// <summary>
		/// public constructor. performs all necessary initializations. 
		/// </summary>
		public Packer()
		{
			cache = new Tree();
			logger = new Logger();
		}

		/// <summary>
		/// Containers Set
		/// </summary>
		/// <value>accessor and mutator for containers set</value>
		private GroupedContainersSet[] ContainersSet
		{
			get { return containersSet; }
			set { containersSet = value; containersVolume = -1; }
		}

		/// <summary>
		/// Boxes Set
		/// </summary>
		/// <value>accessor and mutator for boxes set</value>
		private Product[] Products
		{
			get { return productsSet; }
			set { productsSet = value; boxesVolume = -1; }
		}

		/// <summary>
		/// accessor for containers in containers set
		/// </summary>
		private Int64 ContainersVolume
		{
			get
			{
				if (containersVolume == -1)
				{
					containersVolume = 0;
					foreach (GroupedContainersSet groupedContainersSet in ContainersSet)
					{
						containersVolume += (groupedContainersSet.Container.Volume * groupedContainersSet.Count);
					}
				}
				return containersVolume;
			}
		}

		/// <summary>
		/// accessor for boxes in boxes set
		/// </summary>
		private Int64 BoxesVolume
		{
			get
			{
				if (boxesVolume == -1)
				{
					boxesVolume = 0;
					foreach (Product product in Products)
					{
						boxesVolume += product.Volume;
					}
				}
				return boxesVolume;
			}
		}

		/// <summary>
		/// current free volume in containers
		/// </summary>
		private Int64 FreeVolume
		{
			get { return ContainersVolume - BoxesVolume; }
		}

		/// <summary>
		/// create nodes for first solutions without boxes, just adding new containers
		/// </summary>
		/// <param name="containersSet">containers set</param>
		/// <returns>count of containers (I can't remember, why :) )</returns>
		private int createNodes(GroupedContainersSet[] containersSet)
		{
			TreeItem node = cache.GlobalNode;
			TreeItem item = node.leaves;
			TreeItem prev = null;

			int count = 0;

			foreach (GroupedContainersSet container in containersSet)
			{
				count += container.Count;
				do
				{
					if (item == null || item.container.Container.Volume > container.Container.Volume)
					{

						item = new TreeItem()
						{
							container = new FilledContainer()
							{
								Container = new Container()
								{
									Size = container.Container.Size,
									Weight = container.Container.Weight
								},
								Products = null
							},

							leaves = null
						};
						if (prev == null)
							cache.addNew(node, item);
						else
							cache.addAfter(prev, item);

						prev = item; item = item.next;
						break;
					}
					prev = item; item = item.next;

				} while (item != null);
			}
			return count;
		}

		/// <summary>
		/// trying to pack boxes in specified container, result storing also to treeitem for caching
		/// </summary>
		/// <param name="container">container</param>
		/// <param name="products">boxes</param>
		/// <param name="item">empty treeitem</param>
		/// <returns>container, full of boxes</returns>
		private FilledContainer tryToPack(Container container, Product[] products, TreeItem item)
		{
			try
			{
				if (item.FreeVolume < 0)
				{
					Genetic.Genetic gen = new Genetic.Genetic(products, new Container()
					{
						Size = container.Size,
						Weight = container.Weight
					});

					int generations = gen.GenerationCount;
					FilledContainer bestSolution = null;

					for (int i = 0; i < generations && !gen.IsBestExceed; i++)
					{
						gen.evolution();
					}

					bestSolution = gen.BestSolution;
					item.container = bestSolution;

				}
				foreach (KeyValuePair<int, Pack> kvp in item.container.Products_col)
				{
					if (kvp.Key < products.Length)
					{
						products[kvp.Key] = null;
					}
				}

			}
			catch (Exception ex)
			{
				logger.Append("tryToPack: " + ex.Message, Logger.ERROR);
			}
			return item.container;
		}

		/// <summary>
		/// calling method this.tryToPack in a different combinations with a recursive logic until nice solution found
		/// </summary>
		/// <param name="tempSet">containers set</param>
		/// <param name="tempProds">boxes set</param>
		/// <param name="node">node of caching tree</param>
		/// <param name="filledContainersSet">array of filled containers</param>
		/// <returns>array of filled containers</returns>
		private FilledContainersSet go(GroupedContainersSet[] tempSet, Product[] tempProds, TreeItem node, FilledContainersSet filledContainersSet)
		{
			try
			{
				TreeItem item = node.leaves;
				TreeItem prev = null;
				for (int index = 0; index < tempSet.Length; index++)
				{
					if (tempSet[index].Count == 0) continue;

					GroupedContainersSet[] newContainerSet = (GroupedContainersSet[])tempSet.Clone();
					Product[] newProducts = (Product[])tempProds.Clone();

					while (item != null && item.container.Container.Volume >= newContainerSet[index].Container.Volume)
					{
						prev = item;
						item = item.next;
					}
					if (prev == null || prev.container.Container.Volume < newContainerSet[index].Container.Volume)
					{
						item = new TreeItem()
						{
							container = new FilledContainer()
							{
								Container = new Container()
									{
										Size = newContainerSet[index].Container.Size,
										Weight = newContainerSet[index].Container.Weight
									}
							}
						};

						if (prev == null)
							cache.addNew(node, item);
						else
							cache.addAfter(prev, item);
						prev = item;
					}
					FilledContainer container = tryToPack(newContainerSet[index].Container, newProducts, prev);
					FilledContainersSet newFilledContainersSet = filledContainersSet.copy();
					newFilledContainersSet.addContainer(container);
					if (newFilledContainersSet == null) continue;
					if (newFilledContainersSet.FreeVolume > FreeVolume) continue;
					if (newFilledContainersSet.FreeVolume == FreeVolume && newFilledContainersSet.PackedCount < Products.Length) continue;
					if (newFilledContainersSet.PackedCount == Products.Length) return newFilledContainersSet;

					newFilledContainersSet = go(newContainerSet, newProducts, prev, newFilledContainersSet);
					if (newFilledContainersSet == null) continue;
					if (newFilledContainersSet.FreeVolume > FreeVolume) continue;
					if (newFilledContainersSet.FreeVolume == FreeVolume && newFilledContainersSet.PackedCount < Products.Length) continue;
					if (newFilledContainersSet.PackedCount == Products.Length) return newFilledContainersSet;

				}
			}
			catch (Exception ex)
			{
				logger.Append("packer.go: " + ex.Message, Logger.ERROR);
			}
			return null;
		}

		/// <summary>
		/// make all initialisations for tree and filled containers set and calls method this.go
		/// </summary>
		/// <param name="newContainersSet">containers set</param>
		/// <param name="newProducts">boxes set</param>
		/// <returns>array of filled containers</returns>
		public FilledContainersSet pack(GroupedContainersSet[] newContainersSet, Product[] newProducts)
		{
			try
			{
				containersVolume = -1;
				boxesVolume = -1;
				containersSet = newContainersSet;
				Products = newProducts;
				int count = createNodes(containersSet);

				GroupedContainersSet[] tempSet = (GroupedContainersSet[])containersSet.Clone();
				Product[] tempProds = (Product[])Products.Clone();

				return go(tempSet, tempProds, cache.GlobalNode, new FilledContainersSet(count));
			}
			catch (Exception ex)
			{
				logger.Append("pack: " + ex.Message, Logger.ERROR);
			}
			return null;
		}
	}
}