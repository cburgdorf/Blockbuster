using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Blockbuster.Commands;
using Blockbuster.CommonTypes;
using Blockbuster.Commands.Filtering;

namespace Blockbuster.Core
{
    public class CommandTracker
    {  
        private List<IFilter> Filter { get; set; }
       
        public CommandTracker()
        {
            Filter = new List<IFilter>();
        }

        public void AddCommand(AbstractCommand command)
        {
            if (!Filter.Cast<AbstractCommand>().ToList().Exists(c => c.Name == command.Name))
            {
                Filter.Add(command);
            }
        }
        
        public void DeleteCommands()
        {
            Filter.Clear();
        }

        public IObservable<FileSystemEntity> FilterFileSystemEntityStream(IObservable<FileSystemEntity> source) 
        {
            Filter.ForEach(x => source = x.FilterFileSystemEntities(source));
            return source;
        }       
    }
}
