using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.Contracts;

namespace Blockbuster.Commands.Filtering
{
    public class KeepLastHour : AbstractCommand
    {
        public override IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source)
        {
            return source.Where(x => x.CreationTime < DateTime.Now.AddHours(-1));
        }

        public override string Name
        {
            get { return "KeepLastHour"; }
        }
    }
}
