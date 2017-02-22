using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleFeatureToggle
{
    public class SimpleFeatureToggle
    {
        private readonly SimpleFeatureToggleConfiguration _configuration;

        public SimpleFeatureToggle(SimpleFeatureToggleConfiguration configuration)
        {
            if (configuration.FeatureLoadingStrategy == null)
            {
                throw new ArgumentNullException("configuration");
            }

            _configuration = configuration;
        }

        public IEnumerable<Feature> GetFeatures()
        {
            if(_configuration.FeatureLoadingStrategy == null)
            {
                return Enumerable.Empty<Feature>();
            }

            return _configuration.FeatureLoadingStrategy.GetFeatures();
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
