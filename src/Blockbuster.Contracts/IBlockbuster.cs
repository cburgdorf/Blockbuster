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
		void CleanUp(string directory, string commandName, Dictionary<string, object> commandConfiguration);
		void CleanUp(string directory, Dictionary<string, Dictionary<string, object>> commandList);
        void CleanUp(string directory, IEnumerable<AbstractCommand> commands);
	}
}
