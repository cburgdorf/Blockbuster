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
    
        public FluentActivator WithCommand<T>(IBlockbuster blockbuster) where T : AbstractCommand, new() 
        {
            return WithCommand(blockbuster, new T());
        }

        public FluentActivator WithCommand<T>() where T : AbstractCommand, new()
        {            
            return WithCommand(Blockbuster, new T());
        }

        public FluentActivator WithCommand<T>(IBlockbuster blockbuster, Func<T> commandFunc) where T : AbstractCommand, new()
        {
            return WithCommand(blockbuster, commandFunc());
        }

        public FluentActivator WithCommand<T>(Func<T> commandFunc) where T : AbstractCommand, new()
        {
            return WithCommand(Blockbuster, commandFunc());
        }

        public FluentActivator WithCommand(IBlockbuster blockbuster, AbstractCommand command)
        {
            Blockbuster = blockbuster;
            return WithCommand(command);
        }

        public FluentActivator WithCommand(AbstractCommand command)
        {            
            Commands.Add(command);
            return this;
        }

        public void CleanUp(IBlockbuster blockbuster, string directory)
        {
            blockbuster.CleanUp(directory, Commands);
        }

        public void CleanUp(string directory)
        {
            if (Blockbuster == null)
                throw new InvalidOperationException("Blockbuster Instance Missing");

            CleanUp(Blockbuster, directory);
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

        public static FluentActivator WithCommand<T>(this IBlockbuster blockbuster, Func<T> commandFunc) where T : AbstractCommand, new()
        {
            FluentActivator fluentActivator = new FluentActivator();
			return fluentActivator.WithCommand(blockbuster, commandFunc);
        }

		public static FluentActivator WithCommand(this IBlockbuster blockbuster, AbstractCommand command) 
		{
			FluentActivator fluentActivator = new FluentActivator();
			return fluentActivator.WithCommand(blockbuster, command);
		}
    }
}
