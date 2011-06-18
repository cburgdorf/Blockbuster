using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Blockbuster.Configuration
{
	public class BulkCommandConfigurator
	{
		public BulkCommandInfo ParseCommandStrings(IEnumerable<string> commandStrings)
		{
			var aggregates = commandStrings
				.Aggregate(new
							{
								InvocationInfos = new Dictionary<string, IList<CommandInvocationInfo>>(),
								AdditionalInfos = new List<string>()
							},
						   (previous, x) =>
							   {
								   var commandConfiguration = new DynamicCommandConfiguration(x);
								   var configuration = commandConfiguration.Configuration;
								   if (configuration.ContainsKey("directory") && configuration.ContainsKey("commandname"))
								   {
								       var cachedCommandDirectory = configuration["directory"];
                                       var cachedCommandName = configuration["commandname"];
                                       
                                       if(!previous.InvocationInfos.ContainsKey(cachedCommandDirectory))
										   previous.InvocationInfos.Add(cachedCommandDirectory,new List<CommandInvocationInfo>());

								       configuration.Remove("commandname");
									   configuration.Remove("directory");
									   previous.InvocationInfos[cachedCommandDirectory].Add(new CommandInvocationInfo
																								 {
																									CommandName = cachedCommandName,
																									Configuration = configuration
																								 });
								   }
								   else if (!configuration.ContainsKey("directory"))
								   {
									   previous.AdditionalInfos.Add("One Command skipped due to missing 'Directory' property");
								   }
								   else if (!configuration.ContainsKey("commandname"))
								   {
									   previous.AdditionalInfos.Add("One Command skipped due to missing 'Command' property");
								   }
								   return previous;
							   });

			return new BulkCommandInfo()
					   {
						   InvocationInfos = aggregates.InvocationInfos,
						   AdditionalInfo = aggregates.AdditionalInfos
					   };
		}
	}

	public class BulkCommandInfo
	{
		public IDictionary<string, IList<CommandInvocationInfo>> InvocationInfos { get; set; }
		public IEnumerable<string> AdditionalInfo { get; set; }
	}

	public class CommandInvocationInfo
	{
		public string CommandName { get; set; }
		public Dictionary<string, string> Configuration { get; set; }
	}
}
