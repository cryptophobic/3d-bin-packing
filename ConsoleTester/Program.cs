using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTester.ServiceReference1;

namespace ConsoleTester
{
	class Program
	{
		static void Main(string[] args)
		{
			BoxesArgument[] boxes = new BoxesArgument[7];

			boxes[0] = new BoxesArgument() { height = "1", depth = "0.5", width = "0.5", name = "name1", prodid = "prodid1", weight = "1.5" };
			boxes[1] = new BoxesArgument() { height = "0.5", depth = "0.25", width = "0.25", name = "name2", prodid = "prodid2", weight = "0.5" };
			boxes[2] = new BoxesArgument() { height = "0.5", depth = "0.5", width = "0.5", name = "name3", prodid = "prodid3", weight = "0.3" };
			boxes[3] = new BoxesArgument() { height = "0.9", depth = "0.1", width = "0.1", name = "name4", prodid = "prodid4", weight = "1.2" };
			boxes[4] = new BoxesArgument() { height = "0.1", depth = "0.15", width = "0.15", name = "name5", prodid = "prodid5", weight = "1.1" };
			boxes[5] = new BoxesArgument() { height = "0.1", depth = "0.2", width = "0.1", name = "name6", prodid = "prodid6", weight = "1.0" };
			boxes[6] = new BoxesArgument() { height = "1.1", depth = "0.2", width = "0.1", name = "name7", prodid = "prodid7", weight = "0.2" };

			ContainersArgument[] containers = new ContainersArgument[4];

			containers[0] = new ContainersArgument() { depth = "1", height = "0.5", width = "0.5", weight = "3" };
			containers[1] = new ContainersArgument() { depth = "0.5", height = "0.25", width = "0.25", weight = "2" };
			containers[2] = new ContainersArgument() { depth = "0.5", height = "0.5", width = "0.5", weight = "1" };
			containers[3] = new ContainersArgument() { depth = "0.01", height = "0.01", width = "0.01", weight = "0.5" };

			ServiceSoap serviceSoap = new ServiceSoapClient();

			calculateRequestBody body = new calculateRequestBody();
			body.boxesString = boxes;
			body.containersString = containers;

			calculateRequest req = new calculateRequest(body);
			calculateResponse responce = new calculateResponse();

			responce = serviceSoap.calculate(req);

			Solution calculatedResult = responce.Body.calculateResult;

			Console.WriteLine("containers used=" + calculatedResult.containers);
			Console.WriteLine("packed volume=" + calculatedResult.packedVolume);
			Console.WriteLine("containers volume=" + calculatedResult.containersVolume);
			Console.WriteLine("free volume in containers=" + calculatedResult.freeVolume);

			if (calculatedResult.removedContainers.Count > 0)
			{
				Console.WriteLine();
				Console.WriteLine("removed containers due to sizes or weight conditions");
			}

			for (int i = 0; i < calculatedResult.removedContainers.Count; i++)
			{
				Console.WriteLine("weight=" + calculatedResult.removedContainers[i].weight +
													", height=" + calculatedResult.removedContainers[i].height +
													", width=" + calculatedResult.removedContainers[i].width +
													", depth=" + calculatedResult.removedContainers[i].depth);
			}

			if (calculatedResult.removedBoxes.Count > 0)
			{
				Console.WriteLine();
				Console.WriteLine("removed boxes due to sizes or weight conditions");
			}

			for (int i = 0; i < calculatedResult.removedBoxes.Count; i++)
			{
				Console.WriteLine("name=" + calculatedResult.removedBoxes[i].name + "(" + calculatedResult.removedBoxes[i].prodid +")" +
													"(weight=" + calculatedResult.removedBoxes[i].weight +
													", height=" + calculatedResult.removedBoxes[i].height +
													", width=" + calculatedResult.removedBoxes[i].width +
													", depth=" + calculatedResult.removedBoxes[i].depth + ")");
			}

			for (int i = 0; i < calculatedResult.container.Length; i++)
			{
				Console.WriteLine();
				ResultedContainers container = calculatedResult.container[i];
				Console.WriteLine("Container No " + container.No + 
											": (weight="+container.containerParams.weight+
											", depth="+container.containerParams.depth+
											", width="+container.containerParams.width+
											", height="+container.containerParams.height+")");

				Console.WriteLine("packed boxes=" + container.packedBoxes);
				Console.WriteLine("packed volume=" + container.packedVolume);
				Console.WriteLine("free volume=" + container.freeVolume);
				
				ArrayOfPackedProducts product = container.product;

				for (int j = 0; j < product.Count; j++)
				{
					Console.WriteLine("\t" + product[j].productParams.name+"("+product[j].productParams.prodid+
						") weight="+product[j].productParams.weight+
						", height="+product[j].productParams.height+
						", width="+product[j].productParams.width+
						",depth="+product[j].productParams.depth);
					Console.WriteLine("\tposition y="+product[j].y+",x="+product[j].x+",z="+product[j].z);
				}
			}
		}
	}
}
