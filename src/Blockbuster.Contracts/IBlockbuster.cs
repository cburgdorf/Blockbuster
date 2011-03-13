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
        void CleanUp(string directory, string commandName, object commandParams);
        void CleanUp(string directory, Dictionary<string, object> commandList);
	}
}
