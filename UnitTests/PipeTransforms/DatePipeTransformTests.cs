using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RandomSolutions.PipeTransforms.Tests
{
    [TestClass()]
    public class DatePipeTransformTests
    {
        [TestMethod()]
        public void TransformTest()
        {
            var pipe = new DatePipeTransform();

            var date = DateOnly.FromDateTime(DateTime.Now);
            var datetime = DateTime.Now;
            var dateOffset = DateTimeOffset.Now;
            var dtFormat = "dd.MM.yyyy HH:mm";

            Assert.AreEqual(pipe.Transform(date), date.ToString(DatePipeTransform.DefaultFormat, DatePipeTransform.DefaultLocale), "default format DateOnly");
            Assert.AreEqual(pipe.Transform(datetime, dtFormat), datetime.ToString(dtFormat, DatePipeTransform.DefaultLocale), "default format DateTime");
            Assert.AreEqual(pipe.Transform(dateOffset, dtFormat), dateOffset.ToString(dtFormat, DatePipeTransform.DefaultLocale), "default format DateTimeOffset");

            var args = new[] { "dd MMMM yyyy HH:mm:ss", "ru" };
            Assert.AreEqual(pipe.Transform(dateOffset, args), dateOffset.ToString(args[0], CultureInfo.CreateSpecificCulture(args[1])), "custom format");

            var array = new DateTimeOffset?[] { dateOffset, dateOffset.AddDays(1), null };
            var array_result = (pipe.Transform(array) as IEnumerable<string>)?.ToArray();
            var array_target = array.Select(x => x?.ToString(DatePipeTransform.DefaultFormat, DatePipeTransform.DefaultLocale)).ToArray();

            Assert.AreEqual(array.Length, array_result.Length, "array length");

            for (var i = 0; i < array.Length; i++)
                Assert.AreEqual(array_result[i], array_target[i], "array compare");
        }
    }
}