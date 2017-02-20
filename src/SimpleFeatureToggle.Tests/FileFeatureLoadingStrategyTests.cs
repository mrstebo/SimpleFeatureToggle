using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;

namespace SimpleFeatureToggle.Tests
{
    [TestFixture]
    [Parallelizable]
    public class FileFeatureLoadingStrategyTests
    {
        private Mock<IFileReader> _fileReader;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
        }

        [Test]
        public void GivenANewInstanceIsCreated_ThenANewInstanceShouldBeReturned()
        {
            const string testFileName = "test.json";

            _fileReader
                .Setup(x => x.ReadToEnd(testFileName))
                .Returns("{\"test\": 1}");

            var loader = new FileFeatureLoadingStrategy(testFileName, _fileReader.Object);

            Assert.IsNotNull(loader);
        }

        [Test]
        public void GivenAFileIsLoaded_WhenTheFileContainsFeatures_ThenTheCorrectFeaturesShouldBeReturned()
        {
            const string testFileName = "test.json";
            var expected = new Feature
            {
                Enabled = true,
                Name = "test"
            };

            _fileReader
                .Setup(x => x.ReadToEnd(testFileName))
                .Returns("{\"test\": 1}");

            var loader = new FileFeatureLoadingStrategy(testFileName, _fileReader.Object);
            var actual = loader.GetFeatures().ToList();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.That(actual.First().Name, Is.EqualTo(expected.Name));
            Assert.That(actual.First().Enabled, Is.EqualTo(expected.Enabled));
        }

        [Test]
        public void GivenAFileIsLoaded_WhenTheFileDoesNotExist_ThenAnEmptyEnumerableShouldBeReturned()
        {
            const string testFileName = "test.json";
            IEnumerable<Feature> actual = null;

            _fileReader.Setup(x => x.ReadToEnd(testFileName)).Throws<FileNotFoundException>();

            var loader = new FileFeatureLoadingStrategy(testFileName, _fileReader.Object);

            Assert.DoesNotThrow(() => actual = loader.GetFeatures().ToList());
            Assert.IsNotNull(actual);
        }
    }
}