using RandomSolutions.Extensions;
using System;
using System.Globalization;
using System.Linq;

namespace RandomSolutions.PipeTransforms;

public class DatePipeTransform : IPipeTransform
{
    public string Name => "date";

    public static string DefaultFormat = "dd.MM.yyyy";
    public static CultureInfo DefaultLocale = CultureInfo.CurrentCulture;

    public object Transform(object obj, params string[] args)
    {
        var format = args.Length > 0 ? args[0] : DefaultFormat;
        var locale = args.Length > 1 ? CultureInfo.CreateSpecificCulture(args[1]) : DefaultLocale;
        var array = obj.AsEnumerable();

        return array?.Select(x => _toString(x, format, locale))
            ?? _toString(obj, format, locale)
            as object;
    }

    static string _toString(object obj, string format, CultureInfo locale)
    {
        if (obj is DateTime dt)
            return dt.ToString(format, locale);

        if (obj is DateTimeOffset dto)
            return dto.ToString(format, locale);

#if NET6_0_OR_GREATER
        if (obj is DateOnly date)
            return date.ToString(format, locale);
#endif

        return null;
    }
}
