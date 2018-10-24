using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinTime.Logger;
using ThreeDimensionalBinTimePacking.DataTypes;

namespace ThreeDimensionalBinTimePacking.SinglePack
{
	/// <summary>
	/// class for storing ordered collection of xs, ys and zs of extreme points
	/// </summary>
	public class List
	{
		/// <summary>
		/// class which stores ys
		/// </summary>
		private class ListHigh
		{
			public ListHigh prev;
			public ListHigh next;
			public ListMiddle down;
			public int value;
			public int count;
		}

		/// <summary>
		/// class which stores xs
		/// </summary>
		private class ListMiddle
		{
			public ListMiddle prev;
			public ListMiddle next;
			public ListBottom down;
			public ListHigh up;
			public int value;
			public int count;
		}

		/// <summary>
		/// class which stores zs
		/// </summary>
		private class ListBottom
		{
			public ListBottom prev;
			public ListBottom next;
			public ListMiddle up;
			public int value;
			public int box;
		}

		/// <summary>
		/// pointer to ListHigh
		/// </summary>
		private ListHigh listY = null;

		/// <summary>
		/// pointer to ListMiddle
		/// </summary>
		private ListMiddle listX = null;

		/// <summary>
		/// pointer to ListBottom
		/// </summary>
		private ListBottom listZ = null;

		/// <summary>
		/// pointer to first element
		/// </summary>
		private ListHigh first = null;

		/// <summary>
		/// pointer to current element
		/// </summary>
		private ListBottom current = null;

		private Logger logger = null;

		/// <summary>
		/// public constructor
		/// </summary>
		public List()
		{
			logger = new Logger();
		}

		~List() { }

		/// <summary>
		/// adding element to list
		/// </summary>
		/// <param name="boxNumber">box number</param>
		/// <param name="dot">coordinates</param>
		public void add(int boxNumber, Axis dot)
		{
			try
			{
				if (Current == null)
				{
					listY = new ListHigh() { next = null, prev = null, value = dot.Y, count = 0 };
					listX = new ListMiddle() { next = null, prev = null, value = dot.X, count = 0, up = listY };
					listY.down = listX;
					listY.count++;
					listZ = new ListBottom() { next = null, prev = null, box = boxNumber, value = dot.Z, up = listX };
					listX.down = listZ;
					listX.count++;
					first = listY;
					current = listZ;
				}
				else
				{
					reset();
					ExtremePoint point = Current;
					while (Current.dot.Y < dot.Y && point != null) point = getNext(1);
					if (Current.dot.Y == dot.Y)
					{
						while (Current.dot.X < dot.X && point != null) point = getNext(2);
						if (Current.dot.X == dot.X)
							while (Current.dot.Z < dot.Z && point != null) point = getNext(3);
					}

					if (current.up.up.value == dot.Y)
					{
						if (current.up.value == dot.X)
						{
							if (current.value == dot.Z)
							{
							}
							else // adding new item to level 3
							{
								listZ = new ListBottom() { box = boxNumber, up = current.up, value = dot.Z };
								current.up.count++;
								if (current.value < dot.Z) // adding new item to level 3 to the end
								{
									current.next = listZ;
									listZ.prev = current;
									listZ.next = null;
								}
								else if (current.value > dot.Z) // adding new item to level 3 before the current
								{
									listZ.prev = current.prev;
									listZ.next = current;
									current.prev = listZ;
									if (listZ.prev != null) listZ.prev.next = listZ;
									else listZ.up.down = listZ;
								}
							} // adding new item to level 3
						} // current.up.value == dot.x
						else // adding new item to level 2 and 3
						{
							listZ = new ListBottom() { box = boxNumber, next = null, prev = null, value = dot.Z };
							listX = new ListMiddle() { up = current.up.up, down = listZ, value = dot.X, count = 1 };
							listZ.up = listX;
							current.up.up.count++;
							if (current.up.value < dot.X) // new item to level 2 and 3 to the end of list
							{
								current.up.next = listX;
								listX.prev = current.up;
								listX.next = null;
							}
							else if (current.up.value > dot.X) // new item to level 2 and 3 before the current
							{
								listX.prev = current.up.prev;
								listX.next = current.up;
								current.up.prev = listX;
								if (listX.prev != null) listX.prev.next = listX;
								else listX.up.down = listX;
							}
						} // adding new item to level 2 and 3
					} // current.up.up.value == dot.y
					else // adding new item to level 1, 2 and 3
					{
						listZ = new ListBottom() { box = boxNumber, next = null, prev = null, value = dot.Z };
						listX = new ListMiddle() { next = null, prev = null, down = listZ, value = dot.X, count = 1 };
						listY = new ListHigh() { count = 1, down = listX, value = dot.Y };
						listZ.up = listX;
						listX.up = listY;
						if (current.up.up.value < dot.Y) // new item to level1 1, 2 and 3 to the end of list
						{
							current.up.up.next = listY;
							listY.prev = current.up.up;
							listY.next = null;
						}
						else if (current.up.up.value > dot.Y) // new item to level 1, 2 and 3 before the current
						{
							listY.prev = current.up.up.prev;
							listY.next = current.up.up;
							current.up.up.prev = listY;
							if (listY.prev != null) listY.prev.next = listY;
							else first = listY;
						}
					}
				}
			}
			catch (Exception ex)
			{
				logger.Append("add: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// remove extreme point
		/// </summary>
		/// <param name="dot">coordinates of point</param>
		public void remove(Axis dot)
		{
			try
			{
				reset();
				ExtremePoint point = Current;

				while (Current.dot.Y < dot.Y && point != null) point = getNext(1);
				if (Current.dot.Y == dot.Y)
				{
					while (Current.dot.X < dot.X && point != null) point = getNext(2);
					if (Current.dot.X == dot.X)
					{
						while (Current.dot.Z < dot.Z && point != null) point = getNext(3);
						if (Current.dot.Z == dot.Z)
						{

							listZ = current;
							if (getNext() == null) reset();
							if (listZ.prev != null) listZ.prev.next = listZ.next;
							else listZ.up.down = listZ.next;
							if (listZ.next != null) listZ.next.prev = listZ.prev;
							listX = listZ.up;
							if (listX.count > 1)
							{
								listX.count--;
							}
							else
							{
								if (listX.prev != null) listX.prev.next = listX.next;
								else listX.up.down = listX.next;
								if (listX.next != null) listX.next.prev = listX.prev;
								listY = listX.up;
								if (listY.count > 1)
								{
									listY.count--;
								}
								else
								{
									if (listY.prev == null) first = listY.next;
									if (listY.prev != null) listY.prev.next = listY.next;
									if (listY.next != null) listY.next.prev = listY.prev;
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				logger.Append("remove: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// get next element
		/// </summary>
		/// <param name="getFirst">if true, that get first and current will be first</param>
		/// <returns>pointer to "next" ExtremePoint object</returns>
		public ExtremePoint getNext(bool getFirst)
		{
			try
			{
				if (getFirst)
				{
					if (first != null)
					{
						current = first.down.down;
					}
					else
					{
						return null;
					}
				}
				else
					if (current != null)
						if (current.next != null)
							current = current.next;
						else if (current.up.next != null)
							current = current.up.next.down;
						else if (current.up.up.next != null)
							current = current.up.up.next.down.down;
						else
							return null;
				return Current;
			}
			catch (Exception ex)
			{
				logger.Append("getNext: " + ex.Message, Logger.ERROR);
			}
			return null;
		}

		/// <summary>
		/// current extreme point
		/// </summary>
		private ExtremePoint Current
		{
			get
			{
				if (current != null)
					return new ExtremePoint() { box = current.box, dot = new Axis() { X = current.up.value, Y = current.up.up.value, Z = current.value } };
				else
					return null;
			}
		}

		/// <summary>
		/// get next element in specified level (top, middle, bottom)
		/// </summary>
		/// <param name="level">level top=1, middle=2, bottom=3</param>
		/// <returns>pointer to ExtremePoint object</returns>
		private ExtremePoint getNext(int level)
		{
			try
			{
				switch (level)
				{
					case 1:
						if (current.up.up.next != null)
							current = current.up.up.next.down.down;
						else
							return null;
						break;
					case 2:
						if (current.up.next != null)
							current = current.up.next.down;
						else
							return null;
						break;
					case 3:
						if (current.next != null)
							current = current.next;
						else
							return null;
						break;
				}
				return Current;
			}
			catch (Exception ex)
			{
				logger.Append("getNext: " + ex.Message, Logger.ERROR);
				return null;
			}
		}

		/// <summary>
		/// sets current to first
		/// </summary>
		public void reset()
		{
			try
			{
				if (first == null) current = null;
				current = first.down.down;
			}
			catch (Exception ex)
			{
				logger.Append("reset: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// get next element after current
		/// </summary>
		/// <returns>pointer to the "next" ExtremePoint object</returns>
		public ExtremePoint getNext()
		{
			return getNext(false);
		}

		/// <summary>
		/// first element
		/// </summary>
		/// <returns>pointer to the first ExtremePoint object</returns>
		public ExtremePoint getFirst()
		{
			return getNext(true);
		}

	}
}