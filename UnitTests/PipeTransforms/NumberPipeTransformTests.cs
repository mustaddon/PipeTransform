using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomSolutions.PipeTransforms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace RandomSolutions.PipeTransforms.Tests
{
    [TestClass()]
    public class NumberPipeTransformTests
    {
        [TestMethod()]
        public void TransformTest()
        {
            var pipe = new NumberPipeTransform();

            var num = 12345.67890;
            var args = new[] { "1.2-2" };
            Assert.AreEqual(pipe.Transform(num, args), num.ToString("0.00", _cultureInfo));

            var array = new object[] { null, 12345.67890, 12345.67890f, 12345.67890m, 1234567890, 1234567890L };
            var array_result = (pipe.Transform(array, args) as IEnumerable<string>)?.ToArray();
            var array_target = array.Select(x => x != null ? Convert.ToDouble(x, _cultureInfo).ToString("0.00", _cultureInfo) : null).ToArray();

            Assert.AreEqual(array.Length, array_result.Length, "array length");

            for (var i = 0; i < array.Length; i++)
                Assert.AreEqual(array_target[i], array_result[i], "array compare");
        }


        static CultureInfo _cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
    }
}