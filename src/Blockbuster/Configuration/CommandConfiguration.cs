using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.Contracts;

namespace Blockbuster.Configuration
{
	public class DynamicCommandConfiguration : IDynamicCommandConfiguration
	{
		public DynamicCommandConfiguration(string configurationString)
		{
			Configuration = ParseString(configurationString);
		}

		/// <summary>
		/// Splits up an configuration string and returns an Dictionary<string, string>
		/// (e.g. "CommandName='FilesOnly'; Directory='C:\\Test\\'" becomes 
		/// new Dictionary<string, string>
		/// {
		///     {"commandname", "filesonly"},
		///     {"directory", "c:\\test\\"}
		/// })
		/// </summary>
		private Dictionary<string, string> ParseString(string configurationString)
		{
			Func<string, string> sanitizeString = x => x.Trim().ToLower().Replace("'", "");

			return configurationString
				.Split(';')
				.Select(x => x.Split('='))
				.Where(x => x.Length == 2)
				.ToDictionary(x => sanitizeString(x[0]), x => sanitizeString(x[1]));
		}

		public Dictionary<string, string> Configuration { get; protected set; }
	}
}
