using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BinTime.Logger;

namespace ThreeDimensionalBinTimePacking.DataTypes
{

	/// <summary>
	/// class for storing container full of boxes
	/// </summary>
	public class FilledContainer 
	{
		/// <summary>
		/// container
		/// </summary>
		private Container container;

		/// <summary>
		/// packed voulme
		/// </summary>
		private Int64 packedVolume = -1;

		/// <summary>
		/// free volume
		/// </summary>
		private Int64 freeVolume = -1;

		/// <summary>
		/// array of packed boxes
		/// </summary>
		private Pack[] products_arr;

		/// <summary>
		/// collection of packed boxes
		/// </summary>
		private Dictionary<int, Pack> products_col;

		/// <summary>
		/// public constructor
		/// </summary>
		public FilledContainer()
		{
			container = null;
			products_arr = null;
			products_col = null;
		}

		/// <summary>
		/// count of packeed boxes
		/// </summary>
		public int PackedCount
		{
			get {
				if (products_arr == null)
					return -1;
				return products_arr.Length; 
			}
		}

		/// <summary>
		/// total packed volume
		/// </summary>
		public Int64 PackedVolume
		{
			get
			{
				if (packedVolume == 0)
				{
					packedVolume = 0;
					foreach (Pack product in products_arr)
					{
						packedVolume += product.Product.Volume;
					}
				}
				return packedVolume;
			}
		}

		/// <summary>
		/// free volume
		/// </summary>
		public Int64 FreeVolume
		{
			get
			{
				if (freeVolume == -1 && container != null && PackedVolume > 0)
				{
					freeVolume = container.Volume - PackedVolume;
				}
				return freeVolume;
			}
		}

		/// <summary>
		/// collection of packed boxes
		/// </summary>
		public Dictionary<int, Pack> Products_col
		{
			set
			{
				if (value == null)
					products_col = new Dictionary<int, Pack>();
				else
					products_col = value;
				freeVolume = -1;
			}
			get { return products_col; }
		}

		/// <summary>
		/// array of packed boxes
		/// </summary>
		public Pack[] Products
		{
			set
			{
				if (value == null)
					products_arr = new Pack[0];
				else
					products_arr = value; 
				freeVolume = -1;
			}
			get { return products_arr; }
		}

		/// <summary>
		/// volume of container
		/// </summary>
		public Int64 Volume
		{
			get { return Container.Volume; }
		}

		/// <summary>
		/// weight of container
		/// </summary>
		public Int64 Weight
		{
			get { return Container.Weight; }
		}

		/// <summary>
		/// size of container
		/// </summary>
		public Size Size
		{
			get { return Container.Size; }
		}

		/// <summary>
		/// container
		/// </summary>
		public Container Container
		{
			set
			{
				container = value;
				if (container == null)
				{
					freeVolume = -1;
					packedVolume = -1;
					products_arr = null;
					products_col = null;
				}
				else
				{
					freeVolume = -1;
					packedVolume = 0;
					products_arr = new Pack[0];
					products_col = new Dictionary<int, Pack>();
				}
			}
			get { return container; }
		}
	}

	/// <summary>
	/// stores sizes of containers and boxes
	/// </summary>
	public class Size
	{

		/// <summary>
		/// width
		/// </summary>
		private int width;

		/// <summary>
		/// depth
		/// </summary>
		private int depth;

		/// <summary>
		/// height
		/// </summary>
		private int height;

		/// <summary>
		/// volume
		/// </summary>
		private Int64 volume = -1;

		/// <summary>
		/// accessor to volume
		/// </summary>
		public Int64 Volume
		{
			get
			{
				if (volume == -1) { volume = depth * height * width; }
				return volume;
			}
		}

		/// <summary>
		/// acccessor to width
		/// </summary>
		public int Width
		{
			set { width = value; volume = -1; }
			get { return width; }
		}

		/// <summary>
		/// accessor to height
		/// </summary>
		public int Height
		{
			set { height = value; volume = -1; }
			get { return height; }
		}

		/// <summary>
		/// accessor to depth
		/// </summary>
		public int Depth
		{
			set { depth = value; volume = -1; }
			get { return depth; }
		}

		/// <summary>
		/// overrided method ToString
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			return "width=" + width + ", height=" + height + ", depth=" + depth;
		}
	}

	/// <summary>
	/// stores Axis of boxes and containers
	/// </summary>
	public class Axis
	{
		/// <summary>
		/// x
		/// </summary>
		private int x;

		/// <summary>
		/// y
		/// </summary>
		private int y;

		/// <summary>
		/// z
		/// </summary>
		private int z;

		/// <summary>
		/// public constructor
		/// </summary>
		public Axis()
		{
			x = -1; 
			y = -1; 
			z = -1;
		}

		/// <summary>
		/// accessor and mutator to X
		/// </summary>
		public int X
		{
			set { x = value; }
			get { return x; }
		}

		/// <summary>
		/// accessor and mutator to Y
		/// </summary>
		public int Y
		{
			set { y = value; }
			get { return y; }
		}

		/// <summary>
		/// accessor and mutator to Z
		/// </summary>
		public int Z
		{
			set { z = value; }
			get { return z; }
		}

		/// <summary>
		/// overrided method ToString
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			return "x=" + x.ToString() + ", y=" + y.ToString() + ", z=" + z.ToString();
		}
	}

	/// <summary>
	/// stores container parameters
	/// </summary>
	public class Container : BigAndHeavy
	{
		/// <summary>
		/// pointer to Logger
		/// </summary>
		protected Logger logger = new Logger();

		/// <summary>
		/// is this container fits specified box
		/// </summary>
		/// <param name="box">specified box</param>
		/// <returns>true/false fits or not</returns>
		public bool isFit(Product box)
		{
			try
			{
				if (box.Weight > Weight) return false;
				Size boxSize = box.Size;
				for (int i = 0; i <= 5; i++)
				{
					if (i > 0)
					{
						if (i % 2 == 0)
						{
							int swap = boxSize.Width;
							boxSize.Width = boxSize.Height;
							boxSize.Height = swap;
						}
						else
						{
							int swap = boxSize.Depth;
							boxSize.Depth = boxSize.Width;
							boxSize.Width = swap;
						}
					}

					if (possibleToPut(boxSize, new Axis() { X = 0, Y = 0, Z = 0 }) == false) continue;

					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				logger.Append("isFit: " + ex.Message, Logger.ERROR);
			}
			return true;
		}

		/// <summary>
		/// is possible to put box with specified orientation to specified place
		/// </summary>
		/// <param name="boxSize">size</param>
		/// <param name="dot">coordinates</param>
		/// <returns>true/false possible or not</returns>
		protected bool possibleToPut(Size boxSize, Axis dot)
		{
			try
			{
				if (dot.X + boxSize.Width > Size.Width)
					return false;
				if (dot.Y + boxSize.Height > Size.Height)
					return false;
				if (dot.Z + boxSize.Depth > Size.Depth)
					return false;

				return true;
			}
			catch (Exception ex)
			{
				logger.Append("possibleToPut:" + ex.Message, Logger.ERROR);
			}
			return false;
		}

		/// <summary>
		/// overrided method ToString
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			return "weight=" + Weight.ToString() + ", " + Size.ToString();
		}
	}

	/// <summary>
	/// stores info about product
	/// </summary>
	public class Product : BigAndHeavy
	{
		/// <summary>
		/// prod id
		/// </summary>
		private string prodid;

		/// <summary>
		/// name
		/// </summary>
		private string name;

		/// <summary>
		/// accessor and mutator for prod id
		/// </summary>
		public string ProdId
		{
			set { prodid = value; }
			get { return prodid; }
		}

		/// <summary>
		/// accessor and mutator for name
		/// </summary>
		public string Name
		{
			set { name = value; }
			get { return name; }
		}

		/// <summary>
		/// overrided method ToString
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			return name + " (" + prodid + "), weight=" + Weight.ToString() + ", " + Size.ToString();
		}
	}

	/// <summary>
	/// stores info about packed product
	/// </summary>
	public class Pack
	{
		/// <summary>
		/// product
		/// </summary>
		private Product product;

		/// <summary>
		/// coordinates of place where it packed
		/// </summary>
		private Axis axis;

		/// <summary>
		/// public constructor
		/// </summary>
		public Pack()
		{
			product = new Product();
			axis = new Axis();
		}

		/// <summary>
		/// size of product
		/// </summary>
		public Size Size
		{
			get { return Product.Size; }
		}

		/// <summary>
		/// volume of product
		/// </summary>
		public Int64 Volume
		{
			get { return Product.Volume; }
		}

		/// <summary>
		/// weight of product
		/// </summary>
		public Int64 Weight
		{
			get { return Product.Weight; }
		}

		/// <summary>
		/// name of product
		/// </summary>
		public string Name
		{
			get { return Product.Name; }
		}

		/// <summary>
		/// prod id of product
		/// </summary>
		public string ProdId
		{
			get { return Product.ProdId; }
		}

		/// <summary>
		/// accessor for product
		/// </summary>
		public Product Product
		{
			get { return product; }
			set
			{
				product.Name = value.Name;
				product.ProdId = value.ProdId;
				product.Size = value.Size;
				product.Weight = value.Weight;
			}
		}

		/// <summary>
		/// accessor for coordinates of place where it packed
		/// </summary>
		public Axis Axis
		{
			get { return axis; }
			set
			{
				axis.X = value.X;
				axis.Y = value.Y;
				axis.Z = value.Z;
			}
		}

		/// <summary>
		/// overrided method ToString
		/// </summary>
		/// <returns>string</returns>
		public override string ToString()
		{
			return axis.ToString();
		}
	}
}