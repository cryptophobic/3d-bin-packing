using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BinTime.Logger;
using ThreeDimensionalBinTimePacking.DataTypes;
using ThreeDimensionalBinTimePacking.SinglePack;

using System.Threading;

namespace ThreeDimensionalBinTimePacking.Genetic
{
	/// <summary>
	/// implements genetic algorith of 3d bin packing task with variable orientations and 1 container
	/// </summary>
	/// <remarks>
	/// If one box have a six orientaions n=6 and we have m boxes, so gene is pair of i:j where i is a random number 
	/// from 1 to n, j is a number from 1 to m and we have m genes in 1 chromosome. 
	/// All chromosomes differs from each other just by orientations of boxes (i)
	/// </remarks>
	public class Genetic
	{

		/// <summary>
		/// set of boxes
		/// </summary>
		private Dictionary<int, Product> bioMass = null;

		/// <summary>
		/// count of generations
		/// </summary>
		private int generationsCount = 0;

		/// <summary>
		/// bumber of current generation
		/// </summary>
		private int generationNumber = 0;

		/// <summary>
		/// count of chromosomes in 1 generation
		/// </summary>
		private int population = 0;

		/// <summary>
		/// chromosomes
		/// </summary>
		private Chromosome[] Chromosomes = null;

		/// <summary>
		/// pointer to Logger
		/// </summary>
		private Logger logger = null;

		/// <summary>
		/// current generation (set of filled containers)
		/// </summary>
		private ProcessContainer[] Generation = null;

		/// <summary>
		/// pointer to (filled) container 
		/// </summary>
		private ProcessContainer container = null;

		/// <summary>
		/// flag to turn to off/on threading (not tested properly, sorry :( )
		/// </summary>
		private bool threading = false;

		/// <summary>
		/// count of population which must be killed in next generation
		/// </summary>
		private int terminated = 0;

		/// <summary>
		/// flag to determine if we found nice solution
		/// </summary>
		private bool isBest = false;

		/// <summary>
		/// pointer to Random object
		/// </summary>
		private Random r = null;

		/// <summary>
		/// best filled container in all generations
		/// </summary>
		private ProcessContainer bestSolution = null;

		/// <summary>
		/// best chromosome of all generations
		/// </summary>
		private Chromosome bestChromosome = null;

		/// <summary>
		/// public constructor
		/// </summary>
		/// <param name="boxes">boxes set</param>
		/// <param name="newContainer">container</param>
		public Genetic(Product[] boxes, Container newContainer)
		{
			bioMass = new Dictionary<int, Product>();
			for (int i = 0; i < boxes.Length; i++)
			{
				if (boxes[i] != null)
				{
					bioMass.Add(i, boxes[i]);
				}
			}
			logger = new Logger();
			r = new Random(DateTime.Now.Millisecond);
			container = new ProcessContainer(newContainer.Size, newContainer.Weight);
			generationsCount = bioMass.Count * 5;
			population = bioMass.Count * 3;

			if (generationsCount > 200)
			{
				if (ConfigurationSettings.AppSettings["QandD"].ToString() == "true")
				{
					//logger.Append(bioMass.Count.ToString() + " boxes is too much. Will search for the quick and dirty solution");
					while (generationsCount > 200)
					{
						generationsCount = (int)(generationsCount * 0.15);
					}
					while (population > 120)
					{
						population = (int)(population * 0.5);
					}
				}
			}

			//if (population > 120) population = 30;

			terminated = population / 50;
		}

		/// <summary>
		/// public constructor, we have 2 constructors for testings ;)
		/// please use one or another, it both works, nice. Almost :)
		/// </summary>
		/// <param name="boxes">boxes set</param>
		/// <param name="newContainer">container</param>
		public Genetic(Dictionary<int, Product> boxes, Container newContainer)
		{
			try
			{
				logger = new Logger();
				r = new Random(DateTime.Now.Millisecond);
				bioMass = boxes;
				container = new ProcessContainer(newContainer.Size, newContainer.Weight);
				generationsCount = bioMass.Count * 5;
				population = bioMass.Count * 3;

				if (generationsCount > 200) generationsCount = 200;
				if (population > 100) population = 100;

				terminated = population / 50;
			}
			catch (Exception ex)
			{
				logger.Append("Genetic: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// method to check if we already have nice solution
		/// </summary>
		public bool IsBestExceed
		{
			get { return isBest; }
		}

		/// <summary>
		/// make all crossovers, mutations and murders to create new generation (we need only pure Aryans ;) )
		/// </summary>
		/// <returns>set of chromosomes as new generation</returns>
		private Chromosome[] makeGeneration()
		{
			try
			{
				if (generationNumber == 0)
				{
					Chromosomes = new Chromosome[population];
					for (int i = 0; i < population; i++)
					{
						Chromosomes[i] = new Chromosome(bioMass, r);
					}
				}
				else
				{
					for (int i = population - terminated - 1; i >= 0; i--)
					{
						Chromosomes[i + terminated] = Chromosomes[i];
						if (i < terminated)
						{
							Chromosomes[i] = bestChromosome;
						}
					}

					Dictionary<int, Chromosome> incubator = new Dictionary<int, Chromosome>();

					for (int i = 0; i < population; i++)
					{
						if (r.Next(1, population + 1) > i)
						{
							if (i < terminated || incubator.Count == 0)
							{
								incubator.Add(i, Chromosomes[i]);
							}
							else
							{
								KeyValuePair<int, Chromosome> kvp = incubator.First();
								crossover(Chromosomes[i], kvp.Value);
								incubator.Remove(kvp.Key);
							}
						}
						else
						{
							mutation(Chromosomes[i]);
						}
					}
					while (incubator.Count > 0)
					{
						KeyValuePair<int, Chromosome> kvp = incubator.First();
						mutation(kvp.Value);
						incubator.Remove(kvp.Key);
					}
				}
				return Chromosomes;
			}
			catch (Exception ex)
			{
				logger.Append("makeGeneration: " + ex.Message, Logger.ERROR);
			}
			return null;
		}

		/// <summary>
		/// There are two kinds of mutation here: sequence mutation and position mutation. 
		/// Sequence mutation selects two bits in a chromosome and switch them to form a new one. 
		/// For position mutation, several bits are picked in the chromosome and are rotated into new random positions. 
		/// Choosing kind of mutation for chromosome by random factor
		/// </summary>
		/// <param name="A">Current chromosome</param>
		private void mutation(Chromosome A)
		{
			try
			{
				if (A.Count == 0)
				{
				}
				else if (A.Count == 1)
				{
					int oldOrientation = A.getOrientation(0);
					int newOrientation = oldOrientation;
					do
					{
						newOrientation = r.Next(1, 8);
					} while (newOrientation == oldOrientation);

					A.setOrientation(0, newOrientation);
				}
				else if (A.Count == 2)
				{
					int swap = A.getOrientation(0);
					A.setOrientation(0, A.getOrientation(1));
					A.setOrientation(1, swap);
				}
				else
				{
					if (r.Next(1, 3) == 1)
					{
						int position1 = r.Next(1, A.Count);
						int position2 = position1;
						while (position1 == position2)
						{
							position2 = r.Next(1, A.Count);
						}
						int swap = A.getOrientation(position1);
						A.setOrientation(position1, A.getOrientation(position2));
						A.setOrientation(position2, swap);
					}
					else
					{
						int start = r.Next(1, A.Count);
						int finish = start;
						int swap = 0;
						while (start == finish)
						{
							finish = r.Next(1, A.Count);
						}
						if (finish < start)
						{
							swap = finish;
							finish = start;
							start = swap;
						}
						swap = 0;
						for (int i = start; i <= finish; i++)
						{
							int newSwap = A.getOrientation(i);
							if (i == start)
								A.setOrientation(i, A.getOrientation(finish));
							else
								A.setOrientation(i, swap);
							swap = newSwap;
						}
					}
				}
			}
			catch (Exception ex)
			{
				logger.Append("mutation: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// The crossover is one-point crossover, a random location is selected for two parents and the two parts 
		/// after the crossover point of the two parents are switched over to form two children.
		/// </summary>
		/// <param name="A">Chromosome A</param>
		/// <param name="B">Chromosome B</param>
		private void crossover(Chromosome A, Chromosome B)
		{
			try
			{
				int crossPoint = r.Next(1, A.Count);
				int start = 0;
				int finish = 0;
				if (crossPoint > A.Count / 2)
				{
					start = crossPoint; finish = A.Count;
				}
				else
				{
					start = 0; finish = crossPoint;
				}
				for (int n = start; n < finish; n++)
				{
					int orientation = B.getOrientation(n);
					B.setOrientation(n, A.getOrientation(n));
					A.setOrientation(n, orientation);
				}
			}
			catch (Exception ex)
			{
				logger.Append("crossover: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// get new generation
		/// </summary>
		/// <param name="maxPopulation">population</param>
		/// <returns>set of chromosomes</returns>
		public Chromosome[] getGeneration(int maxPopulation)
		{
			population = maxPopulation;
			return makeGeneration();
		}

		/// <summary>
		/// get generation
		/// </summary>
		/// <returns>set of chromosomes</returns>
		public Chromosome[] getGeneration()
		{
			return makeGeneration();
		}

		/// <summary>
		/// count of generations for found solution
		/// </summary>
		public int GenerationCount
		{
			get { return generationsCount; }
		}

		/// <summary>
		/// count of population
		/// </summary>
		public int Population
		{
			get { return population; }
		}

		/// <summary>
		/// best solution in all generations
		/// </summary>
		public FilledContainer BestSolution
		{
			get
			{
				if (bestSolution == null) return null;
				Container container = new Container();
				container.Size.Width = bestSolution.Size.Width;
				container.Size.Depth = bestSolution.Size.Depth;
				container.Size.Height = bestSolution.Size.Height;
				container.Weight = bestSolution.Weight;

				Pack[] products = new Pack[bestSolution.Packed.Count];
				bestSolution.Packed.Values.CopyTo(products, 0);

				return new FilledContainer()
				{
					Container = container,
					Products = products,
					Products_col = bestSolution.Packed
				};
			}
		}

		/// <summary>
		/// best chromosome of all generations
		/// </summary>
		public Chromosome BestChromosome
		{
			get { return bestChromosome; }
		}

		/// <summary>
		/// for all chromosomes try to implement algorithm of packing in container
		/// </summary>
		public void evolution()
		{
			try
			{
				Chromosomes = getGeneration();

				Generation = new ProcessContainer[Chromosomes.Length];

				Thread[] threadPool = new Thread[5];
				int freePlace = 0;

				while (freePlace < threadPool.Length) { threadPool[freePlace] = null; freePlace++; }

				freePlace = 0;

				for (int i = 0; i < Chromosomes.Length; i++)
				{
					Generation[i] = new ProcessContainer(
						new Size
						{
							Height = container.Size.Height,
							Width = container.Size.Width,
							Depth = container.Size.Depth,
						},
						container.Weight
						);
					if (!threading)
					{
						go(i);
					}
					else
					{
						threadPool[freePlace] = new Thread(go);
						threadPool[freePlace].Start(i);

						while (threadPool[freePlace] != null)
						{
							if (threadPool[freePlace].IsAlive)
							{
								freePlace = (freePlace + 1) >= threadPool.Length ? 0 : freePlace + 1;
							}
							else
							{
								threadPool[freePlace] = null;
								break;
							}
						}
					}
				}

				if (threading)
				{
					bool complete = false;
					freePlace = 0;
					while (!complete)
					{
						if (threadPool[freePlace] == null)
						{
							if (freePlace + 1 >= threadPool.Length)
								complete = true;
							else
								freePlace++;
						}
						else if (threadPool[freePlace].IsAlive == false)
						{
							if (freePlace + 1 >= threadPool.Length)
								complete = true;
							else
								freePlace++;
						}
						else
							freePlace = 0;
					}
				}
				evaluateSolutions();
				generationNumber++;
			}
			catch (Exception ex)
			{
				logger.Append("evolution: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// evaluate solutions. Find the best solution, sorting solutions etc
		/// </summary>
		private void evaluateSolutions()
		{
			try
			{
				ProcessContainer swap = null;
				Chromosome swapChrom = null;
				for (int i = 0; i < Generation.Length - 1; i++)
				{
					for (int j = i + 1; j < Generation.Length; j++)
					{
						if (Generation[j].PackedVolume > Generation[i].PackedVolume)
						{
							swap = Generation[j];
							Generation[j] = Generation[i];
							Generation[i] = swap;
							swapChrom = Chromosomes[j];
							Chromosomes[j] = Chromosomes[i];
							Chromosomes[i] = swapChrom;
						}
						else { }
					}
				}
				if (bestSolution != null)
				{
					if (bestSolution.PackedVolume < Generation[0].PackedVolume)
					{
						bestSolution = Generation[0];
						bestChromosome = Chromosomes[0];
						if (bestSolution.evaluateState1())
						{
							isBest = true;
							//logger.Append("PROBABLY BEST SOLUTION FOUND");
						}
						else
						{
							//logger.Append("better solution found");
							//bestSolution.outputTotals();
						}
					}
				}
				else
				{
					bestSolution = Generation[0];
					bestChromosome = Chromosomes[0];
					if (bestSolution.evaluateState1())
					{
						isBest = true;
						//logger.Append("PROBABLY BEST SOLUTION FOUND");
					}
					else
					{
						//logger.Append("better solution found");
					}
					//bestSolution.outputTotals();
				}
				if ((bestChromosome.Count == bestSolution.PackedCount) || (bestSolution.PackedVolume == bestSolution.Volume))
				{
					isBest = true;
					//logger.Append("BEST SOLUTION FOUND");
					//bestSolution.outputTotals();
				}
			}
			catch (Exception ex)
			{
				logger.Append("evaluateSolutions: " + ex.Message, Logger.ERROR);
			}
		}

		/// <summary>
		/// packing each chromosome
		/// </summary>
		/// <param name="index">index of chromosome</param>
		public void go(object index)
		{
			try
			{
				int i = (int)index;

				ProcessContainer cont = Generation[i];
				Chromosome chrom = Chromosomes[i];

				foreach (KeyValuePair<int, Product> kvp in bioMass)
				{
					Size transSize = chrom.getTransSize(kvp.Value.Size, kvp.Key);
					Axis place = cont.insertBox(kvp.Key, kvp.Value, transSize);
				}
			}
			catch (Exception ex)
			{
				logger.Append("go: " + ex.Message, Logger.ERROR);
			}
		}
	}
}