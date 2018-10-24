using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinTime.Logger;

namespace ThreeDimensionalBinTimePacking.DataTypes
{
	/// <summary>
	/// abstract class for sizeable and weightable products
	/// </summary>
	public abstract class BigAndHeavy
	{
		/// <summary>
		/// size
		/// </summary>
		private Size size;

		/// <summary>
		/// weight
		/// </summary>
		private Int64 weight;

		/// <summary>
		/// public constructor
		/// </summary>
		public BigAndHeavy()
		{
			size = new Size() { Depth = 0, Height = 0, Width = 0 };
			weight = 0;
		}

		/// <summary>
		/// accessor and mutator of size
		/// </summary>
		public Size Size
		{
			set
			{
				size.Depth = value.Depth;
				size.Height = value.Height;
				size.Width = value.Width;
			}
			get
			{
				return size;
			}
		}

		/// <summary>
		/// accessor for volume
		/// </summary>
		public Int64 Volume
		{
			get
			{
				return Size.Volume;
			}
		}

		/// <summary>
		/// accessor for weight
		/// </summary>
		public Int64 Weight
		{
			set { weight = value; }
			get { return weight; }
		}
	}
}
