# C# version 10.0

## Table of contents. <!-- omit in toc -->

- [Overview](#overview)
  - [Since](#since)
- [To study](#to-study)
- [Features](#features)
  - [Record structs](#record-structs)
  - [Improvements of structure types](#improvements-of-structure-types)
  - [Interpolated string handlers](#interpolated-string-handlers)
  - [global using directives](#global-using-directives)
  - [File-scoped namespace declaration](#file-scoped-namespace-declaration)
  - [Extended property patterns](#extended-property-patterns)
  - [Improvements on lambda expressions](#improvements-on-lambda-expressions)
  - [Allow const interpolated strings](#allow-const-interpolated-strings)
  - [Record types can seal ToString()](#record-types-can-seal-tostring)
  - [Assignment and declaration in same deconstruction](#assignment-and-declaration-in-same-deconstruction)
  - [Improved definite assignment](#improved-definite-assignment)
  - [Allow AsyncMethodBuilder attribute on methods](#allow-asyncmethodbuilder-attribute-on-methods)
  - [CallerArgumentExpression attribute](#callerargumentexpression-attribute)
  - [Enhanced #line pragma](#enhanced-line-pragma)
- [.NET Library classes to Remember](#net-library-classes-to-remember)
  - [`LoggerMessageAttribute` class](#loggermessageattribute-class)

## Overview

[The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-10)

### Since

- November 2021
- .NET 6.0
- Visual Studio 2022 version 17.0 Preview

## To study

- [Examples.Features.CSharp100.Tests](../../src/Examples.Features.CSharp100.Tests/)

## Features

### Record structs

> レコード構造体

### Improvements of structure types

> 構造体型の機能強化

- Parameter-less constructors

### Interpolated string handlers

> 補間された文字列ハンドラー

This feature allows optimization and customization of how string interpolation (`$"{var}"`) is processed.
It can reduce boxing and memory allocations from traditional `String.Format`, and enables
high-performance string construction (especially with `ReadOnlySpan<char>`), custom logging output,
and type-safe parsing.

An interpolated string handler is a type that turns interpolated text into the final output string.
When the inserted value is a `string`, it's handled by `System.Runtime.CompilerServices.DefaultInterpolatedStringHandler`.

### global using directives

> グローバルな using ディレクティブ

- [Implicit Using directives](https://learn.microsoft.com/ja-jp/dotnet/core/tutorials/top-level-templates#implicit-using-directives)

```cs
global using System;
global using System.IO;
global using System.Collections.Generic;
global using System.Linq;
global using System.Net.Http;
global using System.Threading;
global using System.Threading.Tasks;
```

### File-scoped namespace declaration

> ファイル スコープの名前空間の宣言

This removes one level of nesting.

```cs
namespace MyNamespace;

```

### Extended property patterns

> 拡張プロパティのパターン

You can reference nested properties or fields inside property patterns.

```cs
// C# 8.0 or later
data is { Prop1: { Prop2: pattern } }

// C# 10.0 or later
data is { Prop1.Prop2: pattern }

```

### Improvements on lambda expressions

> ラムダ式の機能強化

C# 10 adds several nice improvements to how lambdas work.

- Lambdas have a natural type, so the compiler can infer delegate types from lambdas or method groups.
- You can declare a return type when the compiler can't infer it.
- You can apply attributes to lambdas.

Natural type basically means a type the compiler can infer from things like parameters.

### Allow const interpolated strings

> 定数の補間文字列

You can now use string interpolation in `const` strings.

```cs
// C# 10.0 or later
const string Language = "C#";
const string Platform = ".NET";
const string Version = "10.0";
const string FullProductName = $"{Platform} - Language: {Language} Version: {Version}";
```

### Record types can seal ToString()

> レコードの型で `ToString()` を `sealed` することができる。

Marking it `sealed` prevents derived records from overriding `ToString()`.
That makes it easy to enforce a consistent string representation.

```cs
// C# 10.0 or later
private record SealedRecord(int Value)
{
    public sealed override string ToString()
        => $"<<< {GetType().Name}: {{ Value = {Value} }} >>>";
}
```

### Assignment and declaration in same deconstruction

> 同じ Deconstructor 内で宣言と代入を同時に実行できる。

```cs
// C# 9.0 or later
(int x, int y) = point;

int x1 = 0;
int y1 = 0;
(x1, y1) = point;

// C# 10.0 or later
int x = 0;
(x, int y) = point;
```

### Improved definite assignment

> 限定代入の機能強化

### Allow AsyncMethodBuilder attribute on methods

> メソッドで AsyncMethodBuilder 属性を許可する

### CallerArgumentExpression attribute

> CallerArgumentExpression 属性での診断

You can specify a parameter that gets replaced with the argument's source text.

```cs
public static void Validate(bool condition, [CallerArgumentExpression("condition")] string? message = null)
{
    if (!condition)
    {
        throw new InvalidOperationException($"Argument failed validation: <{message}>");
    }
}
```

### Enhanced #line pragma

> 拡張 #line pragma

## .NET Library classes to Remember

### `LoggerMessageAttribute` class

- [LoggerMessageAttribute Class (Microsoft.Extensions.Logging) | Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/api/microsoft.extensions.logging.loggermessageattribute)
- [High-performance logging - .NET | Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/core/extensions/logging/high-performance-logging)
