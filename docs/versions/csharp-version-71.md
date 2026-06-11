# C# version 7.1

## Table of contents. <!-- omit in toc -->

- [Overview](#overview)
  - [Since](#since)
- [To study](#to-study)
- [Features](#features)
  - [async Main method](#async-main-method)
  - [default literal expressions](#default-literal-expressions)
  - [Inferred tuple element names](#inferred-tuple-element-names)
  - [Pattern matching on generic type parameters](#pattern-matching-on-generic-type-parameters)

## Overview

- [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-71)

### Since

- August 2017
- .NET Core 2.0
- Visual Studio 2017 version 15.3

## To study

- [Examples.Features.CSharp71.Tests](../../src/Examples.Features.CSharp71.Tests/)

## Features

### async Main method

> 非同期 `Main`

You can put the `async` modifier on an application's entry point.

```cs
// C# 7.0
public static void Main(string[] args)
{
    MainAsync().GetAwaiter().GetResult();

    async Task MainAsync()
    {
        Console.WriteLine("Hello world");
        await Task.CompletedTask;
   }
}

// C# 7.1 or later
public static Task Main()
{
}

// or

public static Task<int> Main()
{
}

// or 

public static Task Main(string[] args)
{
}

// or 

public static Task<int> Main(string[] args)
{

}
```

### default literal expressions

> `default` 式

We already had `default(T)` for creating default values, but `default` literals made this cleaner and shorter.

You can use default literals in any of these cases:

- Variable assignment or initialization.
- Declaring default values for optional method parameters.
- Supplying argument values in method calls.
- As an expression in `return` statements or expression-bodied members.

### Inferred tuple element names

> タプル要素名の推論

Tuple element names introduced in C# 7.0 can now be inferred from variable names used when creating tuples.

### Pattern matching on generic type parameters

> ジェネリック型に対するパターンマッチング

You can use pattern matching expressions on variables whose type is a generic type parameter.
