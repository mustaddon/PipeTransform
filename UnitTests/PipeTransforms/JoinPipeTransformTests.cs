using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomSolutions.PipeTransforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomSolutions.PipeTransforms.Tests
{
    [TestClass()]
    public class JoinPipeTransformTests
    {
        [TestMethod()]
        public void TransformTest()
        {
            var pipe = new JoinPipeTransform();

            var array_simple = new string[] { null, "111", "222", "333" };
            Assert.AreEqual(pipe.Transform(array_simple), string.Join(JoinPipeTransform.DefaultSeparator, array_simple.Where(x => x != null)), "simple array");

            var array = Enumerable.Range(1, 10).Select(x => new { Name = $"item#{x}" });
            var args = new[] { "; ", "Name" };
            var array_result = pipe.Transform(array, args) as string;
            var array_target = string.Join(args[0], array.Select(x => x.Name));

            Assert.AreEqual(array_result, array_target, "join compare");
        }
    }
}