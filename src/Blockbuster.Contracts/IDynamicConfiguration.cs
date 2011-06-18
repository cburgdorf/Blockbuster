using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blockbuster.Contracts
{
    public interface IDynamicCommandConfiguration
    {
        Dictionary<string, string> Configuration { get; }
    }

    public interface IDynamicConfiguration
    {
        IEnumerable<IDynamicCommandConfiguration> GetCommandConfigurations();
    }
}
