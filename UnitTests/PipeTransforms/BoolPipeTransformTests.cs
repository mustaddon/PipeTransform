using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomSolutions.PipeTransforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomSolutions.PipeTransforms.Tests
{
    [TestClass()]
    public class BoolPipeTransformTests
    {
        [TestMethod()]
        public void TransformTest()
        {
            var pipe = new BoolPipeTransform();

            Assert.AreEqual(pipe.Transform(true), BoolPipeTransform.DefaultTrueValue, "default true");
            Assert.AreEqual(pipe.Transform(false), BoolPipeTransform.DefaultFalseValue, "default false");
            Assert.AreEqual(pipe.Transform(null), BoolPipeTransform.DefaultNullValue, "default null");

            var args = new[] { "Yes", "No", "-" };
            Assert.AreEqual(pipe.Transform(true, args), args[0], "custom true");
            Assert.AreEqual(pipe.Transform(false, args), args[1], "custom false");
            Assert.AreEqual(pipe.Transform(null, args), args[2], "custom null");

            var array = new bool?[] { true, false, null };
            var array_result = (pipe.Transform(array) as IEnumerable<string>)?.ToArray();
            var array_target = array.Select(x => !x.HasValue ? BoolPipeTransform.DefaultNullValue : x.Value ? BoolPipeTransform.DefaultTrueValue : BoolPipeTransform.DefaultFalseValue).ToArray();

            Assert.AreEqual(array.Length, array_result.Length, "array length");

            for (var i = 0; i < array.Length; i++)
                Assert.AreEqual(array_result[i], array_target[i], "array compare");
        }
    }
}