using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ThreeDimensionalBinTimePacking;


namespace ThreeDimensionalBinTimePacking.Test.Algorithm
{
	[TestFixture]
	public class AlgorithmTesting
	{
		private SinglePack.ProcessContainer container;
		private SinglePack.ExtremePoint extremePoint;
		private SinglePack.List list;

		[SetUp]
		public void PerTestSetUp()
		{
			DataTypes.Size size = new DataTypes.Size();
			size.Depth = 100;
			size.Height = 100;
			size.Width = 100;

			container = new SinglePack.ProcessContainer(size, 100);

			Assert.That(container.Volume, Is.EqualTo(1000000), "100*100*100=1000000");
			Assert.That(container.Weight, Is.EqualTo(100), "weight=100");

			extremePoint = new SinglePack.ExtremePoint();
			list = new SinglePack.List();
		}

		public DataTypes.Product[] getProducts()
		{
			DataTypes.Product[] products = new DataTypes.Product[20];
			for (int i = 0; i < products.Length; i++)
			{
				products[i] = new DataTypes.Product();
				products[i].Size = new DataTypes.Size() { Depth = 100, Width = 50, Height = 10 };
				products[i].Weight = 5;
				products[i].ProdId = "prodid" + i.ToString();
				products[i].Name = "name" + i.ToString();
			}
			return products;
		}

		[Test]
		public void processContainer()
		{
			Assert.That(container.NotPacked, Is.InstanceOf(typeof(Dictionary<int, DataTypes.Size>)), "new container must contains zero collection of not packed boxes");
			Assert.That(container.NotPackedCount, Is.EqualTo(0), "new container 0 not packed boxes");

			Assert.That(container.Packed, Is.InstanceOf(typeof(Dictionary<int, DataTypes.Pack>)), "new container must contains zero collection of packed boxes");
			Assert.That(container.PackedCount, Is.EqualTo(0), "new container 0 packed boxes, 0 packed weight, 0 packed volume");

			Assert.That(container.PackedVolume, Is.EqualTo(0), "new container 0 packed boxes, 0 packed weight, 0 packed volume");
			Assert.That(container.PackedWeight, Is.EqualTo(0), "new container 0 packed boxes, 0 packed weight, 0 packed volume");
			Assert.That(container.RemainingVolume, Is.EqualTo(container.Volume), "new container remaining volume is equals to volume");

			DataTypes.Product[] products = getProducts();
			for (int i = 0; i < products.Length; i++)
			{
				Assert.That(container.insertBox(i, products[i], products[i].Size), Is.InstanceOf(typeof(DataTypes.Axis)),"all boxes stands directly, must place all");
			}

			Assert.That(container.NotPackedCount, Is.EqualTo(0), "all of boxes must be packed");
			Assert.That(container.PackedCount, Is.EqualTo(products.Length), "all of boxes must be packed");

			Assert.That(container.PackedVolume, Is.EqualTo(container.Volume), "total boxes volume must be equals to container volume");
			Assert.That(container.PackedWeight, Is.EqualTo(container.Weight), "total boxes volume must be equals to container volume");
			Assert.That(container.RemainingVolume, Is.EqualTo(0), "there is no place");

		}

		[Test]
		public void listTest()
		{
			Assert.That(list.getFirst(), Is.Null, "empty list, first item must be null");
			Assert.That(list.getNext(), Is.Null, "empty list, next item must be null");

			DataTypes.Product[] products = getProducts();

			int i = 0;

			foreach (DataTypes.Product product in products)
			{
				i++;
				list.add(i, new DataTypes.Axis() { X = i, Y = i, Z = i });
			}

			for (i = 0; i < products.Length; i++)
			{
				if (i == 0)
					Assert.That(list.getFirst(), Is.InstanceOf(typeof(SinglePack.ExtremePoint)), "item No." + (i + 1).ToString() + ", must be not null");
				else
					Assert.That(list.getNext(), Is.InstanceOf(typeof(SinglePack.ExtremePoint)), "item No." + (i + 1).ToString() + ", must be not null");
			}

			Assert.That(list.getNext(), Is.Null, "item No. 21 must be null");

			i = 0;

			foreach (DataTypes.Product product in products)
			{
				i++;
				list.remove(new DataTypes.Axis() { X = i, Y = i, Z = i });
			}

			Assert.That(list.getFirst(), Is.Null, "empty list, first item must be null");
			Assert.That(list.getNext(), Is.Null, "empty list, next item must be null");

		}

	}
}
