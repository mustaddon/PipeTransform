using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomSolutions.PipeTransforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomSolutions.PipeTransforms.Tests
{
    [TestClass()]
    public class SelectPipeTransformTests
    {
        [TestMethod()]
        public void TransformTest()
        {
            var pipe = new SelectPipeTransform();

            var obj = new { Name = $"item" };
            var args = new[] { "Name" };
            Assert.AreEqual(pipe.Transform(obj, args), obj.Name, "select object property");

            var array = Enumerable.Range(1, 10).Select(x => new { Name = $"item#{x}" });
            var array_result = (pipe.Transform(array, args) as IEnumerable<object>)?.ToArray();
            var array_target = array.Select(x => x.Name).ToArray();

            Assert.AreEqual(array_target.Length, array_result?.Length, "array length");

            for (var i = 0; i < array_target.Length; i++)
                Assert.AreEqual(array_target[i], array_result[i], "array compare");
        }
    }
}