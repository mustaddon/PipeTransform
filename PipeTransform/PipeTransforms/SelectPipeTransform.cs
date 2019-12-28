using RandomSolutions.Extensions;
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
            var array = obj.AsEnumerable();

            return array?.Select(x => x.GetValue(prop))
                ?? obj.GetValue(prop)
                as object;
        }
    }
}
