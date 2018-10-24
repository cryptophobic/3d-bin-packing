using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;

namespace BinTime.Logger
{
	/// <summary>
	/// class for logging
	/// </summary>
	public class Logger
	{
		/// <summary>
		/// path of logfile
		/// </summary>
		private string LogPath = "";

		/// <summary>
		/// path of logfile for errors
		/// </summary>
		private string ErrorPath = "";

		/// <summary>
		/// const for general logs
		/// </summary>
		public const byte GENERAL = 1;

		/// <summary>
		/// const for error logs
		/// </summary>
		public const byte ERROR = 2;

		/// <summary>
		/// public constructor
		/// </summary>
		/// <param name="LogFile">path to logfile</param>
		/// <param name="ErrorFile">path to logfile of errors</param>
		public Logger(string LogFile, string ErrorFile)
		{
			LogPath = LogFile;
			ErrorPath = ErrorFile;
		}

		/// <summary>
		/// public constructor
		/// </summary>
		/// <param name="LogFile">path to logfile</param>
		public Logger(string LogFile)
		{
			LogPath = LogFile;
			ErrorPath = LogFile.Replace(".", "_error.");
		}

		/// <summary>
		/// public constructor
		/// </summary>
		public Logger()
		{
			AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE");
			LogPath = ConfigurationSettings.AppSettings["customLog"].ToString();
			ErrorPath = ConfigurationSettings.AppSettings["errorLog"].ToString();
		}

		/// <summary>
		/// add new string to logfile
		/// </summary>
		/// <param name="str">string</param>
		public void Append(string str)
		{
			Append(str, GENERAL);
		}

		/// <summary>
		/// clear logfile
		/// </summary>
		public void clear()
		{
			FileStream fs = new FileStream(LogPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
			fs = new FileStream(ErrorPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
		}

		/// <summary>
		/// add new string to logfile
		/// </summary>
		/// <param name="str">string</param>
		/// <param name="mode">error or general</param>
		public void Append(string str, byte mode)
		{
			try
			{
				if ((mode & ERROR) != 0)
				{
					str = " EXCEPTION! " + str;
				}

				mode |= GENERAL;
				if ((mode & GENERAL) != 0)
				{
					FileStream fs = new FileStream(LogPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
					StreamWriter sw = new StreamWriter(fs);
					sw.WriteLine("[" + DateTime.Now.ToLongTimeString() + "]:" + str);
					sw.Flush();
					sw.Close();
				}
				if (((mode & ERROR) != 0) && LogPath != ErrorPath)
				{
					FileStream fs = new FileStream(ErrorPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
					StreamWriter sw = new StreamWriter(fs);
					sw.WriteLine("[" + DateTime.Now.ToLongTimeString() + "]:" + str);
					sw.Flush();
					sw.Close();
				}
			}
			catch (Exception)
			{
			}
		}
	}
}

