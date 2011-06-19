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

        public IsOlder()
        {
        }

        public IsOlder(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public override void Configure(Dictionary<string, string> value)
        {
            if (value.ContainsKey("isolder"))
                _dateTime = DateTime.Parse(value["isolder"]);
        }

        public override IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source)
        {
            return source.Where(x =>
                                    {
                                        return x.CreationTime < _dateTime;
                                    });
        }

        public override string Name
        {
            get { return "IsOlder"; }
        }
    }
}
