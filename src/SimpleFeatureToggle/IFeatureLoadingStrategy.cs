using System.Collections.Generic;

namespace SimpleFeatureToggle
{
    public interface IFeatureLoadingStrategy
    {
        IEnumerable<Feature> GetFeatures();
    }
}
