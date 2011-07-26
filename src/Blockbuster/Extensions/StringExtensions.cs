using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blockbuster.Extensions
{
	public static class StringExtensions
	{
		public static DateTime? SubstractMinutesFromNow(this string str)
		{
			double minutes = 0;
			if (double.TryParse(str, out minutes))
			{
				return DateTime.Now.AddMinutes(minutes * -1);
			}
			return null;
		}

		public static double? ParseToNullableDouble(this string str)
		{
			double result;
			if (double.TryParse(str, out result))
			{
				return result;
			}
			return null;
		}
	}
}
