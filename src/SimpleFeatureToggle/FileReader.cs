using System.IO;

namespace SimpleFeatureToggle
{
    public interface IFileReader
    {
        string ReadToEnd(string filename);
    }

    internal class FileReader : IFileReader
    {
        public string ReadToEnd(string filename)
        {
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
