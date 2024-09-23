using RandomSolutions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RandomSolutions.PipeTransforms;

public class HtmlessPipeTransform : IPipeTransform
{
    public string Name => "htmless";

    public static string DefaultLinkFormat = "$2 ($1)";

    public object Transform(object obj, params string[] args)
    {
        var linkFormat = args.Length > 0 ? args[0] : DefaultLinkFormat;
        var array = obj.AsEnumerable();
        return array?.Select(x => _toSimpleText(x as string, linkFormat))
            ?? _toSimpleText(obj as string, linkFormat)
            as object;
    }

    static string _toSimpleText(string html, string linkFormat)
    {
        if (string.IsNullOrEmpty(html))
            return null;

        // replace tabs
        var result = Regex.Replace(html, @"\n[\t\s]+", "\n");

        // replace comments
        result = Regex.Replace(result, @"<!--(.*?)-->", string.Empty);

        // replace hyperlinks
        result = Regex.Replace(result,
            @"<a [^>]*?href=""([^""]+?)""[^>]*?>(.*?)</a>",
            m =>
            {
                var href = m.Groups[1].Value.Trim();
                var text = m.Groups[2].Value.Trim();
                return href.Length > 0 && !string.Equals(href, text, StringComparison.InvariantCultureIgnoreCase)
                    ? m.Result(linkFormat)
                    : text;
            }, RegexOptions.IgnoreCase);

        // replace other tags
        result = Regex.Replace(result,
            @"<([^<>\s/]*)[^<>]*?([^<>\s/]*)>",
            m =>
            {
                var tag = m.Groups[1].Value != string.Empty ? m.Groups[1].Value.ToLower() : m.Groups[2].Value != string.Empty ? m.Groups[2].Value.ToLower() : null;
                var open = m.Groups[1].Value != string.Empty;
                var res = tag == "li" && open ? "\r\n- "
                    : _inlines.Contains(tag) ? string.Empty
                    : _tabs.Contains(tag) ? (open ? string.Empty : " \t")
                    : "\r\n";
                return res;
            });

        // replace newlines
        result = Regex.Replace(result, @"[\r\n]+", "\r\n");

        return System.Net.WebUtility.HtmlDecode(result.Trim());
    }

    static readonly HashSet<string> _inlines = new() { "a", "span", "b", "big", "i", "small", "em", "strong", "button", "label", "tr" };
    static readonly HashSet<string> _tabs = new() { "td", "th" };

}
