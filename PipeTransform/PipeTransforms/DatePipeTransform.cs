using RandomSolutions.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSolutions.PipeTransforms
{
    public class DatePipeTransform : IPipeTransform
    {
        public string Name => "date";

        public static string DefaultFormat = "dd.MM.yyyy HH:mm";
        public static CultureInfo DefaultLocale = CultureInfo.CurrentCulture;

        public object Transform(object obj, params string[] args)
        {
            var format = args.Length > 0 ? args[0] : DefaultFormat;
            var locale = args.Length > 1 ? CultureInfo.CreateSpecificCulture(args[1]) : DefaultLocale;
            var array = obj.AsEnumerable();

            return array?.Select(x => _toString(x, format, locale))
                ?? _toString(obj, format, locale)
                as object;
        }

        static string _toString(object obj, string format, CultureInfo locale)
        {
            var type = obj?.GetType();

            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return ((DateTime)obj).ToString(format, locale);

            if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
                return ((DateTimeOffset)obj).ToString(format, locale);

            return null;
        }
    }
}
