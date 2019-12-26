using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RandomSolutions.PipeTransforms
{
    public class NumberPipeTransform : IPipeTransform
    {
        public string Name => "number";

        public static string DefaultFormat = "1.0-3";

        public object Transform(object obj, params string[] args)
        {
            var match = args.Length > 0 ? Regex.Match(args[0], _formatPattern) : null;

            if (match?.Success != true)
                match = Regex.Match(DefaultFormat, _formatPattern);

            var thousandSeparator = match.Groups[1].Value;
            var decimalSeparator = match.Groups[3].Value;
            var minIntegerDigits = int.Parse(match.Groups[2].Value);
            var minFractionDigits = int.Parse(match.Groups[4].Value);
            var maxFractionDigits = int.Parse(match.Groups[5].Value);

            var format = string.Format("#,#{0}.{1}",
                string.Join("", Enumerable.Range(1, minIntegerDigits).Select(x => "0")),
                string.Join("", Enumerable.Range(1, maxFractionDigits).Select(x => x > minFractionDigits ? "#" : "0")));

            var array = obj?.GetType() == typeof(string) ? null
                : (obj as System.Collections.IEnumerable)?.Cast<object>();

            return array?.Select(x => _toString(x, format, thousandSeparator, decimalSeparator))
                ?? _toString(obj, format, thousandSeparator, decimalSeparator)
                as object;
        }

        static string _formatPattern = @"^([\D]*?)([\d]+?)(\.|,)([\d]+?)-([\d]+?)$";

        static string _toString(object obj, string format, string thousandSeparator, string decimalSeparator)
        {
            if (obj == null) return null;
            var value = Convert.ToDouble(obj, _cultureInfo);
            var str = value.ToString(format, _cultureInfo);
            return Regex.Replace(str, @"\.|,", x => x.Value == "." ? decimalSeparator : thousandSeparator);
        }

        static CultureInfo _cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
    }
}
