using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SimpleFeatureToggle.Tests
{
    [TestFixture]
    [Parallelizable]
    public class FeatureTests
    {
        [Test]
        public void Parse_ShouldReturn_Feature()
        {
            var kvp = new KeyValuePair<string, string>("test", "true");

            var result = Feature.Parse(kvp);

            Assert.AreEqual(kvp.Key, result.Name);
            Assert.IsTrue(result.Enabled);
        }

        [Test]
        [TestCase(0, false)]
        [TestCase("n", false)]
        [TestCase("false", false)]
        [TestCase(1, true)]
        [TestCase("y", true)]
        [TestCase("true", true)]
        public void Parse_ShouldConvert_ValueToBoolean(object value, bool expected)
        {
            var kvp = new KeyValuePair<string, string>("test", Convert.ToString(value));

            var result = Feature.Parse(kvp);

            Assert.AreEqual(expected, result.Enabled);
        }
    }
}
