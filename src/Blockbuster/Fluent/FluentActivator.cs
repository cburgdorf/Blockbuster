using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.Commands;
using Blockbuster.Contracts;

namespace Blockbuster.Fluent
{
    public class FluentActivator
    {
        public FluentActivator() 
        {
            Commands = new List<AbstractCommand>();
        }

        public FluentActivator WithCommand<T>() where T : AbstractCommand, new()
        {
            if (Blockbuster == null)
                throw new InvalidOperationException("Blockbuster Instance Missing");

            return WithCommand<T>(Blockbuster);
        }
    
        public FluentActivator WithCommand<T>(IBlockbuster blockbuster) where T : AbstractCommand, new() 
        {
            Blockbuster = blockbuster;
            Commands.Add(new T());
            return this;
        }

        public void CleanUp(string directory)
        {
            if (Blockbuster == null)
                throw new InvalidOperationException("Blockbuster Instance Missing");

            CleanUp(Blockbuster, directory);
        }

        public void CleanUp(IBlockbuster blockbuster, string directory)
        {           
            blockbuster.CleanUp(directory, Commands);
        }

        private List<AbstractCommand> Commands { get; set; }

        private IBlockbuster Blockbuster { get; set;}
    }

    public static class FluentExtensions
    {
        public static FluentActivator WithCommand<T>(this IBlockbuster blockbuster) where T : AbstractCommand, new()
        {
            FluentActivator fluentActivator = new FluentActivator();
            return fluentActivator.WithCommand<T>(blockbuster);
        }
    }
}
