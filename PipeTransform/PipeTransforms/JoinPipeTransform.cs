using RandomSolutions.Extensions;
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

            var array = obj.AsEnumerable()?
                .Select(x => x.GetValue(prop))
                .Where(x => x != null)
                .ToArray();

            return array != null ? string.Join(separator, array) : null;
        }
    }
}
