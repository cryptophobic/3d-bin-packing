using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ThreeDimensionalBinTimePacking;
using ThreeDimensionalBinTimePacking.Test.Algorithm;

namespace ThreeDimensionalBinTimePacking.Test.Data
{
	[TestFixture]
	public class DataTesting
	{
		private Controller.Solution solution;
		private Controller.FilledContainersSet filledContainers;
		private DataTypes.FilledContainer filledContainer;

		[SetUp]
		public void PerTestSetUp()
		{
			solution = new Controller.Solution();
			filledContainers = new Controller.FilledContainersSet(0);
			filledContainer = new DataTypes.FilledContainer();
		}

		[Test]
		public void solutionData()
		{
			
			Assert.That(solution.FreeVolume, Is.EqualTo(-1), "FreeVolume! no solution, no data, but seems like object tries to return some");
			Assert.That(solution.PackedVolume, Is.EqualTo(-1), "PackedVolume! no solution, no data. not zero!");
			Assert.That(solution.ContainersVolume, Is.EqualTo(-1), "ContainersVolume! volume used should be -1, because we have no data, but it is not");
			Assert.That(solution.ContainersCount, Is.EqualTo(0), "ContainersCount! containers used should 0, but it is not. 1 level");
			Assert.That(solution.RemovedBoxes.Length, Is.EqualTo(0), "RemovedBoxes.Length! removed boxes should be 0, but");
			Assert.That(solution.RemovedContainers.Length, Is.EqualTo(0), "RemovedContainers.Length! removed containers should be 0, but");

			emptyContainersSet(solution.Containers);

			Assert.That(solution.RemovedBoxes, Is.InstanceOf(typeof(DataTypes.Product[])), "new solution must contains 0 removed product at least");
			Assert.That(solution.RemovedContainers, Is.InstanceOf(typeof(DataTypes.Container[])), "new solution must contains 0 removed containers at least");

			int last = solution.Containers.addContainer(null);
			Assert.That(last, Is.EqualTo(-1), "Solution.containers supposed to be empty");

			emptyContainersSet(solution.Containers);

			last = solution.Containers.addContainer(filledContainer);
			Assert.That(last, Is.EqualTo(0), "Solution.containers must contains 1 item");
			Assert.That(solution.ContainersCount, Is.EqualTo(1), "Solution.ContainersCount must contains 1 item");

			notEmptyContainersSet(solution.Containers, 1);

		}

		public DataTypes.Container createNewContainer()
		{
			DataTypes.Container container = new DataTypes.Container();
			container.Size.Depth = 10;
			container.Size.Height = 15;
			container.Size.Width = 20;
			container.Weight = 10;
			Assert.That(container.Volume, Is.EqualTo(3000), "10*15*20=3000");
			return container;
		}

		public DataTypes.Product[] createNewProducts(DataTypes.Container container)
		{
			DataTypes.Product[] products = new ThreeDimensionalBinTimePacking.DataTypes.Product[4];
			products[0] = new DataTypes.Product();
			products[0].Size = container.Size;
			products[0].Name = "bestFit";
			products[0].ProdId = "megamod1";
			products[0].Weight = 10;

			products[1] = new DataTypes.Product();
			products[1].Size = container.Size;
			products[1].Size.Depth *= 2;
			products[1].Size.Height *= 2;
			products[1].Size.Width *= 2;
			products[1].Name = "notFit";
			products[1].ProdId = "megamod2";
			products[1].Weight = 10;

			products[2] = new DataTypes.Product();
			products[2].Size = container.Size;
			products[2].Size.Depth /= 5;
			products[2].Size.Height /= 5;
			products[2].Size.Width /= 5;
			products[2].Name = "tooSmall";
			products[2].ProdId = "megamod3";
			products[2].Weight = 10;

			products[3] = new DataTypes.Product();
			products[3].Size = container.Size;
			products[3].Name = "tooHeavy";
			products[3].ProdId = "megamod4";
			products[3].Weight = 15;

			return products;
		}

		public void notEmptyContainersSet(Controller.FilledContainersSet containers, int containersCount)
		{
			Assert.That(containers.Containers, Is.InstanceOf(typeof(DataTypes.FilledContainer[])), "containers set supposed to be not empty, so containers.Containers must be not null");
			Assert.That(containers.Count, Is.EqualTo(containersCount), "containers.Count = containersCount, because we know exactly it");
			Assert.That(containers.Containers.Length, Is.EqualTo(containersCount), "containers.Containers.Length = containersCount, because we know exactly it");
			Assert.That(containers.Size, Is.AtLeast(containersCount), "containers.Size = containersCount, because we know exactly it");
		}

		public void emptyContainersSet(Controller.FilledContainersSet containers)
		{
			Assert.That(containers.Containers, Is.InstanceOf(typeof(DataTypes.FilledContainer[])), "empty containers list should contains array of 0 containers");
			Assert.That(containers.Count, Is.EqualTo(0), "Containers.Count! containers used should 0, but it is not. 2 level");
			Assert.That(containers.Containers.Length, Is.EqualTo(0), "length of empty containers set must be 0");
			Assert.That(containers.FreeVolume, Is.EqualTo(-1), "free volume of empty containers set must be -1");
			Assert.That(containers.PackedCount, Is.EqualTo(-1), "packed count of empty containers set must be -1");
			Assert.That(containers.PackedVolume, Is.EqualTo(-1), "packed volume of empty containers set must be -1");
			Assert.That(containers.Size, Is.EqualTo(0), "size of array without containers must be 0");
			Assert.That(containers.Volume, Is.EqualTo(-1), "sum of volumes of containers in an empty list must be -1");
		}

		[Test]
		public void filledContainersData()
		{
			emptyContainersSet(filledContainers);
			filledContainer.Container = createNewContainer();
			DataTypes.Product[] products = createNewProducts(filledContainer.Container);

			Assert.That(filledContainer.Container.isFit(products[0]), Is.EqualTo(true), "first product best fit");
			Assert.That(filledContainer.Container.isFit(products[1]), Is.EqualTo(false), "second product doesn't fit");
			Assert.That(filledContainer.Container.isFit(products[2]), Is.EqualTo(true), "third product small, but fit");
			Assert.That(filledContainer.Container.isFit(products[3]), Is.EqualTo(false), "fourth product too heavy");
		}

		[Test]
		public void filledContainerData()
		{

			Assert.That(filledContainer.Container, Is.Null, "container and products of a new filledContainer must be null");
			Assert.That(filledContainer.FreeVolume, Is.EqualTo(-1), "FreeVolume, PackedCount and PackedVolume of a new filledContainer must be -1");
			Assert.That(filledContainer.PackedCount, Is.EqualTo(-1), "FreeVolume, PackedCount and PackedVolume of a new filledContainer must be -1");
			Assert.That(filledContainer.PackedVolume, Is.EqualTo(-1), "FreeVolume, PackedCount and PackedVolume of a new filledContainer must be -1");
			Assert.That(filledContainer.Products, Is.Null, "container and products of a new filledContainer must be null");

		}
	}
}