using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.CommonTypes;

namespace Blockbuster.Commands.Filtering
{
    public class DeleteNothing : AbstractCommand
    {
        public override IObservable<CommonTypes.FileSystemEntity> FilterFileSystemEntities(IObservable<CommonTypes.FileSystemEntity> source)
        {
            return Observable.Empty<FileSystemEntity>();
        }

        public override string Name
        {
            get { return "DeleteNothing"; }
        }
    }
}
