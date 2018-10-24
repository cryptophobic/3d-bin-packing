using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThreeDimensionalBinTimePacking.DataTypes;
using ThreeDimensionalBinTimePacking.Controller;
using BinTime.Logger;


namespace packingConsole
{
	class Program
	{
		private static Logger logger = null;

		static void Main(string[] args)
		{
			logger = new Logger("d:\\Log.txt");
			logger.clear();

			try
			{

				System.IO.StreamReader fr = new System.IO.StreamReader("d:\\shipmentlines1000.csv");
				string csv = fr.ReadToEnd();

				string[] rows = csv.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

				Dictionary<int, Product[]> packages = new Dictionary<int, Product[]>();
				for (int i = 0; i < rows.Length; i++)
				{
					string[] columns = rows[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
					int packageNum = Convert.ToInt32(columns[0].Replace("\"", ""));
					string prodid = columns[2].Replace("\"", "");
					string name = columns[3].Replace("\"", "");
					int count = Convert.ToInt32(columns[4].Replace("\"", ""));
					int width = (int)(Convert.ToDouble(columns[5].Replace("\"", "").Replace(".", ",")) * 1000);
					int depth = (int)(Convert.ToDouble(columns[6].Replace("\"", "").Replace(".", ",")) * 1000);
					int height = (int)(Convert.ToDouble(columns[7].Replace("\"", "").Replace(".", ",")) * 1000);
					int weight = (int)(Convert.ToDouble(columns[8].Replace("\"", "").Replace(".", ",")) * 1000);

					do
					{
						if (packages.ContainsKey(packageNum))
						{
							Product[] currBoxes = packages[packageNum];
							Product[] swap = new Product[currBoxes.Length + 1];
							currBoxes.CopyTo(swap, 0);
							swap[currBoxes.Length] = new Product() { Name = name, ProdId = prodid, Size = new Size() { Depth = depth, Height = height, Width = width } };
							packages[packageNum] = swap;
						}
						else
						{
							packages.Add(packageNum, new Product[1] { 
							new Product() { Name = name, ProdId = prodid, Size = new Size() { Depth = depth, Height = height, Width = width }}
						});
						}
						count--;
					} while (count > 0);
				}

				Container[] cns = new Container[] {
					new Container () { Size = new Size() {Depth = 1000, Width = 500, Height = 500}, Weight = 10},
					new Container () { Size = new Size() {Depth = 500, Width = 250, Height = 250}, Weight =	10},
					new Container () { Size = new Size() {Depth = 500, Width = 500, Height = 500}, Weight = 10}
				};

				logger.Append(cns.Length + " types of containers");
				foreach (Container cont in cns)
				{
					logger.Append(cont.Size.ToString());
				}

				logger.Append(packages.Count.ToString() + " packages");

				foreach (KeyValuePair<int, Product[]> kvp in packages)
				{
					logger.Append("------------------------------------------------------------------------------------");
					logger.Append("Started: package No. " + kvp.Key.ToString() + " boxes count=" + kvp.Value.Length.ToString());
					if (kvp.Value.Length >= 40)
					{
						logger.Append("skipped for now. sorry :(");
						continue;
					}

					/*if (kvp.Key == 600807)
					{
						int a = 8;
					}*/

					Container[] newContainers = new Container[cns.Length];
					cns.CopyTo(newContainers, 0);

					Controller cs = new Controller(newContainers, kvp.Value);

					Solution sol = cs.getSolution();


					logger.Append("finished: ");

					logger.Append(sol.ToString());



				}
			}
			catch (Exception ex)
			{
				logger.Append("main:" + ex.Message, Logger.ERROR);
			}
			
		}
	}
}
