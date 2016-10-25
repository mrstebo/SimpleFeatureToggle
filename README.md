[![Build status](https://ci.appveyor.com/api/projects/status/c3kdvlw7g30kn99h?svg=true)](https://ci.appveyor.com/project/mrstebo/simplefeaturetoggle)

[![MyGet Prerelease](https://img.shields.io/myget/mrstebo/v/SimpleFeatureToggle.svg?label=MyGet_Prerelease)](https://www.myget.org/feed/mrstebo/package/nuget/SimpleFeatureToggle)
[![NuGet Version](https://img.shields.io/nuget/v/SimpleFeatureToggle.svg)](https://www.nuget.org/packages/SimpleFeatureToggle/)

# SimpleFeatureToggle
A really simple library for checking if a feature is enabled/disabled

Here is what the file that is passed in to `SimpleFeatureToggleConfiguration` should look like:

```js
{
  "disabled-feature-1": 0
  "disabled-feature-2": "n",
  "disabled-feature-3": "false"
  "enabled-feature-1": 1
  "enabled-feature-2": "y",
  "enabled-feature-3": "true",  
}
```
