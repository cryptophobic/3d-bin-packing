using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using BinTime.Logger;
using ThreeDimensionalBinTimePacking.DataTypes;
using ThreeDimensionalBinTimePacking.Controller;

using System.Collections.Generic;
using System.Globalization;


namespace PackingService
{
	public class DataAccess
	{
		private static Double k = 1000;
		private static Double k3 = k * k * k;

		private static Logger logger = new Logger();

		public static Solution findSolution(BoxesArgument[] boxesString, ContainersArgument[] containersString)
		{
			Solution result = null;
			try
			{
				result = new Solution();
				Container[] cns = parseContainers(containersString);
				if (cns == null) return result;
				Product[] bxs = parseBoxes(boxesString);
				if (bxs == null) return result;

				Controller cs = new Controller(cns, bxs);

				ThreeDimensionalBinTimePacking.Controller.Solution sol = cs.getSolution();

				result.containers = sol.ContainersCount.ToString();
				result.containersVolume = (sol.ContainersVolume / k3).ToString();
				result.freeVolume = (sol.FreeVolume / k3).ToString();
				result.packedVolume = (sol.PackedVolume / k3).ToString();

				Product[] removedBoxes = sol.RemovedBoxes;
				Container[] removedContainers = sol.RemovedContainers;

				result.removedBoxes = new productParams[removedBoxes.Length];
				result.removedContainers = new containerParams[removedContainers.Length];

				for (int i = 0; i < removedBoxes.Length; i++)
				{
					result.removedBoxes[i] = new productParams()
					{
						depth = (removedBoxes[i].Size.Depth / k).ToString(),
						height = (removedBoxes[i].Size.Height / k).ToString(),
						width = (removedBoxes[i].Size.Width / k).ToString(),
						name = removedBoxes[i].Name,
						prodid = removedBoxes[i].ProdId,
						weight = (removedBoxes[i].Weight / k).ToString()
					};
				}

				for (int i = 0; i < removedContainers.Length; i++)
				{
					result.removedContainers[i] = new containerParams()
					{
						depth = (removedContainers[i].Size.Depth / k).ToString(),
						height = (removedContainers[i].Size.Height / k).ToString(),
						width = (removedContainers[i].Size.Width / k).ToString(),
						volume = (removedContainers[i].Volume / k3).ToString(),
						weight = (removedContainers[i].Weight / k).ToString()
					};
				}


				result.container = new ResultedContainers[sol.ContainersCount];

				for (int i = 0; i < sol.ContainersCount; i++)
				{
					FilledContainer currContainer = sol.Containers.Containers[i];

					result.container[i] = new ResultedContainers();
					result.container[i].containerParams = new containerParams();
					result.container[i].No = (i + 1).ToString();
					result.container[i].containerParams.depth = (currContainer.Size.Depth / k).ToString();
					result.container[i].containerParams.height = (currContainer.Size.Height / k).ToString();
					result.container[i].containerParams.width = (currContainer.Size.Width / k).ToString();
					result.container[i].containerParams.weight = (currContainer.Weight / k).ToString();

					result.container[i].packedBoxes = currContainer.PackedCount.ToString();
					result.container[i].packedVolume = (currContainer.PackedVolume / k3).ToString();
					result.container[i].freeVolume = (currContainer.FreeVolume / k3).ToString();
					result.container[i].containerParams.volume = (currContainer.Volume / k3).ToString();

					result.container[i].product = new packedProducts[currContainer.PackedCount];

					Pack[] products = currContainer.Products;

					int j = 0;

					foreach (Pack product in products)
					{
						result.container[i].product[j] = new packedProducts();
						result.container[i].product[j].productParams = new productParams();

						result.container[i].product[j].productParams.depth = (product.Size.Depth / k).ToString();
						result.container[i].product[j].productParams.width = (product.Size.Width / k).ToString();
						result.container[i].product[j].productParams.height = (product.Size.Height / k).ToString();

						result.container[i].product[j].productParams.weight = (product.Weight / k).ToString();

						result.container[i].product[j].productParams.name = product.Name;
						result.container[i].product[j].productParams.prodid = product.ProdId;

						result.container[i].product[j].x = (product.Axis.X / k).ToString();
						result.container[i].product[j].y = (product.Axis.Y / k).ToString();
						result.container[i].product[j].z = (product.Axis.Z / k).ToString();

						j++;
					}
				}
			}
			catch (Exception ex)
			{
				logger.Append("findSolution: " + ex.Message, Logger.ERROR);
			}
			return result;
		}

		private static Container[] parseContainers(ContainersArgument[] containersArguments)
		{
			try
			{
				Container[] containers = new Container[containersArguments.Length];

				for (int i = 0; i < containersArguments.Length; i++)
				{

					containers[i] = new Container();
					containers[i].Size = new Size()
						{
							Depth = (int)(Convert.ToDouble(containersArguments[i].depth.Replace('.', ',')) * k),
							Width = (int)(Convert.ToDouble(containersArguments[i].width.Replace('.', ',')) * k),
							Height = (int)(Convert.ToDouble(containersArguments[i].height.Replace('.', ',')) * k)
						};
					containers[i].Weight = (int)(Convert.ToDouble(containersArguments[i].weight.Replace('.', ',')) * k);

				}

				return containers;
			}
			catch (Exception ex)
			{
				logger.Append("parseContainers: " + ex.Message, Logger.ERROR);
			}
			return null;
		}

		private static Product[] parseBoxes(BoxesArgument[] boxesArguments)
		{
			try
			{
				Product[] products = new Product[boxesArguments.Length];

				for (int i = 0; i < boxesArguments.Length; i++)
				{
					products[i] = new Product()
					{
						Name = boxesArguments[i].name,
						ProdId = boxesArguments[i].prodid,
						Size = new Size()
						{
							Depth = (int)(Convert.ToDouble(boxesArguments[i].depth.Replace('.', ',')) * k),
							Height = (int)(Convert.ToDouble(boxesArguments[i].height.Replace('.', ',')) * k),
							Width = (int)(Convert.ToDouble(boxesArguments[i].width.Replace('.', ',')) * k)
						},
						Weight = (int)(Convert.ToDouble(boxesArguments[i].weight.Replace('.', ',')) * k)
					};
				}

				return products;
			}
			catch (Exception ex)
			{
				logger.Append("parseBoxes: " + ex.Message, Logger.ERROR);
			}
			return null;
		}



	}
}
