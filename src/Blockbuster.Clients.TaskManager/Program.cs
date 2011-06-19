using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.Configuration;

namespace Blockbuster.Clients.TaskManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var blockbuster = new Blockbuster();
            blockbuster.CleanUp(new XmlConfigurationReader(), true);
        }
    }
}
