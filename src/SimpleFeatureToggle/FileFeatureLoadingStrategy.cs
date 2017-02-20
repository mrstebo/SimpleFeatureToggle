using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace SimpleFeatureToggle
{
    public class FileFeatureLoadingStrategy : IFeatureLoadingStrategy
    {
        private readonly IFileReader _fileReader;
        private string _fileName;

        public FileFeatureLoadingStrategy(string fileName = null, IFileReader fileReader = null)
        {
            _fileReader = fileReader ?? new FileReader();
            _fileName = fileName;
        }

        public FileFeatureLoadingStrategy From(string fileName)
        {
            _fileName = fileName;
            return this;
        }

        public IEnumerable<Feature> GetFeatures()
        {
            try
            {
                var content = _fileReader.ReadToEnd(_fileName);
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

                return dict.Select(Feature.Parse);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Failed to get features: {0}", ex);
            }

            return Enumerable.Empty<Feature>();
        }
    }
}
