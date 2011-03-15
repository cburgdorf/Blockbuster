using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Blockbuster.Contracts
{
    public abstract class AbstractCommand
    {
        public object AdditionalParameters { get; set; }
        
        public void Initialize()
        {
            if (string.IsNullOrEmpty(Name))
                throw new InvalidOperationException("command name is mandatory");
        }

        public abstract string Name { get; }

        public abstract IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source);
    }
}
