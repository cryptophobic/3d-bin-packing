// Container.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinTime.Logger;
using ThreeDimensionalBinTimePacking.DataTypes;

namespace ThreeDimensionalBinTimePacking.SinglePack
{
	/// <summary>
	/// class which works with container
	/// </summary>
	/// <remarks>
	/// I mean that this class works with one container, pack boxes to container, check consistence box to container, 
	/// calculates weihts and volumes of container and packed boxes 
	/// </remarks>
	public class ProcessContainer : Container
	{
		/// <summary>
		/// packed volume
		/// </summary>
		private Int64 packedVolume = 0;

		/// <summary>
		/// packed weight
		/// </summary>
		private Int64 packedWeight = 0;

		/// <summary>
		/// volume of smalles not packed box
		/// </summary>
		private Int64 minNotPackedVolume = -1;

		/// <summary>
		/// height of packed boxes from bottom of container to top of the top box
		/// </summary>
		private int height = 0;

		/// <summary>
		/// collection of packed boxes
		/// </summary>
		private Dictionary<int, Pack> packed = null;

		/// <summary>
		/// collection of not packed boxes
		/// </summary>
		private Dictionary<int, Size> notPacked = null;

		/// <summary>
		/// collection of points to put new box
		/// </summary>
		private List p = null;

		/// <summary>
		/// public constructor of ProcessContainer
		/// </summary>
		/// <param name="newSize">size of container</param>
		/// <param name="newWeight">weight condition of container</param>
		public ProcessContainer(Size newSize, Int64 newWeight)
		{
			p = new List();

			packed = new Dictionary<int, Pack>();
			notPacked = new Dictionary<int, Size>();

			Size = newSize;
			Weight = newWeight;
		}

		/// <summary>
		/// packed weight
		/// </summary>
		public Int64 PackedWeight
		{
			get { return packedWeight; }
		}

		/// <summary>
		/// packed volume
		/// </summary>
		public Int64 PackedVolume
		{
			get { return packedVolume; }
		}

		/// <summary>
		/// free volume in container
		/// </summary>
		public Int64 RemainingVolume
		{
			get { return Volume - PackedVolume; }
		}

		/// <summary>
		/// collection of packed boxes
		/// </summary>
		public Dictionary<int, Pack> Packed
		{
			get { return packed; }
		}

		/// <summary>
		/// count of packed boxes
		/// </summary>
		public int PackedCount
		{
			get { return packed.Count; }
		}

		/// <summary>
		/// collection of not packed boxes
		/// </summary>
		public Dictionary<int, Size> NotPacked
		{
			get { return notPacked; }
		}

		/// <summary>
		/// count of not packed boxes
		/// </summary>
		public int NotPackedCount
		{
			get { return notPacked.Count; }
		}

		/// <summary>
		/// check if possible to put box to specified dot in container
		/// </summary>
		/// <param name="boxSize">size of the box to insert</param>
		/// <param name="dot">dot for inserting box to</param>
		/// <param name="box">box with dot</param>
		/// <returns></returns>
		private Size possibleToPut(Size boxSize, Axis dot, Pack box)
		{
			Size convexHull = null;
			try
			{
				if (dot.X + boxSize.Width > Size.Width)
					return null;
				if (dot.Y + boxSize.Height > Size.Height)
					return null;
				if (dot.Z + boxSize.Depth > Size.Depth)
					return null;

				Relation relate = null;

				if (dot.X != box.Axis.X) // right to the box
				{
					convexHull = new Size() { Height = box.Product.Size.Height, Depth = box.Product.Size.Depth, Width = boxSize.Width };
					relate = new Relation() { front = false, right = true, top = false };
				}
				else if (dot.Y != box.Axis.Y) // on the top of the box
				{
					convexHull = new Size() { Height = boxSize.Height, Depth = box.Product.Size.Depth, Width = box.Product.Size.Width };
					relate = new Relation() { front = false, right = false, top = true };
				}
				else if (dot.Z != box.Axis.Z) // front 
				{
					convexHull = new Size() { Height = box.Product.Size.Height, Depth = boxSize.Depth, Width = box.Product.Size.Width };
					relate = new Relation() { front = true, right = false, top = false };
				}
				foreach (KeyValuePair<int, Pack> kvp in packed)
				{
					Pack currBox = kvp.Value as Pack;
					bool ok = false;
					bool xcross = false;
					bool ycross = false;
					bool zcross = false;
					if (dot.X >= currBox.Axis.X + currBox.Product.Size.Width || dot.X + boxSize.Width <= currBox.Axis.X) ok = true; else xcross = true;
					if (dot.Y >= currBox.Axis.Y + currBox.Product.Size.Height || dot.Y + boxSize.Height <= currBox.Axis.Y) ok = true; else ycross = true;
					if (dot.Z >= currBox.Axis.Z + currBox.Product.Size.Depth || dot.Z + boxSize.Depth <= currBox.Axis.Z) ok = true; else zcross = true;
					if (ok)
					{
						if ((relate.front && zcross) || (relate.top && ycross))
						{
							if (dot.X < currBox.Axis.X && dot.X + convexHull.Width > currBox.Axis.X)
							{
								convexHull.Width = currBox.Axis.Z - dot.X;
							}
						}
						else if ((relate.front && zcross) || (relate.right && xcross))
						{
							if (dot.Y < currBox.Axis.Y && dot.Y + convexHull.Height > currBox.Axis.Y)
							{
								convexHull.Height = currBox.Axis.Y - dot.Y;
							}
						}
						else if ((relate.top && ycross) || (relate.right && xcross))
						{
							if (dot.Z < currBox.Axis.Z && dot.Z + convexHull.Depth > currBox.Axis.Z)
							{
								convexHull.Depth = currBox.Axis.Z - dot.Z;
							}
						}
					}
					else
						return null;
				}
			}
			catch (Exception ex)
			{
				logger.Append("possibleToPut:" + ex.Message, Logger.ERROR);
			}
			return convexHull;
		}



		/// <summary>
		/// function which evaluates state of packing
		/// </summary>
		/// <returns>if remaining volume less that minimal volume of not packed boxes returns true
		/// else if each of convex hulls less that minimal volume of not packed boxes returns true
		/// else return false</returns>
		public bool evaluateState1()
		{
			try
			{
				if (minNotPackedVolume >= 0)
				{
					if (RemainingVolume < minNotPackedVolume)
					{
						return true;
					}
					else if (packed.Count > 0)
					{

						ExtremePoint dots = p.getFirst();
						do
						{
							//Pack box = packed[dots.box];
							Axis dot = null;

							if (dots == null)
								dot = new Axis() { X = 0, Y = height, Z = 0 };
							else
								dot = dots.dot;

							Size convexHull = new Size() { Height = Size.Height - dot.Y, Depth = Size.Depth - dot.Z, Width = Size.Width - dot.X };

							foreach (KeyValuePair<int, Pack> kvp in packed)
							{
								Pack currBox = kvp.Value as Pack;
								Product productInBox = currBox.Product;
								bool ok = false;

								if (dot.X >= currBox.Axis.X + productInBox.Size.Width || dot.X + convexHull.Width <= currBox.Axis.X) ok = true;
								if (dot.Y >= currBox.Axis.Y + productInBox.Size.Height || dot.Y + convexHull.Height <= currBox.Axis.Y) ok = true;
								if (dot.Z >= currBox.Axis.Z + productInBox.Size.Depth || dot.Z + convexHull.Depth <= currBox.Axis.Z) ok = true;

								if (!ok)
								{
									if (dot.X < currBox.Axis.X)
										convexHull.Width = currBox.Axis.X - dot.X;
									else if (dot.Z < currBox.Axis.Z)
										convexHull.Depth = currBox.Axis.Z - dot.Z;
									else if (dot.Y < currBox.Axis.Y)
										convexHull.Height = currBox.Axis.Y - dot.Y;

								}

							}

							Int64 currVolume = convexHull.Width * convexHull.Depth * convexHull.Height;
							if (currVolume > minNotPackedVolume)
							{
								return false;
							}

						} while ((dots != null) && (dots = p.getNext()) != null);
					}
					else
					{
						return false;
					}

				}

				return true;
			}
			catch (Exception ex)
			{
				logger.Append("EvaluateState1: " + ex.Message, Logger.ERROR);
			}
			return false;
		}

		/// <summary>
		/// output solution to the log file
		/// </summary>
		public void outputSolution()
		{
			try
			{
				foreach (KeyValuePair<int, Pack> kvp in packed)
				{
					logger.Append("product=" + kvp.Value.Product.ToString() + " (" + kvp.Value.Product.Size.ToString() + ") " + kvp.Value.ToString());
				}
			}
			catch (Exception ex)
			{
				logger.Append("outputSolution:" + ex.Message, Logger.ERROR);
			}
		}

		public void outputTotals()
		{
			try
			{
				logger.Append("Total packed boxes=" + PackedCount + ", container volume=" + Volume + ", total packed volume=" + PackedVolume + ", remaining volume=" + RemainingVolume);
			}
			catch (Exception ex)
			{
				logger.Append("outputAll:" + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// output state of packing to the log file
		/// </summary>
		public void outputAll()
		{
			try
			{
				outputSolution();
				outputTotals();
			}
			catch (Exception ex)
			{
				logger.Append("outputAll:" + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// add extreme points of packed box
		/// </summary>
		/// <param name="boxNumber">box number</param>
		/// <param name="axis">position of box in container</param>
		/// <param name="size">size of box</param>
		private void addBoxExtremes(int boxNumber, Axis axis, Size size)
		{
			try
			{
				p.add(boxNumber, new Axis() { X = axis.X, Y = axis.Y, Z = axis.Z + size.Depth });
				p.add(boxNumber, new Axis() { X = axis.X + size.Width, Y = axis.Y, Z = axis.Z });
				p.add(boxNumber, new Axis() { X = axis.X, Y = axis.Y + size.Height, Z = axis.Z });
			}
			catch (Exception ex)
			{
				logger.Append("addBoxExtremes:" + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// tries to insert box to container
		/// </summary>
		/// <param name="boxNumber">box number</param>
		/// <param name="newBox">box</param>
		/// <param name="boxSize">box size</param>
		/// <returns></returns>
		public Axis insertBox(int boxNumber, Product newBox, Size boxSize)
		{
			float maxIndex = 0;
			Axis maxDot = null;
			float index = 0;

			try
			{
				if (newBox.Weight > Weight - packedWeight)
				{
					// can't insert due to weight conditions
				}
				else if (p.getFirst() == null && packed.Count == 0)
				{
					if (base.possibleToPut(boxSize, new Axis() { X = 0, Y = 0, Z = 0 }))
					{
						maxDot = new Axis() { X = 0, Y = 0, Z = 0 };
					}

					if (maxDot != null)
					{

						packed.Add(boxNumber, new Pack()
						{
							Product = newBox,
							Axis = maxDot
						});
						packedVolume = boxSize.Volume;
						packedWeight = newBox.Weight;

						height = boxSize.Height;

						addBoxExtremes(boxNumber, maxDot, boxSize);
					}
				}
				else
				{
					ExtremePoint dots = p.getFirst();
					do
					{
						Size convexHull = null;
						convexHull = possibleToPut(boxSize, dots.dot, packed[dots.box]);
						if (convexHull != null)
						{
							index = convexHull.Width * convexHull.Depth * convexHull.Height * convexHull.Height;
							index /= packedVolume + boxSize.Height * boxSize.Width * boxSize.Depth;
							if (index > maxIndex)
							{
								maxIndex = index;
								maxDot = dots.dot;
							}
						}
					} while ((dots = p.getNext()) != null);

					if (maxDot == null)	if (base.possibleToPut(boxSize, new Axis() { X = 0, Y = height, Z = 0 }))
						maxDot = new Axis() { X = 0, Y = height, Z = 0 };

					if (maxDot != null)
					{
						if (height < (maxDot.Y + boxSize.Height)) height = maxDot.Y + boxSize.Height;
						Axis boxPlace = maxDot;
						packed.Add(boxNumber, new Pack()
						{
							Product = newBox,
							Axis = maxDot
						});
						packedVolume += boxSize.Volume;
						packedWeight += newBox.Weight;
						p.remove(maxDot);
						addBoxExtremes(boxNumber, maxDot, boxSize);
					}
				}
			}
			catch (Exception ex)
			{
				logger.Append("insertBox:" + ex.Message, Logger.ERROR);
			}

			if (maxDot == null)
			{
				if (notPacked.ContainsKey(boxNumber))
				{
					notPacked[boxNumber] = boxSize;
				}
				else
				{
					notPacked.Add(boxNumber, boxSize);
				}
				Int64 currVolume = boxSize.Depth * boxSize.Height * boxSize.Width;
				if (minNotPackedVolume > currVolume || minNotPackedVolume == -1) minNotPackedVolume = currVolume;
			}

			return maxDot;
		}
	}
}
