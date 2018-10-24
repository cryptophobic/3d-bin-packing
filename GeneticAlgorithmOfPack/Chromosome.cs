using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinTime.Logger;
using ThreeDimensionalBinTimePacking.DataTypes;


namespace ThreeDimensionalBinTimePacking.Genetic
{
	/// <summary>
	/// store info about chromosome
	/// </summary>
	public class Chromosome
	{
		/// <summary>
		/// relation orientation to parallels 
		/// </summary>
		public class Parallel
		{
			public int Xl;
			public int Zl;
			public int Yw;
			public int Zh;
		}

		/// <summary>
		/// orientaions of boxes
		/// </summary>
		private Dictionary<int, int> orientation = null;

		/// <summary>
		/// relations of orientaions to parallels
		/// </summary>
		private Dictionary<int, Parallel> parallels = null;

		/// <summary>
		/// translation box number to box
		/// </summary>
		private int[] translation = null;

		/// <summary>
		/// pointer to Logger
		/// </summary>
		private static Logger logger = null;

		/// <summary>
		/// public constructor
		/// </summary>
		/// <param name="boxes">boxes</param>
		/// <param name="r">pointer to Random object</param>
		public Chromosome(Dictionary<int, Product> boxes, Random r)
		{
			logger = new Logger();
			try
			{
				parallels = new Dictionary<int, Parallel>()
            {
                {1, new Parallel{Xl = 1, Zl = 0, Yw = 0, Zh = 0}},
                {2, new Parallel{Xl = 0, Zl = 0, Yw = 0, Zh = 0}},
                {3, new Parallel{Xl = 1, Zl = 0, Yw = 1, Zh = 1}},
                {4, new Parallel{Xl = 0, Zl = 0, Yw = 0, Zh = 1}},
                {5, new Parallel{Xl = 0, Zl = 1, Yw = 1, Zh = 0}},
                {6, new Parallel{Xl = 0, Zl = 1, Yw = 0, Zh = 0}}
            };

				orientation = new Dictionary<int, int>();

				translation = new int[boxes.Count];


				int i = 0;
				int newOrient = 1;
				foreach (KeyValuePair<int, Product> kvp in boxes)
				{
					newOrient = r.Next(1, 8);
					orientation.Add(kvp.Key, newOrient);
					translation[i] = kvp.Key; i++;
				}
			}
			catch (Exception ex)
			{
				logger.Append("Chromosome: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// count of genes
		/// </summary>
		public int Count
		{
			get { return orientation.Count; }
		}

		/// <summary>
		/// get orientation by boxnumber
		/// </summary>
		/// <param name="boxNumber">box number</param>
		/// <returns>orientation number</returns>
		public int getOrientation(int boxNumber)
		{
			if (boxNumber < translation.Length)
			{
				boxNumber = translation[boxNumber];
				if (orientation.ContainsKey(boxNumber))
					return orientation[boxNumber];
			}
			return 0;
		}

		/// <summary>
		/// set orientation to box
		/// </summary>
		/// <param name="boxNumber">box number</param>
		/// <param name="orient">orientation number</param>
		public void setOrientation(int boxNumber, int orient)
		{
			if (boxNumber < translation.Length)
			{
				boxNumber = translation[boxNumber];

				if (parallels.ContainsKey(orient) && orientation.ContainsKey(boxNumber))
				{
					orientation[boxNumber] = orient;
				}
			}
		}

		/// <summary>
		/// get size for rotated box
		/// </summary>
		/// <param name="originalSize">original size</param>
		/// <param name="box">box number</param>
		/// <returns>translated size</returns>
		public Size getTransSize(Size originalSize, int box)
		{
			try
			{
				if (orientation.ContainsKey(box))
				{
					int swap = 0;
					int boxOrient = orientation[box];
					switch (boxOrient)
					{
						case 1:
							break;
						case 2:
							swap = originalSize.Width;
							originalSize.Width = originalSize.Height;
							originalSize.Height = swap;
							break;
						case 3:
							swap = originalSize.Depth;
							originalSize.Depth = originalSize.Height;
							originalSize.Height = swap;
							break;
						case 4:
							swap = originalSize.Width;
							originalSize.Width = originalSize.Depth;
							originalSize.Depth = originalSize.Height;
							originalSize.Height = swap;
							break;
						case 5:
							swap = originalSize.Width;
							originalSize.Width = originalSize.Height;
							originalSize.Height = originalSize.Depth;
							originalSize.Depth = swap;
							break;
						case 6:
							swap = originalSize.Width;
							originalSize.Width = originalSize.Depth;
							originalSize.Depth = swap;
							break;
					}

				}
			}
			catch (Exception ex)
			{
				logger.Append("getTransSize: " + ex.Message, Logger.ERROR);
			}
			return originalSize;
		}

		~Chromosome() { }

	}
}