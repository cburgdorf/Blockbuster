using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.Contracts;

namespace Blockbuster.Commands.Filtering
{
    public class HasDate : AbstractCommand
    {
        Func<DateTime, bool> _predicate;

        public HasDate(Func<DateTime, bool> predicate) 
        {
            _predicate = predicate;
        }

        public override Dictionary<string, object> Configuration
        {
            get
            {
                return base.Configuration;
            }
            set
            {
                base.Configuration = value;
                if (value.ContainsKey("HasDate"))
                    _predicate = x => x == (DateTime)value["HasDate"];
            }
        }

        public override IObservable<FileSystemEntity> FilterFileSystemEntities(IObservable<FileSystemEntity> source)
        {
            return source.Where(x => _predicate(x.CreationTime) );
        }

        public override string Name
        {
            get { return "HasDate"; }
        }
    }
}
