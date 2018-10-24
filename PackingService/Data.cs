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

namespace PackingService
{
	public struct ContainersArgument
	{
		public string height;
		public string width;
		public string depth;

		public string weight;
	}

	public struct BoxesArgument
	{
		public string prodid;
		public string name;

		public string height;
		public string width;
		public string depth;

		public string weight;
	}

	public class containerParams
	{
		public string height;
		public string width;
		public string depth;
		public string volume;

		public string weight;
	}

	public class productParams
	{
		public string prodid;
		public string name;

		public string height;
		public string width;
		public string depth;

		public string weight;
	}

	public class Solution
	{
		public string containers;
		public string packedVolume;
		public string containersVolume;
		public string freeVolume;
		public containerParams[] removedContainers;
		public productParams[] removedBoxes;
		public ResultedContainers[] container;
	}

	public class ResultedContainers
	{
		public string No;
		public containerParams containerParams;
		public string packedBoxes;
		public string packedVolume;
		public string freeVolume;
		public packedProducts[] product;
	}

	public class packedProducts
	{
		public productParams productParams;
		public string x;
		public string y;
		public string z;
	}

}
