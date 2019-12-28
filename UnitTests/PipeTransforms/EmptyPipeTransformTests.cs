using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomSolutions.PipeTransforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomSolutions.PipeTransforms.Tests
{
    [TestClass()]
    public class EmptyPipeTransformTests
    {
        [TestMethod()]
        public void TransformTest()
        {
            var pipe = new EmptyPipeTransform();

            Assert.AreEqual(pipe.Transform(""), EmptyPipeTransform.DefaultValue, "default args");
            
            var args = new[] { "-" };
            Assert.AreEqual(pipe.Transform(null, args), args[0], "custom args");

            var array = new string[] { " ", null, "not", "" };
            var array_result = (pipe.Transform(array) as IEnumerable<string>)?.ToArray();
            var array_target = array.Select(x => string.IsNullOrWhiteSpace(x) ? EmptyPipeTransform.DefaultValue : x).ToArray();

            Assert.AreEqual(array.Length, array_result.Length, "array length");

            for (var i = 0; i < array.Length; i++)
                Assert.AreEqual(array_result[i], array_target[i], "array compare");
        }
    }
}