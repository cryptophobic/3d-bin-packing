using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimensionalBinTimePacking.DataTypes;

namespace ThreeDimensionalBinTimePacking.SinglePack
{
	/// <summary>
	/// class which stores extreme points (points to put next box)
	/// </summary>
	public class ExtremePoint
	{
		/// <summary>
		/// box
		/// </summary>
		public int box;
		/// <summary>
		/// coordinates of box
		/// </summary>
		public Axis dot;
	}

	/// <summary>
	/// box relation
	/// </summary>
	public class Relation
	{
		/// <summary>
		/// in front of
		/// </summary>
		public bool front;
		/// <summary>
		/// in right from
		/// </summary>
		public bool right;
		/// <summary>
		/// on the top of
		/// </summary>
		public bool top;
	}
}
