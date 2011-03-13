using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.CommonTypes;

namespace Blockbuster.Commands.Filtering
{
    public class KeepLastMonth : AbstractCommand
    {
        public override IObservable<CommonTypes.FileSystemEntity> FilterFileSystemEntities(IObservable<CommonTypes.FileSystemEntity> source)
        {
            return source.Where(x => x.CreationTime < DateTime.Now.AddMonths(-1));
        }

        public override string Name
        {
            get { return "KeepLastMonth"; }
        }
    }
}
