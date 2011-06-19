using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using Blockbuster.Contracts;

namespace Blockbuster.Configuration
{
	public class XmlConfigurationReader : IDynamicConfiguration
	{
		public IEnumerable<string> GetCommandConfigurations()
		{
			var section = (NameValueCollection)ConfigurationManager.GetSection("CleanUpTasks");

			for (int i = 0; i < section.Keys.Count; i++)
			{
				string configurationEntry = section.Keys[i];
				var values = section.GetValues(configurationEntry);
				if (values == null || values.Length == 0)
					continue;

				yield return values[0];
			}
		}
	}
}
