using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.Contracts;

namespace Blockbuster.Commands.Filtering
{
    public class IsNewer : AbstractCommand
    {
        DateTime _dateTime;

        public IsNewer(DateTime dateTime) 
        {
            _dateTime = dateTime;
        }

        public override void Configure(Dictionary<string, string> value)
        {
            if (value.ContainsKey("isnewer"))
                _dateTime = DateTime.Parse(value["isnewer"]);
        }

        public override IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source)
        {
            return source.Where(x => x.CreationTime > _dateTime);
        }

        public override string Name
        {
            get { return "IsNewer"; }
        }
    }
}
