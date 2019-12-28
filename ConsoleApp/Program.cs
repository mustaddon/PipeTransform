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
        }

        static Random _rnd = new Random();
    }
}
