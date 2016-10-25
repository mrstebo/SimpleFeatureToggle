using System.Collections.Generic;
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
            var content = _fileReader.ReadToEnd(_configuration.Filename);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

            return dict.Select(Feature.Parse);
        }
    }
}
