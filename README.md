# ricaun.Revit.Mvvm

Mvvm for Revit with `RelayCommand` and `ObservableObject` to use with [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged) package.

[![Revit 2017](https://img.shields.io/badge/Revit-2017+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Publish](../../actions/workflows/Publish.yml/badge.svg)](../../actions)
[![Develop](../../actions/workflows/Develop.yml/badge.svg)](../../actions)
[![Release](https://img.shields.io/nuget/v/ricaun.Revit.Mvvm?logo=nuget&label=release&color=blue)](https://www.nuget.org/packages/ricaun.Revit.Mvvm)

## Release

* [Latest release](../../releases/latest)

## Example

```xml
  <!-- Fody -->
  <ItemGroup>
    <PackageReference Include="PropertyChanged.Fody" Version="3.4.0" IncludeAssets="compile; build" PrivateAssets="all" />
  </ItemGroup>
  <PropertyGroup>
    <WeaverConfiguration >
      <Weavers>
        <PropertyChanged/>
      </Weavers>
    </WeaverConfiguration>
  </PropertyGroup>
```

## License

This project is [licensed](LICENSE) under the [MIT Licence](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!