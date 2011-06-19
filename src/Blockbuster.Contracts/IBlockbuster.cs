using System;
using System.Collections.Generic;

namespace Blockbuster.Contracts
{
	/// <summary>
	/// Description of ICleaningService.
	/// </summary>
	public interface IBlockbuster
	{
		void CleanUp(string directory);
		void CleanUp(string directory, string commandName, Dictionary<string, string> commandConfiguration);
		void CleanUp(string directory, Dictionary<string, Dictionary<string, string>> commandList);
		void CleanUp(string directory, IEnumerable<AbstractCommand> commands);
		void CleanUp(IDynamicConfiguration configuration, bool raiseExceptionsOnConfigErrors);
	}
}
