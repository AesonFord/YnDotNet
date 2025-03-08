# YnDotNet

[![NuGet](https://img.shields.io/nuget/v/YnDotNet.svg)](https://www.nuget.org/packages/YnDotNet/)
[![Build Status](https://github.com/AesonFord/YnDotNet/workflows/Build%20and%20Publish%20NuGet%20Package/badge.svg)](https://github.com/AesonFord/YnDotNet/actions)
[![codecov](https://codecov.io/gh/AesonFord/YnDotNet/branch/main/graph/badge.svg)](https://codecov.io/gh/AesonFord/YnDotNet)

YnDotNet is a .NET port of the popular [yn](https://github.com/sindresorhus/yn) npm package. It provides a simple way to parse "yes" or "no" like values into boolean values, with support for lenient parsing to handle typos.

## Installation

Install using the NuGet Package Manager:

```
Install-Package YnDotNet
```

Or using the .NET CLI:

```
dotnet add package YnDotNet
```

## Usage

To use YnDotNet, include it in your project:

```csharp
using YnDotNet;
```

Then, you can use the `Yn` class and its methods:

```csharp
bool result = Yn.Parse("yes");
```

The following case-insensitive values are recognized:

```csharp
'y', 'yes', 'true', true, '1', 1, 'n', 'no', 'false', false, '0', 0, 'on', 'off', 'enabled', 'disabled'
```

## Building a Release Version

To manually build a release version of YnDotNet, run the following commands in the repository root:

```
dotnet build --configuration Release
dotnet pack --configuration Release
```

Alternatively, pushing a git tag beginning with "v" (e.g., v1.0.0) on the main or master branch will trigger the automated GitHub Actions workflow to build and publish the release version.

## Contributing

If you would like to contribute to YnDotNet, please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE.txt](LICENSE.txt) file for details.
