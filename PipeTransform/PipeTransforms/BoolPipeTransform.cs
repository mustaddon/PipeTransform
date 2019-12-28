using RandomSolutions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSolutions.PipeTransforms
{
    public class BoolPipeTransform : IPipeTransform
    {
        public string Name => "bool";

        public static string DefaultTrueValue = "TRUE";
        public static string DefaultFalseValue = "FALSE";
        public static string DefaultNullValue = null;

        public object Transform(object obj, params string[] args)
        {
            var trueValue = args.Length > 0 ? args[0] : DefaultTrueValue;
            var falseValue = args.Length > 1 ? args[1] : DefaultFalseValue;
            var nullValue = args.Length > 2 ? args[2] : DefaultNullValue;
            var array = obj.AsEnumerable();

            return array?.Select(x => _toString(x, trueValue, falseValue, nullValue)) 
                ?? _toString(obj, trueValue, falseValue, nullValue) 
                as object;
        }

        static string _toString(object obj, string trueValue, string falseValue, string nullValue)
        {
            return obj == null ? nullValue 
                : Convert.ToBoolean(obj) ? trueValue
                : falseValue;
        }
    }
}
