using System.Linq;
using Moq;
using NUnit.Framework;

namespace SimpleFeatureToggle.Tests
{
    [TestFixture]
    [Parallelizable]
    public class SimpleFeatureToggleTests
    {
        private Mock<IFileReader> _fileReader;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
        }

        [Test]
        public void PublicConstructor_ShouldReturn_NewInstance()
        {
            var simpleFeatureToggle = new SimpleFeatureToggle(new SimpleFeatureToggleConfiguration
            {
                Filename = "test.json"
            });

            Assert.IsNotNull(simpleFeatureToggle);
        }

        [Test]
        public void GetFeatures_ShouldReturn_CollectionOf_Features()
        {
            _fileReader
                .Setup(x => x.ReadToEnd("test.json"))
                .Returns("{\"test\": \"true\"}");

            var simpleFeatureToggle = new SimpleFeatureToggle(new SimpleFeatureToggleConfiguration
            {
                Filename = "test.json"
            }, _fileReader.Object);

            var result = simpleFeatureToggle.GetFeatures().ToArray();

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("test", result[0].Name);
            Assert.IsTrue(result[0].Enabled);
        }
    }
}
