using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blockbuster.Contracts
{
	interface IConfigureable
	{
		void Configure(Dictionary<string, string> configuration);
	}
}
