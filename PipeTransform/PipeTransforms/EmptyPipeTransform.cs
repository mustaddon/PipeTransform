using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSolutions.PipeTransforms
{
    public class EmptyPipeTransform : IPipeTransform
    {
        public string Name => "empty";

        public static string DefaultValue = "EMPTY";

        public object Transform(object obj, params string[] args)
        {
            var emptyValue = args.Length > 0 ? args[0] : DefaultValue;

            var array = obj?.GetType() == typeof(string) ? null
                : (obj as System.Collections.IEnumerable)?.Cast<object>();

            return array?.Select(x => _toString(x, emptyValue))
                ?? _toString(obj, emptyValue)
                as object;
        }

        static string _toString(object obj, string emptyValue)
        {
            var str = obj?.ToString();
            return string.IsNullOrWhiteSpace(str) ? emptyValue : str;
        }
    }
}
