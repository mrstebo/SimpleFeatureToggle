using System.IO;
using NUnit.Framework;

namespace SimpleFeatureToggle.Tests
{
    [TestFixture]
    [Parallelizable]
    public class FileReaderTests
    {
        private IFileReader _fileReader;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new FileReader();
        }

        [Test]
        public void ReadToEnd_ShouldReturn_ContentsOfFile()
        {
            const string fileContents = "This is some text";
            var tmp = Path.GetTempFileName();
            File.WriteAllText(tmp, fileContents);

            var result = _fileReader.ReadToEnd(tmp);

            Assert.AreEqual(fileContents, result);
        }
    }
}
