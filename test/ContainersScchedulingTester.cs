using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ThreeDimensionalBinTimePacking;
using BinTime.Logger;
using System.Configuration;

namespace ThreeDimensionalBinTimePacking.Test.ContainersScchedulingTester
{
	[TestFixture]
	class ContainersScchedulingTesting
	{
		Logger logger = null;

		[SetUp]
		public void PerTestSetUp()
		{
			logger = new Logger();
		}

		[Test]
		public void schedulerTestWithNullValues()
		{
			Controller.BoxesStorage bs = new Controller.BoxesStorage(null);
			Assert.That(bs.Boxes, Is.InstanceOf(typeof(DataTypes.Product[])));
			Assert.That(bs.BoxesVolume, Is.EqualTo(0));
			Assert.That(bs.BoxesWeight, Is.EqualTo(0));
			Assert.That(bs.Count, Is.EqualTo(0));

			Controller.ContainersStorage cs = new Controller.ContainersStorage(null);
			Assert.That(cs.Containers, Is.InstanceOf(typeof(DataTypes.Container[])));
			Assert.That(cs.Containers.Length, Is.EqualTo(0));

			Controller.ContainersBoxesConsistence cbc = new Controller.ContainersBoxesConsistence(null, null);
			Assert.That(cbc.Boxes, Is.InstanceOf(typeof(DataTypes.Product[])));
			Assert.That(cbc.Containers, Is.InstanceOf(typeof(DataTypes.Container[])));
			Assert.That(cbc.RemovedBoxes, Is.InstanceOf(typeof(DataTypes.Product[])));
			Assert.That(cbc.RemovedContainers, Is.InstanceOf(typeof(DataTypes.Container[])));
		}

		private DataTypes.Container[] getKnownContainers()
		{
			DataTypes.Container[] containers = null;

			containers = new DataTypes.Container[3];
			containers[0] = new DataTypes.Container();
			containers[0].Size.Depth = 1000;
			containers[0].Size.Height = 500;
			containers[0].Size.Width = 500;
			containers[0].Weight = 101200;

			containers[1] = new DataTypes.Container();
			containers[1].Size.Depth = 500;
			containers[1].Size.Height = 250;
			containers[1].Size.Width = 250;
			containers[1].Weight = 101200;

			containers[2] = new DataTypes.Container();
			containers[2].Size.Depth = 500;
			containers[2].Size.Height = 500;
			containers[2].Size.Width = 500;
			containers[2].Weight = 101200;

			return containers;
		}

		private DataTypes.Product[] getKnownBoxes()
		{
			DataTypes.Product[] products = new DataTypes.Product[23];

			for (int i = 0; i < products.Length; i++)
			{
				products[i] = new DataTypes.Product();
				products[i].Name = "ThinkPad T400";
				products[i].ProdId = "NM352MH";
				products[i].Size.Depth = 115;
				products[i].Size.Height = 320;
				products[i].Size.Width = 480;
				products[i].Weight = 4400;
			}
			return products;
		}

		[Test]
		public void schedulerMultipleRealTest()
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
						swap[currBoxes.Length] = new DataTypes.Product()
						{
							Name = name,
							ProdId = prodid,
							Size = new DataTypes.Size() { Depth = depth, Height = height, Width = width },
							Weight = weight
						};
						packages[packageNum] = swap;
					}
					else
					{
						packages.Add(packageNum, new DataTypes.Product[1] { 
							new DataTypes.Product() { Name = name, ProdId = prodid, Size = new DataTypes.Size() { Depth = depth, Height = height, Width = width }}
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
				logger.Append("------------------------------------------------------------------------------------");
				logger.Append("Started: package No. " + kvp.Key.ToString() + " boxes count=" + kvp.Value.Length.ToString());

				Controller.BoxesStorage bs = new Controller.BoxesStorage(kvp.Value);

				DataTypes.Container[] newContainers = new DataTypes.Container[cns.Length];
				cns.CopyTo(newContainers, 0);

				Controller.ContainersStorage cs = new Controller.ContainersStorage(newContainers);

				Assert.That(bs.Boxes, Is.Not.Null);
				Assert.That(cs.Containers, Is.Not.Null);

				Controller.ContainersBoxesConsistence cbc = new Controller.ContainersBoxesConsistence(cs.Containers, bs.Boxes);
				if (cbc.BoxesCount <= 40) {
					if (cbc.BoxesCount > 0 && cbc.ContainersCount > 0)
					{

						Controller.Packer packer = new Controller.Packer();
						Controller.ContainersScheduler containersScheduler = new Controller.ContainersScheduler(cbc.BoxesWeight, cbc.BoxesVolume, cbc.ContainersLimit, cbc.Containers, cbc.BoxesCount);
						Controller.FilledContainersSet filledContainersSet = null;
						Controller.GroupedContainersSet[] groupedContainersSets = containersScheduler.getNextSolution();

						logger.Append("trying solution:");

						while (filledContainersSet == null && groupedContainersSets != null)
						{

							foreach (Controller.GroupedContainersSet groupedContainerSet in groupedContainersSets)
							{
								logger.Append(groupedContainerSet.ToString());
							}
							filledContainersSet = packer.pack(groupedContainersSets, cbc.Boxes);
							if (filledContainersSet == null)
							{
								logger.Append("rejected :( trying next solution:");
								groupedContainersSets = containersScheduler.getNextSolution();
							}
							else
							{
								logger.Append("accepted :)");
							}
						}
						Controller.Solution solution = new Controller.Solution();
						solution.Containers = filledContainersSet;
						solution.RemovedBoxes = cbc.RemovedBoxes;
						solution.RemovedContainers = cbc.RemovedContainers;
						logger.Append(solution.ToString());
						Assert.That(solution, Is.InstanceOf(typeof(Controller.Solution)));
					}
					else
					{
						logger.Append("skipped for test, because too much boxes");
					}
				}
			}
		}

		[Test]
		public void schedulerSingleKnownTest()
		{

			DataTypes.Product[] products = getKnownBoxes();

			logger.Append("products are: ");
			foreach (DataTypes.Product product in products)
			{
				logger.Append(product.ToString());
			}

			DataTypes.Container[] containers = getKnownContainers();

			logger.Append("containers are: ");
			foreach (DataTypes.Container container in containers)
			{
				logger.Append(container.ToString());
			}

			Controller.BoxesStorage bs = new Controller.BoxesStorage(products);
			Assert.That(bs.Boxes, Is.InstanceOf(typeof(DataTypes.Product[])));
			Assert.That(bs.BoxesVolume, Is.EqualTo(406272000));
			Assert.That(bs.BoxesWeight, Is.EqualTo(101200));
			Assert.That(bs.Count, Is.EqualTo(23));

			Controller.ContainersStorage cs = new Controller.ContainersStorage(containers);
			Assert.That(cs.Containers, Is.InstanceOf(typeof(DataTypes.Container[])));
			Assert.That(cs.Containers.Length, Is.EqualTo(3));

			Controller.ContainersBoxesConsistence cbc = new Controller.ContainersBoxesConsistence(cs.Containers, bs.Boxes);
			Assert.That(cbc.Boxes, Is.InstanceOf(typeof(DataTypes.Product[])));
			Assert.That(cbc.Containers, Is.InstanceOf(typeof(DataTypes.Container[])));
			Assert.That(cbc.RemovedBoxes, Is.InstanceOf(typeof(DataTypes.Product[])));
			Assert.That(cbc.RemovedContainers, Is.InstanceOf(typeof(DataTypes.Container[])));

			Controller.Packer packer = new Controller.Packer();
			
			Controller.ContainersScheduler containersScheduler = new Controller.ContainersScheduler(cbc.BoxesWeight, cbc.BoxesVolume, cbc.ContainersLimit, cbc.Containers, cbc.BoxesCount);

			Controller.FilledContainersSet filledContainersSet = null;
			Controller.GroupedContainersSet[] groupedContainersSets = containersScheduler.getNextSolution();

			logger.Append("trying solution:");

			while (filledContainersSet == null && groupedContainersSets != null)
			{

				foreach (Controller.GroupedContainersSet groupedContainerSet in groupedContainersSets)
				{
					logger.Append(groupedContainerSet.ToString());
				}
				filledContainersSet = packer.pack(groupedContainersSets, cbc.Boxes);
				if (filledContainersSet == null)
				{
					logger.Append("rejected :( trying next solution:");
					groupedContainersSets = containersScheduler.getNextSolution();
				}
				else
				{
					logger.Append("accepted :)");
				}
			}

			Controller.Solution solution = new Controller.Solution();
			solution.Containers = filledContainersSet;
			solution.RemovedBoxes = cbc.RemovedBoxes;
			solution.RemovedContainers = cbc.RemovedContainers;
			logger.Append(solution.ToString());

		}
	}
}
