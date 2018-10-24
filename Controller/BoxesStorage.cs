using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimensionalBinTimePacking.DataTypes;
using BinTime.Logger;

namespace ThreeDimensionalBinTimePacking.Controller
{
	/// <summary>
	/// boxes storage, calculatings
	/// </summary>
	public class BoxesStorage
	{
		/// <summary>
		/// array of boxes
		/// </summary>
		private Product[] boxes = null;

		/// <summary>
		/// boxes volume
		/// </summary>
		private Int64 boxesVolume = 0;

		/// <summary>
		/// boxes weight
		/// </summary>
		private Int64 boxesWeight = 0;

		/// <summary>
		/// pointer to Logger
		/// </summary>
		private Logger logger = null;

		/// <summary>
		/// public constructor
		/// </summary>
		/// <param name="newBoxes">boxes for store</param>
		public BoxesStorage(Product[] newBoxes)
		{
			logger = new Logger();
			try
			{
				if (newBoxes == null)
					boxes = new Product[0];
				else
					boxes = newBoxes;
				
				calculateBoxes();
			}
			catch (Exception ex)
			{
				logger.Append("BoxesScheduler: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// calculate boxes weights and volumes
		/// </summary>
		private void calculateBoxes()
		{
			try
			{
				for (int i = 0; i < boxes.Length; i++)
				{
					for (int j = i; j < boxes.Length; j++)
					{
						if (boxes[i].Volume < boxes[j].Volume)
						{
							Product swap = boxes[i];
							boxes[i] = boxes[j];
							boxes[j] = swap;
						}
					}

					boxesVolume += boxes[i].Volume;
					boxesWeight += boxes[i].Weight;

					//orderedBoxes.Add(i, boxes[i]);
				}
			}
			catch (Exception ex)
			{
				logger.Append("calculateBoxesVolume: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// boxes weight
		/// </summary>
		public Int64 BoxesWeight
		{
			get { return boxesWeight; }
		}

		/// <summary>
		/// boxes volume
		/// </summary>
		public Int64 BoxesVolume
		{
			get { return boxesVolume; }
		}

		/// <summary>
		/// boxes
		/// </summary>
		public Product[] Boxes
		{
			get { return boxes; }
		}

		/// <summary>
		/// boxescount
		/// </summary>
		public int Count
		{
			get { return boxes.Length; }
		}
	}
}
