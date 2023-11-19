# ricaun.Revit.Mvvm

Mvvm for Revit with `RelayCommand`, `AsyncRelayCommand` and `ObservableObject` to use with [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged) package.

[![Revit 2015](https://img.shields.io/badge/Revit-2015+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](../../actions/workflows/Build.yml/badge.svg)](../../actions)
[![Release](https://img.shields.io/nuget/v/ricaun.Revit.Mvvm?logo=nuget&label=release&color=blue)](https://www.nuget.org/packages/ricaun.Revit.Mvvm)

## Installation

Insert this configuration to enable and install the package [ricaun.Revit.Mvvm](https://www.nuget.org/packages/ricaun.Revit.Mvvm) in the `csproj`.

```xml
<ItemGroup>
    <PackageReference Include="ricaun.Revit.Mvvm" Version="*" />
</ItemGroup>
```

### PropertyChanged.Fody

Insert this configuration to enable and install the package [PropertyChanged.Fody](https://github.com/Fody/PropertyChanged) in the `csproj`.

```xml
<!-- Fody -->
<ItemGroup>
    <PackageReference Include="PropertyChanged.Fody" Version="3.*" PrivateAssets="all" />
</ItemGroup>
<PropertyGroup>
    <WeaverConfiguration >
        <Weavers>
            <PropertyChanged/>
        </Weavers>
    </WeaverConfiguration>
</PropertyGroup>
```

## Features

### IRelayCommand & IAsyncRelayCommand
```C#
public IRelayCommand Command { get; }
public IAsyncRelayCommand AsyncCommand { get; }
```

### RelayCommand & RelayCommand<T>
```C#
Command = new RelayCommand(() =>
{
    // Execute something
});
Command = new RelayCommand<string>((text) =>
{
    // Execute something with text
});
```

### AsyncRelayCommand & AsyncRelayCommand<T>
```C#
AsyncCommand = new AsyncRelayCommand(async () =>
{
    // Execute something async
    await Task.Delay(1000);
});
AsyncCommand = new AsyncRelayCommand<string>(async (text) =>
{
    // Execute something async with text
    await Task.Delay(1000);
});
```

### ExceptionHandler
```C#
Command.ExceptionHandler += (ex) => { Console.WriteLine(ex); };

// or

Command = new RelayCommand(() =>
{
    // Execute something
}).SetExceptionHandler((ex) => { Console.WriteLine(ex); });
```

## Release

* [Latest release](../../releases/latest)

## License

This project is [licensed](LICENSE) under the [MIT Licence](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!