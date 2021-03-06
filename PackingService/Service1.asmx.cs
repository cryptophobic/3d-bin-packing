﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using BinTime.Logger;


namespace PackingService
{
	/// <summary>
	/// Summary description for Service1
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	// [System.Web.Script.Services.ScriptService]
	public class Service : System.Web.Services.WebService
	{
		private static Logger logger = new Logger();


		public Service()
		{
			try
			{

			}
			catch (Exception e)
			{
				logger.Append("Service: " + e.Message, Logger.ERROR);
			}

		}



		[WebMethod]
		public Solution calculate(BoxesArgument[] boxesString, ContainersArgument[] containersString)
		{
			return DataAccess.findSolution(boxesString, containersString);
		}
	}
}