using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.Contracts;

namespace Blockbuster.Commands.Filtering
{
    public class DeleteNothing : AbstractCommand
    {
        public override IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source)
        {
            return Observable.Empty<FileSystemEntity>();
        }

        public override string Name
        {
            get { return "DeleteNothing"; }
        }
    }
}
