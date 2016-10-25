using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace SimpleFeatureToggle
{
    public class SimpleFeatureToggle
    {
        private readonly SimpleFeatureToggleConfiguration _configuration;
        private readonly IFileReader _fileReader;

        public SimpleFeatureToggle(SimpleFeatureToggleConfiguration configuration)
            : this(configuration, new FileReader())
        {
        }

        internal SimpleFeatureToggle(SimpleFeatureToggleConfiguration configuration, IFileReader fileReader)
        {
            _configuration = configuration;
            _fileReader = fileReader;
        }

        public IEnumerable<Feature> GetFeatures()
        {
            try
            {
                var content = _fileReader.ReadToEnd(_configuration.Filename);
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

                return dict.Select(Feature.Parse);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Failed to get features: {0}", ex);
            }
            return Enumerable.Empty<Feature>();
        }

        public Feature Find(string name)
        {
            return GetFeatures().FirstOrDefault(x => x.Name == name);
        }

        public bool IsEnabled(string name, bool defaultValue = false)
        {
            var feature = Find(name);

            return feature == null ? defaultValue : feature.Enabled;
        }
    }
}
