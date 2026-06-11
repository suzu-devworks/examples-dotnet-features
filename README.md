# examples-dotnet-features

![Dynamic XML Badge](https://img.shields.io/badge/dynamic/xml?url=https%3A%2F%2Fraw.githubusercontent.com%2Fsuzu-devworks%2Fexamples-dotnet-features%2Frefs%2Fheads%2Fmain%2Fsrc%2FDirectory.Build.props&query=%2F%2FLatestFramework&logo=dotnet&label=Framework&color=%2328c2d1)
[![build](https://github.com/suzu-devworks/examples-dotnet-features/actions/workflows/dotnet-build.yml/badge.svg)](https://github.com/suzu-devworks/examples-dotnet-features/actions/workflows/dotnet-build.yml)
[![CodeQL](https://github.com/suzu-devworks/examples-dotnet-features/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/suzu-devworks/examples-dotnet-features/actions/workflows/github-code-scanning/codeql)

## What is the purpose of this repository?

This repository is basically my personal playground for learning and experimenting with .NET programming in C#.

Most of the content focuses on the Generic Host and the infrastructure commonly used in .NET applications,
such as dependency injection, configuration, logging, application lifetime management, and command-line argument handling.

The repository primarily serves as a personal knowledge base and a place to explore ideas through small, focused examples.

The examples reflect my current understanding of each topic and may evolve over time.

## The history of C# versions

- [C# version 15.0](./docs/versions/csharp-version-150.md)
- [C# version 14.0](./docs/versions/csharp-version-140.md)
- [C# version 13.0](./docs/versions/csharp-version-130.md)
- [C# version 12.0](./docs/versions/csharp-version-120.md)
- [C# version 11.0](./docs/versions/csharp-version-110.md)
- [C# version 10.0](./docs/versions/csharp-version-100.md)
- [C# version  9.0](./docs/versions/csharp-version-90.md)
- [C# version  8.0](./docs/versions/csharp-version-80.md)
- [C# version  7.3](./docs/versions/csharp-version-73.md)
- [C# version  7.2](./docs/versions/csharp-version-72.md)
- [C# version  7.1](./docs/versions/csharp-version-71.md)
- [C# version  7.0](./docs/versions/csharp-version-70.md)
- [C# version  6.0](./docs/versions/csharp-version-60.md)
- [C# version  5.0](./docs/versions/csharp-version-50.md)
- [C# version  4.0](./docs/versions/csharp-version-40.md)
- [C# version  3.0](./docs/versions/csharp-version-30.md)
- [C# version  2.0](./docs/versions/csharp-version-20.md)
- [C# version  1.2](./docs/versions/csharp-version-12.md)
- [C# version  1.0](./docs/versions/csharp-version-10.md)

## Why use Dev Containers?

I recommend using Dev Containers when working with this repository.

The development container provides the tools and dependencies needed to build and run the examples,
making it easy to get started without modifying your local environment.

For container details, see [`.devcontainer/devcontainer.json`](.devcontainer/devcontainer.json).

After the container is created, run [`.devcontainer/postCreateCommand.sh`](.devcontainer/postCreateCommand.sh)
and follow the instructions shown in the terminal.
