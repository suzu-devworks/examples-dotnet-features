# C# version 8.0

## Table of contents. <!-- omit in toc -->

- [Overview](#overview)
  - [Since](#since)
- [To study](#to-study)
- [Features](#features)
  - [Readonly members](#readonly-members)
  - [Default interface methods](#default-interface-methods)
  - [Pattern matching enhancements](#pattern-matching-enhancements)
  - [Using declarations](#using-declarations)
  - [Static local functions](#static-local-functions)
  - [Disposable ref structs](#disposable-ref-structs)
  - [Nullable reference types](#nullable-reference-types)
  - [Asynchronous streams](#asynchronous-streams)
  - [Asynchronous disposable](#asynchronous-disposable)
  - [Indices and ranges](#indices-and-ranges)
  - [Null-coalescing assignment](#null-coalescing-assignment)
  - [Unmanaged constructed types](#unmanaged-constructed-types)
  - [Stackalloc in nested expressions](#stackalloc-in-nested-expressions)
  - [Enhancement of interpolated verbatim strings](#enhancement-of-interpolated-verbatim-strings)

## Overview

- [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-80)

### Since

- September 2019
- .NET Core 3.0
- .NET Core 3.1
- Visual Studio 2019 version 16.3

## To study

- [Examples.Features.CSharp80.Tests](../../src/Examples.Features.CSharp80.Tests/)

## Features

### Readonly members

> 読み取り専用メンバー

### Default interface methods

> インターフェイスのデフォルトメソッド

### Pattern matching enhancements

> パターン マッチングの拡張機能

- Switch expressions
- Property patterns
- Tuple patterns
- Positional patterns

### Using declarations

> using 宣言 (IDisposable)

### Static local functions

> 静的ローカル関数

### Disposable ref structs

> 破棄可能な `ref` 構造体

### Nullable reference types

> Null 許容参照型

### Asynchronous streams

> 非同期ストリーム

### Asynchronous disposable

> 非同期の破棄可能

### Indices and ranges

> インデックスと範囲

Indices and ranges provide concise syntax for accessing a single element or a slice within a sequence.

This language support uses two new types and two new operators:

- [`System.Index`] represents an index into a sequence.
- The index-from-end operator `^` specifies an index relative to the end of a sequence.
- [`System.Range`] represents a sub-range of a sequence.
- The range operator `..` specifies the start and end of a range.

### Null-coalescing assignment

> null 合体割り当て

```cs
List<int> numbers = null;
int? i = null;

numbers ??= new List<int>();  // create List.
numbers.Add(i ??= 17);        // set 17.
numbers.Add(i ??= 20);        // not set 20 (i == 17).
```

### Unmanaged constructed types

> 構築されたアンマネージド型

### Stackalloc in nested expressions

> 入れ子になった式の stackalloc

### Enhancement of interpolated verbatim strings

> verbatim 補間文字列の拡張
