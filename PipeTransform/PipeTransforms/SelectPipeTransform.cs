using RandomSolutions.Extensions;
using System.Linq;

namespace RandomSolutions.PipeTransforms;

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
