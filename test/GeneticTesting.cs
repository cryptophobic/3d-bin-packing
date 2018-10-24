using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ThreeDimensionalBinTimePacking;

namespace ThreeDimensionalBinTimePacking.Test.GeneticTest
{
	[TestFixture]
	public class GeneticTesting
	{
		Genetic.Genetic genetic = null;

		[SetUp]
		public void PerTestSetUp()
		{
		}

		[Test]
		public void gen()
		{

			DataTypes.Container container = new DataTypes.Container();
			container.Size.Depth = 100;
			container.Size.Height = 100;
			container.Size.Width = 100;
			container.Weight = 100;

			DataTypes.Product[] products = new DataTypes.Product[20];
			for (int i = 0; i < products.Length; i++)
			{
				products[i] = new DataTypes.Product();
				products[i].Size = new DataTypes.Size() { Depth = 100, Width = 50, Height = 10 };
				products[i].Weight = 5;
				products[i].ProdId = "prodid" + i.ToString();
				products[i].Name = "name" + i.ToString();
			}

			genetic = new Genetic.Genetic(products, container);
			int generations = genetic.GenerationCount;
			Assert.That(genetic.BestChromosome, Is.Null, "we haven't best chromosome without any generation");
			Assert.That(genetic.BestSolution, Is.Null, "best solution must be null at start");
			Assert.That(genetic.IsBestExceed, Is.False, "best not exceed yet");

			for (int i = 0; i < generations && !genetic.IsBestExceed; i++)
			{
				genetic.evolution();
				Assert.That(genetic.BestChromosome, Is.InstanceOf(typeof(Genetic.Chromosome)), "must be best chromosome");
				Assert.That(genetic.BestSolution, Is.InstanceOf(typeof(DataTypes.FilledContainer)), "must be best solution");
			}
		}

	}
}
