using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;

namespace Blockbuster.Clients.TaskManager.Settings
{
    public class Settings
    {
        public Settings()
        {
            _commandConfigurations = new List<CommandConfiguration>();
        }

        private readonly List<CommandConfiguration> _commandConfigurations;

        public IEnumerable<CommandConfiguration> CommandConfigurations
        {
            get { return _commandConfigurations; }
        }

        public static Settings FromAppConfig()
        {
            var section = (NameValueCollection)ConfigurationManager.GetSection("CleanUpTasks");

            var settings = new Settings();

            for (int i = 0; i < section.Keys.Count; i++)
            {
                string commandName = section.Keys[i];
                var values = section.GetValues(commandName);
                if (values == null || values.Length == 0)
                    continue;

                string value = values[0];

                var splittedValue = value.Split('|');

                if (splittedValue.Length < 2)
                    continue;

                settings._commandConfigurations.Add(new CommandConfiguration
                                                        {
                                                            EntryName = commandName,
                                                            CommandName = splittedValue[0],
                                                            DirectoryPath = splittedValue[1]
                                                        });
            }
            return settings;
        }
    }

    public class CommandConfiguration
    {
        public string EntryName { get; set; }
        public string CommandName { get; set; }
        public string DirectoryPath { get; set; }
    }
}
