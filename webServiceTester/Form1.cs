using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using webServiceTester.ServiceReference1;

namespace webServiceTester
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			BoxesArgument[] boxes = new BoxesArgument[7];

			boxes[0] = new BoxesArgument() { height = "1", depth = "0.5", width = "0.5", name = "name1", prodid = "prodid1", weight =  "1.5"};
			boxes[1] = new BoxesArgument() { height = "0.5", depth = "0.25", width = "0.25", name = "name2", prodid = "prodid2", weight = "0.5"};
			boxes[2] = new BoxesArgument() { height = "0.5", depth = "0.5", width = "0.5", name = "name3", prodid = "prodid3", weight = "0.3" };
			boxes[3] = new BoxesArgument() { height = "0.9", depth = "0.1", width = "0.1", name = "name4", prodid = "prodid4", weight = "1.2" };
			boxes[4] = new BoxesArgument() { height = "0.1", depth = "0.15", width = "0.15", name = "name5", prodid = "prodid5", weight = "1.1" };
			boxes[5] = new BoxesArgument() { height = "0.1", depth = "0.2", width = "0.1", name = "name6", prodid = "prodid6", weight = "1.0" };
			boxes[6] = new BoxesArgument() { height = "1.1", depth = "0.2", width = "0.1", name = "name7", prodid = "prodid7", weight = "0.2" };

			ContainersArgument[] containers = new ContainersArgument[4];

			containers[0] = new ContainersArgument() { depth = "1", height = "0.5", width = "0.5", weight = "3"};
			containers[1] = new ContainersArgument() { depth = "0.5", height = "0.25", width = "0.25", weight = "2"};
			containers[2] = new ContainersArgument() { depth = "0.5", height = "0.5", width = "0.5", weight = "1"};
			containers[3] = new ContainersArgument() { depth = "0.01", height = "0.01", width = "0.01", weight = "0.5"};

			ServiceSoap serviceSoap = new ServiceSoapClient();

			calculateRequestBody body = new calculateRequestBody();
			body.boxesString = boxes;
			body.containersString = containers;

			calculateRequest req = new calculateRequest(body);
			calculateResponse responce = new calculateResponse();

			responce = serviceSoap.calculate(req);

			//responce.ToString();

			
		}
	}
}