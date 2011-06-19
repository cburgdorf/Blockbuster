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
            try
            {
                blockbuster.CleanUp(new XmlConfigurationReader(), IsDebug(args));
            }
            catch (Exception e)
            {
                if (IsDebug(args))
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }

        private static bool IsDebug(string[] args)
        {
            return args.Length > 0 && args[0] == "-debug";
        }
    }
}
