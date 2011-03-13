using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.CommonTypes;

namespace Blockbuster.Commands.Filtering
{
    interface IFilter
    {
        
        IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source);
    }
}
