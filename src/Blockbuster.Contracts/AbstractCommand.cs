using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Blockbuster.Contracts
{
    public abstract class AbstractCommand
    {
        private Dictionary<string, object> _configuration;
        
        public virtual Dictionary<string, object> Configuration
        {
            get { return _configuration; }
            set { _configuration = value; }
        }
        
        public void Initialize()
        {
            if (string.IsNullOrEmpty(Name))
                throw new InvalidOperationException("command name is mandatory");
        }

        public abstract string Name { get; }

        public abstract IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source);
    }
}
