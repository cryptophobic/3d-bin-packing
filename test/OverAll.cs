using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ThreeDimensionalBinTimePacking;
using BinTime.Logger;
using System.Configuration;

namespace ThreeDimensionalBinTimePacking.Test.OverAll
{

	[TestFixture]
	class OverAll
	{
		Logger logger = null;

		[SetUp]
		public void PerTestSetUp()
		{
			logger = new Logger();
		}

		[Test]
		public void go()
		{

			System.IO.StreamReader fr = new System.IO.StreamReader(ConfigurationSettings.AppSettings["testFile"]);
			string csv = fr.ReadToEnd();

			string[] rows = csv.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

			Dictionary<int, DataTypes.Product[]> packages = new Dictionary<int, DataTypes.Product[]>();

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
				Int64 weight = (Int64)(Convert.ToDouble(columns[8].Replace("\"", "").Replace(".", ",")) * 1000);

				do
				{
					if (packages.ContainsKey(packageNum))
					{
						DataTypes.Product[] currBoxes = packages[packageNum];
						DataTypes.Product[] swap = new DataTypes.Product[currBoxes.Length + 1];
						currBoxes.CopyTo(swap, 0);
						swap[currBoxes.Length] = new DataTypes.Product() { Name = name, ProdId = prodid, Size = new DataTypes.Size() { Depth = depth, Height = height, Width = width } };
						packages[packageNum] = swap;
					}
					else
					{
						packages.Add(packageNum, new DataTypes.Product[1] 
						{ 
							new DataTypes.Product() 
							{ 
								Name = name, 
								ProdId = prodid, 
								Size = new DataTypes.Size() { Depth = depth, Height = height, Width = width },
								Weight = weight
							}
						});
					}
					count--;
				} while (count > 0);
			}

			DataTypes.Container[] cns = new DataTypes.Container[] {
					new DataTypes.Container () { Size = new DataTypes.Size() {Depth = 1000, Width = 500, Height = 500}, Weight = 1000000},
					new DataTypes.Container () { Size = new DataTypes.Size() {Depth = 500, Width = 250, Height = 250}, Weight =	1000000},
					new DataTypes.Container () { Size = new DataTypes.Size() {Depth = 500, Width = 500, Height = 500}, Weight = 1000000}
			};

			logger.Append(cns.Length + " types of containers");
			foreach (DataTypes.Container cont in cns)
			{
				logger.Append(cont.Size.ToString());
			}

			logger.Append(packages.Count.ToString() + " packages");

			foreach (KeyValuePair<int, DataTypes.Product[]> kvp in packages)
			{
				if (kvp.Value.Length <= 40)
				{
					logger.Append("------------------------------------------------------------------------------------");
					logger.Append("Started: package No. " + kvp.Key.ToString() + " boxes count=" + kvp.Value.Length.ToString());

					DataTypes.Container[] newContainers = new DataTypes.Container[cns.Length];
					cns.CopyTo(newContainers, 0);

					Controller.Controller cs = new Controller.Controller(newContainers, kvp.Value);
					Controller.Solution sol = cs.getSolution();
					Assert.That(sol, Is.InstanceOf(typeof(Controller.Solution)));

					logger.Append(sol.ToString());
				}
			}
		}
	}
}
