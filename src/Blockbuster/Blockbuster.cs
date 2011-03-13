using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Blockbuster.Contracts;
using Blockbuster.Core;
using Blockbuster.Commands;
using Blockbuster.Commands.Filtering;
using Blockbuster.DirectoryIterator;

namespace Blockbuster
{
	/// <summary>
	/// Description of CleanUpService.
	/// </summary>
	public class Blockbuster : IBlockbuster
	{
		readonly List<AbstractCommand> _cleaningCommands;
		readonly WorkManager _workManager;
		
		public Blockbuster()
		{
			_workManager = new WorkManager();
		   _cleaningCommands = new List<AbstractCommand>();
		   RegisterCommands();
		}

		/// <summary>
		/// This overload will simply delete all files and folders inside the give directory
		/// </summary>
		/// <param name="directory">Directory to delete</param>
		public void CleanUp(string directory)
		{
			CleanUp(directory, Enumerable.Empty<AbstractCommand>());
		}

		/// <summary>
		/// This Overload is useful for command line invoking with a single command
		/// </summary>
		/// <param name="directory">Directory to delete</param>
		/// <param name="commandName">Command name</param>
		/// <param name="commandParams">Optional parameters for the command</param>
		public void CleanUp(string directory, string commandName, object commandParams)
		{           
			//Find Command
			AbstractCommand command = FindCommand(commandName);
			//Set (optional) additional command prameter
			command.AdditionalParameters = commandParams;
			CleanUp(directory, new List<AbstractCommand>() { command });
		}

		/// <summary>
		/// This overload is useful for command line invoking 
		/// e.g. (blockbuster.exe -d="C:\Test" -FilesOnly -FileExtension="txt")
		/// </summary>
		/// <param name="Root directory where the cleanup should happen"></param>
		/// <param name="A string based list of commands and some additional command data 
		/// (e.g. -FileExtension='txt'"></param>
		public void CleanUp(string directory, Dictionary<string,object> commandList)
		{
			//temporary command list
			List<AbstractCommand> commands = new List<AbstractCommand>();

			//Lookup each command, set additional parameters and add it to the temporary command list
			foreach (var kvp in commandList)
			{
				//Command finden
				AbstractCommand command = FindCommand(kvp.Key);
				if (command != null && kvp.Value != null)
				{
					command.AdditionalParameters = kvp.Value;
					commands.Add(command);
				}
			}

			CleanUp(directory, commands);
		}
		
		/// <summary>
		/// This overload is useful if you take care of the command instantiation yourself
		/// (e.g. when you use the Blockbuster API inside your own application)
		/// </summary>
		/// <param name="directory">Directory to delete</param>
		/// <param name="commands">The list of commands to be used for the cleanup</param>
		public void CleanUp(string directory, IEnumerable<AbstractCommand> commands)
		{
			_workManager.Delete(directory, commands);
		}

		private AbstractCommand FindCommand(string command)
		{
			AbstractCommand currentCommand = _cleaningCommands.FirstOrDefault(c => c.Name.ToLower() == command.ToLower());
			if (currentCommand != null)
				return currentCommand;
			else
				throw new InvalidOperationException(string.Format("Command not found: {0}", command));
		}

		private void RegisterCommands()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			IEnumerable<Type> types = assembly.GetTypes();

			foreach (Type type in types)
			{                               
				if(type.IsSubclassOf(typeof(AbstractCommand)))
				{
					try
					{
						AbstractCommand currentCommand = (AbstractCommand)assembly.CreateInstance(type.ToString());
						_cleaningCommands.Add(currentCommand);
					}
					catch 
					{
						continue;
					}
				}
			}                       
		}
	}
}
