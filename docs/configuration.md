# Configuration

## The way to the present

```shell
git clone https://github.com/examples-dotnet-features.git
cd examples-dotnet-features

## run in Dev Container.

dotnet new sln -o .

#dotnet nuget update source github --username suzu-devworks --password "{parsonal access token}" --store-password-in-clear-text


## Examples.Features.CS${CSVER}.Tests
CSVER=11
dotnet new xunit -o src/Examples.Features.CS${CSVER}.Tests
dotnet sln add src/Examples.Features.CS${CSVER}.Tests
cd src/Examples.Features.CS${CSVER}.Tests
dotnet add package Moq
dotnet add package ChainingAssertion.Core.Xunit
cd ../../


dotnet build

# Update outdated package
dotnet list package --outdated

# Tools
dotnet new tool-manifest

```
