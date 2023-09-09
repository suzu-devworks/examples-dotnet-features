# Configuration

## The way to the present

```shell
git clone https://github.com/examples-dotnet-features.git
cd examples-dotnet-features

## run in Dev Container.

dotnet new sln -o .

#dotnet nuget update source github --username suzu-devworks --password "{parsonal access token}" --store-password-in-clear-text

dotnet build

# Update outdated package
dotnet list package --outdated

# Tools
dotnet new tool-manifest

```
