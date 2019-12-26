using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RandomSolutions.PipeTransforms
{
    public class JoinPipeTransform : IPipeTransform
    {
        public string Name => "join";

        public static string DefaultSeparator = string.Empty;

        public object Transform(object obj, params string[] args)
        {
            var separator = args.Length > 0 ? args[0] : DefaultSeparator;
            var prop = args.Length > 1 ? args[1] : null;

            var array = (obj as System.Collections.IEnumerable)?.Cast<object>()
                .Select(x => _getObjValue(x, prop))
                .Where(x => x != null)
                .ToArray();

            return array != null ? string.Join(separator, array) : null;
        }

        static object _getObjValue(object obj, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return obj;

            try
            {
                var param = Expression.Parameter(obj.GetType(), string.Empty);
                var prop = path.Trim().Split('.').Aggregate<string, Expression>(param, (r, x) => Expression.PropertyOrField(r, x));
                var getter = Expression.Lambda(prop, param);
                return getter.Compile().DynamicInvoke(obj);
            }
            catch { }

            return null;
        }
    }
}
