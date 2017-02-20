using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;

namespace SimpleFeatureToggle.Tests
{
    [TestFixture]
    [Parallelizable]
    public class InMemoryFeatureLoadingStrategyTests
    {
        [Test]
        public void GivenANewInstanceIsCreated_ThenANewInstanceShouldBeReturned()
        {
            var loader = new InMemoryFeatureLoadingStrategy();

            Assert.IsNotNull(loader);
        }

        [Test]
        public void GivenFeaturesArePassedToTheConstructor_ThenTheCorrectFeaturesShouldBeReturned()
        {
            var expected = new Feature
            {
                Enabled = true,
                Name = "test"
            };
            var features = new List<Feature> { expected };

            var loader = new InMemoryFeatureLoadingStrategy(features);
            var actual = loader.GetFeatures().ToList();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Any());
            Assert.That(actual.First().Name, Is.EqualTo(expected.Name));
            Assert.That(actual.First().Enabled, Is.EqualTo(expected.Enabled));
        }

        [Test]
        public void GivenThereAreNoFeaturesPassedIn_ThenAnEmptyEnumerableShouldBeReturned()
        {
            var loader = new InMemoryFeatureLoadingStrategy();
            var actual = loader.GetFeatures().ToList();

            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Any());
        }
    }
}
