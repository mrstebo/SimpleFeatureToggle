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
        private readonly string _fileName;

        public FileFeatureLoadingStrategy(string fileName)
            : this(fileName, new FileReader())
        {
            // no-op
        }

        internal FileFeatureLoadingStrategy(string fileName, IFileReader fileReader)
        {
            _fileReader = fileReader;
            _fileName = fileName;
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
