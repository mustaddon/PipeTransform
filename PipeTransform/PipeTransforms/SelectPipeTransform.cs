using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RandomSolutions.PipeTransforms
{
    public class SelectPipeTransform : IPipeTransform
    {
        public string Name => "select";

        public object Transform(object obj, params string[] args)
        {
            var prop = args.Length > 0 ? args[0] : null;

            var array = obj?.GetType() == typeof(string) ? null
                : (obj as System.Collections.IEnumerable)?.Cast<object>();

            return array?.Select(x => _getObjValue(x, prop))
                ?? _getObjValue(obj, prop)
                as object;
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
