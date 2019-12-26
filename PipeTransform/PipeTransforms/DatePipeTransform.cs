using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSolutions.PipeTransforms
{
    public class DatePipeTransform : IPipeTransform
    {
        public string Name => "date";

        public static string DefaultFormat = "dd.MM.yyyy";

        public object Transform(object obj, params string[] args)
        {
            var format = args.Length > 0 ? args[0] : DefaultFormat;
            var array = (obj as System.Collections.IEnumerable)?.Cast<object>();

            return array?.Select(x => _toString(x, format))
                ?? _toString(obj, format)
                as object;
        }

        static string _toString(object obj, string format)
        {
            var type = obj?.GetType();

            if (type == typeof(DateTime) || type == typeof(DateTime?))
                return ((DateTime)obj).ToString(format);

            if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
                return ((DateTimeOffset)obj).ToString(format);

            return null;
        }
    }
}
