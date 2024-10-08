﻿using RandomSolutions.Extensions;
using System.Linq;

namespace RandomSolutions.PipeTransforms;

public class EmptyPipeTransform : IPipeTransform
{
    public string Name => "empty";

    public static string DefaultValue = "EMPTY";

    public object Transform(object obj, params string[] args)
    {
        var emptyValue = args.Length > 0 ? args[0] : DefaultValue;
        var array = obj.AsEnumerable();

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
