using System;
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
            var sft = new SimpleFeatureToggle(new SimpleFeatureToggleConfiguration
            {
                Filename = "test.json"
            });

            Assert.IsNotNull(sft);
        }

        [Test]
        public void GetFeatures_ShouldReturn_CollectionOf_Features()
        {
            _fileReader
                .Setup(x => x.ReadToEnd("test.json"))
                .Returns("{\"test\": 1}");

            var sft = new SimpleFeatureToggle(new SimpleFeatureToggleConfiguration
            {
                Filename = "test.json"
            }, _fileReader.Object);

            var result = sft.GetFeatures().ToArray();

            Assert.AreEqual(1, result.Length);
            Assert.AreEqual("test", result[0].Name);
            Assert.IsTrue(result[0].Enabled);
        }

        [Test]
        public void GetFeatures_When_FileLoaderRaisesException_ShouldReturn_EmptyCollectionOf_Features()
        {
            _fileReader
                .Setup(x => x.ReadToEnd("test.json"))
                .Throws(new Exception());

            var sft = new SimpleFeatureToggle(new SimpleFeatureToggleConfiguration
            {
                Filename = "test.json"
            }, _fileReader.Object);

            var result = sft.GetFeatures().ToArray();

            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void Find_ShouldReturn_Feature()
        {
            _fileReader
                .Setup(x => x.ReadToEnd("test.json"))
                .Returns("{\"MY_FEATURE\": 1}");

            var sft = new SimpleFeatureToggle(new SimpleFeatureToggleConfiguration
            {
                Filename = "test.json"
            }, _fileReader.Object);

            var result = sft.Find("MY_FEATURE");

            Assert.IsNotNull(result);
        }

        [Test]
        public void Find_When_FeatureDoesNotExist_ShouldReturn_Null()
        {
            _fileReader
                .Setup(x => x.ReadToEnd("test.json"))
                .Returns("{\"MY_FEATURE\": 1}");

            var sft = new SimpleFeatureToggle(new SimpleFeatureToggleConfiguration
            {
                Filename = "test.json"
            }, _fileReader.Object);

            var result = sft.Find("NOT_A_FEATURE");

            Assert.IsNull(result);
        }

        [Test]
        public void IsEnabled_ShouldReturn_FeatureEnabled()
        {
            _fileReader
                .Setup(x => x.ReadToEnd("test.json"))
                .Returns("{\"MY_FEATURE\": 1}");

            var sft = new SimpleFeatureToggle(new SimpleFeatureToggleConfiguration
            {
                Filename = "test.json"
            }, _fileReader.Object);

            var result = sft.IsEnabled("MY_FEATURE");

            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsEnabled_When_FeatureNotFound_ShouldReturn_DefaultValue(bool defaultValue)
        {
            _fileReader
                .Setup(x => x.ReadToEnd("test.json"))
                .Returns("{\"MY_FEATURE\": " + Convert.ToInt32(defaultValue) + "}");

            var sft = new SimpleFeatureToggle(new SimpleFeatureToggleConfiguration
            {
                Filename = "test.json"
            }, _fileReader.Object);

            var result = sft.IsEnabled("MY_FEATURE", defaultValue);

            Assert.AreEqual(defaultValue, result);
        }
    }
}
