# C# version 4.0

## Table of contents. <!-- omit in toc -->

- [Overview](#overview)
  - [Since](#since)
- [To study](#to-study)
- [Features](#features)
  - [Dynamic binding](#dynamic-binding)
  - [Named and optional arguments](#named-and-optional-arguments)
  - [Generic Co- and Contra- variance](#generic-co--and-contra--variance)
  - [Embedded interop types](#embedded-interop-types)

## Overview

- [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-40)

### Since

- April 2010
- .NET Framework 4
- Visual Studio 2010

## To study

- [Examples.Features.CSharp40.Tests](../../src/Examples.Features.CSharp40.Tests/)

## Features

### Dynamic binding

> 動的型付け変数

**The main star of C# 4.0.**

This feature appears to have been introduced in anticipation of
the DLR (Dynamic Language Runtime) adoption trend.

With `dynamic`, you can do things like:

- Late binding
- Duck typing
- Calling static methods when working with generics
- Multiple dispatch

### Named and optional arguments

> オプション引数、名前付き引数

### Generic Co- and Contra- variance

> ジェネリックの共変性と反変性

In type systems, covariance, contravariance, and invariance are defined like this:

- `Covariance`
  - Lets you use a more specific (stronger) derived type than the one originally specified.
  - for example, `IEnumerable<T>`, `IEnumerator<T>`, `IQueryable<T>`, and `IGrouping<TKey,TElement>`.
- `Contravariance`
  - Lets you use a more general (weaker) type than the one originally specified.
  - for example: `IComparer<T>`, `IComparable<T>`, and `IEqualityComparer<T>`.
- `Invariance`
  - Means only the originally specified type can be used.

### Embedded interop types

> COM(Embedded) 相互運用型の特別処理

It seems COM interop classes (Runtime Callable Wrappers) got special handling to make COM calls easier.

- `ref` became optional for COM interface arguments (since everything used to be `ref` all the time).
- Indexed properties on COM objects became usable (`get_X(index)`, `Set_X(index, value)` => `X[index]`).
