using RandomSolutions.PipeTransforms;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var pipe = new SortPipeTransform();
            var array = Enumerable.Range(0, 20).Select(x => _rnd.Next(1, 1000)).Select(x => new { Id = x, Name = $"item#{x}" }).ToArray();
            var result = (pipe.Transform(array, "desc", "Id") as IEnumerable<object>).ToArray();

            var text = new HtmlessPipeTransform().Transform(@"
<div>test</div>
<table>
<thead>
<tr><th>aaa</th><th>bbb</th></tr>
</thead>
<tbody>
<tr><td>11</td><td>22</td></tr>
<tr><td>111</td><td>222</td></tr>
</tbody>
</table>");
        }

        static readonly Random _rnd = new Random();
    }
}
