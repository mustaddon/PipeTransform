using RandomSolutions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RandomSolutions.PipeTransforms
{
    public class SortPipeTransform : IPipeTransform
    {
        public string Name => "sort";

        public object Transform(object obj, params string[] args)
        {
            var array = obj.AsEnumerable();

            if (array == null)
                return obj;

            var asc = args.Length > 0 ? !_desc.Contains(args[0]?.ToLower()) : true;
            var prop = args.Length > 1 ? args[1] : null;

            return asc
                ? array.OrderBy(x => x.GetValue(prop))
                : array.OrderByDescending(x => x.GetValue(prop));
        }

        static HashSet<string> _desc = new HashSet<string> { ">", "desc", "descending" };

    }
}
