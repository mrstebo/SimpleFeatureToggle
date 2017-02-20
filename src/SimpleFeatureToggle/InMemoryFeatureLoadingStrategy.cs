using System.Collections.Generic;
using System.Linq;

namespace SimpleFeatureToggle
{
    public class InMemoryFeatureLoadingStrategy : IFeatureLoadingStrategy
    {
        private readonly IEnumerable<Feature> _features;

        public InMemoryFeatureLoadingStrategy()
            : this(Enumerable.Empty<Feature>())
        {
            // no-op
        }

        public InMemoryFeatureLoadingStrategy(IEnumerable<Feature> features)
        {
            _features = features;
        }

        public IEnumerable<Feature> GetFeatures()
        {
            return _features;
        }
    }
}
