using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomSolutions.PipeTransforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RandomSolutions.PipeTransforms.Tests
{
    [TestClass()]
    public class HtmlessPipeTransformTests
    {
        [TestMethod()]
        public void TransformTest()
        {
            var pipe = new HtmlessPipeTransform();
            var html = "<html><!-- comment --><div>text <i>iii</i></div><ul><li>111</li><li>222</li></ul><a href=\"http://aaa.bbb\">aaa <b>bbb</b></a><html>";

            Assert.IsFalse(Regex.Match(pipe.Transform(html) as string, @"<(.*?)>").Success, "remove tags");

            var args = new[] { "$2 [$1]" };
            Assert.IsTrue((pipe.Transform(html, args) as string).Contains("aaa bbb [http://aaa.bbb]"), "custom link");

            var array = new string[] { html, null };
            var array_result = (pipe.Transform(array) as IEnumerable<string>)?.ToArray();

            Assert.AreEqual(array.Length, array_result.Length, "array length");

            for (var i = 0; i < array.Length; i++)
                Assert.IsFalse(Regex.Match(array_result[i] ?? "", @"<(.*?)>").Success, "array tags check");
        }
    }
}