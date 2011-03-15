using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.Contracts;

namespace Blockbuster.Commands.Filtering
{
    public class IsOlder : AbstractCommand
    {
        DateTime _dateTime;

        public IsOlder(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public override IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source)
        {
            return source.Where(x => x.CreationTime < _dateTime);
        }

        public override string Name
        {
            get { return "IsOlder"; }
        }
    }
}
