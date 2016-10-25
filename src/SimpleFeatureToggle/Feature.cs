using System;
using System.Collections.Generic;

namespace SimpleFeatureToggle
{
    public class Feature
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }

        public static Feature Parse(KeyValuePair<string, string> kvp)
        {
            return new Feature
            {
                Name = kvp.Key,
                Enabled = kvp.Value.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                          kvp.Value.Equals("y", StringComparison.OrdinalIgnoreCase)
            };
        }
    }
}
