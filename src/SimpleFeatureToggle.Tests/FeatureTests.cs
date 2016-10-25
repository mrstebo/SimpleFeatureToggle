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
        [TestCase("y", true)]
        [TestCase("true", true)]
        [TestCase("n", false)]
        [TestCase("false", false)]
        public void Parse_ShouldConvert_ValueToBoolean(string value, bool expected)
        {
            var kvp = new KeyValuePair<string, string>("test", value);

            var result = Feature.Parse(kvp);

            Assert.AreEqual(expected, result.Enabled);
        }
    }
}
