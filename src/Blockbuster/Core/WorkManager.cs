using System;
using System.Collections.Generic;
using System.Linq;
using Blockbuster.Commands;
using Blockbuster.Commands.Filtering;
using Blockbuster.DirectoryIterator;
using Blockbuster.CommonTypes;

namespace Blockbuster.Core
{
	/// <summary>
	/// Description of WorkManager.
	/// </summary>
	public class WorkManager
	{
		private CommandTracker CommandTracker { get; set; }
		private IFileSystemIterator Iterator { get; set; }
		private IScrapHeap ScrapHeap { get; set; }

		public WorkManager(IFileSystemIterator iterator, IScrapHeap scrapHeap)
		{
			CommandTracker = new CommandTracker();
			Iterator = iterator;
			ScrapHeap = scrapHeap;
		}

		public WorkManager(IFileSystemIterator iterator) : this(iterator, new ScrapHeap())
		{
		}        

		public WorkManager() : this(new ObservableFileAndDirectoryIterator(), new ScrapHeap())
		{					
		}
		
		public void Delete(string directory)
		{
			Delete(directory, Enumerable.Empty<AbstractCommand>());
		}

		public void Delete(string directory, IEnumerable<AbstractCommand> commands) 
		{
			SetCommands(commands);
			RunDelete(directory);
		}
		
		private void RunDelete(string directory)
		{
			//create a stream of files and directories from the given root directory
			var fileStream = Iterator.Iterate(directory);

			//filter with given commands and create a stream of garbage
			var scrapStream = CommandTracker.FilterFileSystemEntityStream(fileStream);
			
			//clean the garbage with the given delete action
			ScrapHeap.BurnScrap(scrapStream);            
		}

		private void SetCommands(IEnumerable<AbstractCommand> commands) 
		{
			//Clear old commands
			CommandTracker.DeleteCommands();
			//Set given commands
			commands.ToList().ForEach(c => CommandTracker.AddCommand(c));
		}
	}
}
