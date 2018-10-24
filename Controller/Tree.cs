using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinTime.Logger;
using ThreeDimensionalBinTimePacking.DataTypes;

namespace ThreeDimensionalBinTimePacking.Controller
{
	/// <summary>
	/// class for storing filled container (caching it for next solutions)
	/// </summary>
	public class TreeItem
	{
		/// <summary>
		/// FreeVolume
		/// </summary>
		/// <value>free volume in container</value>
		public Int64 FreeVolume
		{
			get { return container.FreeVolume; }
		}

		/// <summary>
		/// level
		/// </summary>
		/// <value>level in tree</value>
		public int Level
		{
			get
			{
				int level = 0;
				TreeItem swap = this;
				while (swap.node != null)
				{
					level++;
					swap = swap.node;
				}
				return level;
			}
		}

		/// <summary>
		/// filled container in current level
		/// </summary>
		public FilledContainer container;

		/// <summary>
		/// pointer to leaves
		/// </summary>
		public TreeItem leaves;

		/// <summary>
		/// pointer to node
		/// </summary>
		public TreeItem node;

		/// <summary>
		/// pointer to next leaf
		/// </summary>
		public TreeItem next;
	}

	/// <summary>
	/// class which stores filled containers in order of possible filling
	/// </summary>
	public class Tree
	{
		/// <summary>
		/// node of tree
		/// </summary>
		private TreeItem globalNode = null;

		/// <summary>
		/// logger
		/// </summary>
		private Logger logger = null;

		/// <summary>
		/// public constructor
		/// </summary>
		public Tree()
		{
			logger = new Logger();
			try
			{
				globalNode = new TreeItem() { container = null, leaves = null, next = null, node = null };
			}
			catch (Exception ex)
			{
				logger.Append("Tree: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// GlobalNode (Accessor)
		/// </summary>
		/// <value>node of tree</value>
		public TreeItem GlobalNode
		{
			get { return globalNode; }
		}

		/// <summary>
		/// add as next leaf
		/// </summary>
		/// <param name="item">item after that will be previous</param>
		/// <param name="newItem">new item</param>
		/// <returns>new itemm</returns>
		public TreeItem addAfter(TreeItem item, TreeItem newItem)
		{
			try
			{
				if (item != null)
				{
					newItem.next = item.next;
					newItem.node = item.node;
					newItem.leaves = null;
					item.next = newItem;
				}
				return newItem;
			}
			catch (Exception ex)
			{
				logger.Append("addAfter: " + ex.Message, Logger.ERROR);
			}
			return null;
		}

		/// <summary>
		/// add new item
		/// </summary>
		/// <param name="node">node for bew item</param>
		/// <param name="newItem">new item</param>
		/// <returns>added item</returns>
		public TreeItem addNew(TreeItem node, TreeItem newItem)
		{
			try
			{
				if (node != null)
				{
					newItem.node = node;
					newItem.next = node.leaves;
					node.leaves = newItem;
				}
				else
				{
					return null;
				}

				return newItem;
			}
			catch (Exception ex)
			{
				logger.Append("addNew: " + ex.Message, Logger.ERROR);
			}
			return null;
		}

		/// <summary>
		/// delete item from tree
		/// </summary>
		/// <param name="delItem">item to delete</param>
		/// <returns>deleted item</returns>
		public TreeItem delete(TreeItem delItem)
		{
			try
			{
				if (delItem != null)
				{
					if (delItem == globalNode)
					{
						delItem = null;
						globalNode = null;
					}
					TreeItem curr = delItem.node.leaves;
					TreeItem prev = null;

					do
					{
						if (curr == delItem)
						{
							if (prev == null)
							{
								curr.node.leaves = curr.next;
								curr = null;
							}
							else
							{
								prev.next = curr.next;
							}
							curr = null;
							delItem = null;
						}
						else
						{

							prev = curr;
							curr = curr.next;
						}

					} while (curr != null);
				}
				return delItem;
			}
			catch (Exception ex)
			{
				logger.Append("delete: " + ex.Message, Logger.ERROR);
			}
			return null;
		}
	}
}