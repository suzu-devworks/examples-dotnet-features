# Configuration

## The way to the present

```shell
git clone https://github.com/examples-dotnet-features.git
cd examples-dotnet-features

## run in Dev Container.

dotnet new sln -o .

#dotnet nuget update source github --username suzu-devworks --password "{parsonal access token}" --store-password-in-clear-text

## Examples.Features.CS{CSVER}.Tests
CSVER=120
dotnet new xunit -o src/Examples.Features.CS${CSVER}.Tests
dotnet sln add src/Examples.Features.CS${CSVER}.Tests
cd src/Examples.Features.CS${CSVER}.Tests
dotnet add reference ../../src/Examples.Features
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package coverlet.collector
dotnet add package moq
dotnet add package FluentAssertions
cd ../../


dotnet build

# Update outdated package
dotnet list package --outdated

# Tools
dotnet new tool-manifest

```
