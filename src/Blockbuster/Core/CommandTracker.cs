using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Blockbuster.Commands;
using Blockbuster.Commands.Filtering;
using Blockbuster.Contracts;

namespace Blockbuster.Core
{
	public class CommandTracker
	{  
		private List<AbstractCommand> Commands { get; set; }
	   
		public CommandTracker()
		{
			Commands = new List<AbstractCommand>();
		}

		public void AddCommand(AbstractCommand command)
		{
			if (!Commands.Exists(c => c.Name == command.Name))
			{
				Commands.Add(command);
			}
		}
		
		public void DeleteCommands()
		{
			Commands.Clear();
		}

		public IObservable<FileSystemEntity> FilterFileSystemEntityStream(IObservable<FileSystemEntity> source) 
		{
			Commands.ForEach(x => source = x.FilterFileSystemEntities(source));
			return source;
		}       
	}
}
