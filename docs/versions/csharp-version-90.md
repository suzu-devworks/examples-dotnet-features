# C# version 9.0

## Table of contents. <!-- omit in toc -->

- [Overview](#overview)
  - [Since](#since)
- [To study](#to-study)
- [Features](#features)
  - [Records](#records)
  - [Init only setters](#init-only-setters)
  - [Top-level statements](#top-level-statements)
  - [Pattern matching enhancements](#pattern-matching-enhancements)
  - [Performance and interop](#performance-and-interop)
  - [Fit and finish features](#fit-and-finish-features)
    - [Target-typed new expressions](#target-typed-new-expressions)
    - [static anonymous functions](#static-anonymous-functions)
    - [Target-typed conditional expressions](#target-typed-conditional-expressions)
    - [Covariant return types](#covariant-return-types)
    - [Extension GetEnumerator support for foreach loops](#extension-getenumerator-support-for-foreach-loops)
    - [Lambda discard parameters](#lambda-discard-parameters)
    - [Attributes on local functions](#attributes-on-local-functions)
  - [Support for code generators](#support-for-code-generators)
    - [Module initializers](#module-initializers)
    - [New features for partial methods](#new-features-for-partial-methods)

## Overview

- [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-9)

### Since

- September 2020
- .NET 5.0
- Visual Studio 2019 version 16.8, Preview 4

## To study

- [Examples.Features.CSharp90.Tests](../../src/Examples.Features.CSharp90.Tests/)

## Features

### Records

> レコード型

Defines reference types with built-in support for encapsulating data.

- Record types provide features like:
  - Concise syntax for creating reference types with immutable properties
  - Useful behavior for data-centric reference types:
    - Value equality (operator ==)
    - Concise syntax for nondestructive mutation (`with` expressions)
    - Built-in display formatting (`ToString` method)
  - Support for inheritance hierarchies

### Init only setters

> 初期化専用セッター

With init-only setters, values can be assigned to properties or indexers
only during object construction (including `with` expressions).

### Top-level statements

> トップレベルステートメント

This is well-suited to small console applications and utility-style `Main()` programs.

```cs
// C# 9.0 or later
using System;

Console.WriteLine("Hello World!");
```

### Pattern matching enhancements

> パターンマッチングの拡張

- Relational patterns
- Logical patterns

### Performance and interop

> パフォーマンスと相互運用

- Native-sized integers
- Function pointers
- Suppress emitting the `localsinit` flag

<!-- ----- -->
### Fit and finish features

> 適合性と完成度の機能

#### Target-typed new expressions

> new式 の型推論

When the target type is known, you don't need to repeat the constructor type.

```cs
//C# 8.0
var lines = new List<string>();

// C# 9.0 later
List<string> lines = new();
```

#### static anonymous functions

> 静的匿名関数

#### Target-typed conditional expressions

> 条件式（3項演算子）の型推論

#### Covariant return types

> 戻り値の共変性

#### Extension GetEnumerator support for foreach loops

> foreach ループの拡張機能 GetEnumerator サポート

#### Lambda discard parameters

> ラムダ式のパラメータ廃棄

#### Attributes on local functions

> ローカル関数の属性

<!-- ----- -->
### Support for code generators

> コード ジェネレーターのサポート1

#### Module initializers

> モジュールの初期化子

A module initializer is a method associated with `ModuleInitializerAttribute`.
The runtime invokes these methods before any other field access or method call in the module.

- Module initializer methods must be:
  - Must be ```static```
  - Must be parameter-less
  - Must return ```void```
  - Must not be a generic method
  - Must not be contained in a generic class
  - Must be accessible from the containing module

#### New features for partial methods

> 部分メソッドの新機能
