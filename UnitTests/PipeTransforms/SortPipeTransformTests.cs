using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomSolutions.PipeTransforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomSolutions.PipeTransforms.Tests
{
    [TestClass()]
    public class SortPipeTransformTests
    {
        [TestMethod()]
        public void TransformTest()
        {
            var pipe = new SortPipeTransform();

            var array_simple = Enumerable.Range(0, 20).Select(x => _rnd.Next(1, 1000)).ToArray();
            var array_result = (pipe.Transform(array_simple) as IEnumerable<object>)?.ToArray();
            var array_target = array_simple.OrderBy(x=>x).ToArray();

            Assert.AreEqual(array_simple.Length, array_result?.Length, "simple array length");

            for (var i = 0; i < array_simple.Length; i++)
                Assert.AreEqual(array_target[i], array_result[i], "simple array compare");

            var args = new[] { "desc", "Id" };
            var array = array_simple.Select(x => new { Id = x, Name = $"item#{x}" }).ToArray();
            var array_result2 = (pipe.Transform(array, args) as IEnumerable<object>)?.ToArray();
            var array_target2 = array.OrderByDescending(x=>x.Id).ToArray();

            Assert.AreEqual(array.Length, array_result?.Length, "array length");

            for (var i = 0; i < array.Length; i++)
                Assert.AreEqual(array_target2[i], array_result2[i], "array compare");
        }

        static Random _rnd = new Random();
    }
}