# C# version 12.0

## Table of contents. <!-- omit in toc -->

- [Overview](#overview)
  - [Since](#since)
- [To study](#to-study)
- [Features](#features)
  - [Primary constructors](#primary-constructors)
  - [Collection expressions](#collection-expressions)
  - [ref readonly parameters](#ref-readonly-parameters)
  - [Default lambda parameters](#default-lambda-parameters)
  - [Alias any type](#alias-any-type)
  - [Inline arrays](#inline-arrays)
  - [Experimental attribute](#experimental-attribute)
  - [Interceptors](#interceptors)
- [.NET Library classes to Remember](#net-library-classes-to-remember)
  - [`TimeProvider` class](#timeprovider-class)

## Overview

- [What's new in C# 12 - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-12)
- [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-12)

### Since

- version 12.0
  - November 2023
  - .NET 8.0
  - Visual Studio 2022 version 17.8

## To study

- [Examples.Features.CSharp120.Tests](../../src/Examples.Features.CSharp120.Tests/)

## Features

### Primary constructors

> プライマリ コンストラクター

You can now define primary constructors on all `class` and `struct` types.

Primary constructors don't just reduce boilerplate member declarations.
They can also force other constructors to chain through the primary constructor.

Even though primary constructor parameters are in scope across the type, they still follow parameter-specific rules.

- Primary constructor parameters might not be stored if they are not needed.
- They are not class members, so you can't access them via `this.`.
- They can be assignment targets.
- Except for `record` types, they don't automatically become properties.

Typical uses of primary constructor parameters include:

- Passing arguments to `base()` constructors.
- Initializing member fields or properties.
- Referencing constructor parameters from instance members.

### Collection expressions

> コレクション式

Collection expressions introduce a concise new syntax for creating common collection values.

```cs
// Create an array:
int[] a = [1, 2, 3, 4, 5, 6, 7, 8];

// Create a list:
List<string> b = ["one", "two", "three"];

// Create a span
Span<char> c  = ['a', 'b', 'c', 'd', 'e', 'f', 'h', 'i'];

// Create a jagged 2D array:
int[][] twoD = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];

// Create a jagged 2D array from variables:
int[] row0 = [1, 2, 3];
int[] row1 = [4, 5, 6];
int[] row2 = [7, 8, 9];
int[][] twoDFromVariables = [row0, row1, row2];
```

The spread operator `..` in collection expressions expands its argument into collection elements.

```cs
int[] row0 = [1, 2, 3];
int[] row1 = [4, 5, 6];
int[] row2 = [7, 8, 9];
int[] single = [.. row0, .. row1, .. row2];
```

### ref readonly parameters

> `ref readonly` パラメーター

Adding `ref readonly` parameters makes APIs that use `ref` or `in` semantics clearer.

So the call-site rules look like this:

| Callsite annotation | ref parameter | ref readonly parameter | in parameter | out parameter |
| ------------------- | ------------- | ---------------------- | ------------ | ------------- |
| ref                 | Allowed       | Allowed                | Warning      | Error         |
| in                  | Error         | Allowed                | Allowed      | Error         |
| out                 | Error         | Error                  | Error        | Allowed       |
| No annotation       | Error         | Warning                | Allowed      | Error         |

For `ref readonly`, you get a warning when neither `ref` nor `in` is specified.

Rules for rvalues and lvalues are as follows:

| Value kind | ref parameter | ref readonly parameter | in parameter | out parameter |
| ---------- | ------------- | ---------------------- | ------------ | ------------- |
| rvalue     | Error         | Warning                | Allowed      | Error         |
| lvalue     | Allowed       | Allowed                | Allowed      | Allowed       |

With `ref readonly`, rvalue arguments produce a warning.

### Default lambda parameters

> 既定のラムダ パラメーター

You can now define default values for lambda parameters.

### Alias any type

> 任意の型の別名設定

Using alias directives can now target any type, not just named types.

What this means in practice:

- You can put keywords like `int` and `string` on the right side of a using alias.
- You can also alias tuple, pointer, array, and other types using normal C# type syntax.

### Inline arrays

> インライン配列

An inline array is a struct that contains a contiguous block of `N` elements of the same type.
It's basically a safe-code alternative to fixed buffer declarations that previously required unsafe code.
Inline arrays are `struct` types with these characteristics:

- They contain exactly one field.
- Their layout is not explicitly specified.

This feature mainly targets performance-focused runtime and library authors.

### Experimental attribute

> 試験段階の属性

You can mark types, methods, or assemblies with
`System.Diagnostics.CodeAnalysis.ExperimentalAttribute` to indicate experimental features.

### Interceptors

> インターセプター

*Interceptors are experimental and available in C# 12 preview mode.*

Pending.

## .NET Library classes to Remember

### `TimeProvider` class

- [TimeProvider Class (System) | Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/api/system.timeprovider)
