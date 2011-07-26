using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blockbuster.Contracts;
using Blockbuster.Extensions;

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
			else
            {
            	var olderThan = DateTime.Now;
				if (value.ContainsKey("olderminutes"))
				{
					var minutesFromNow = value["olderminutes"].ParseToNullableDouble();
					_dateTime = minutesFromNow.HasValue ? olderThan.AddMinutes(minutesFromNow.Value * -1) : olderThan;
				}
				if (value.ContainsKey("olderhours"))
				{
					var hoursFromNow = value["olderhours"].ParseToNullableDouble();
					_dateTime = hoursFromNow.HasValue ? olderThan.AddHours(hoursFromNow.Value * -1) : olderThan;
				}
				if (value.ContainsKey("olderdays"))
				{
					var daysFromNow = value["olderdays"].ParseToNullableDouble();
					_dateTime = daysFromNow.HasValue ? olderThan.AddDays(daysFromNow.Value * -1) : olderThan;
				}
            }
			
				
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
