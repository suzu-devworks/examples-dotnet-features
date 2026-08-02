# C# version 15.0

## Table of contents. <!-- omit in toc -->

- [Overview](#overview)
  - [Since](#since)
- [Features](#features)
  - [Collection expression arguments](#collection-expression-arguments)
  - [Union types](#union-types)
  - [Closed hierarchies](#closed-hierarchies)
  - [Extension indexers](#extension-indexers)
  - [Labeled break and continue](#labeled-break-and-continue)
  - [Memory safety](#memory-safety)

## Overview

- [What's new in C# 15 - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-15)

### Since

- version 15.0
  - (Before release)
  - .NET 11.0
  - Visual Studio 2026 version ???

## Features

### Collection expression arguments

> コレクション式の引数

By using a `with(...)` element as the first item in a collection expression,
you can pass arguments to the underlying collection constructor or factory method.

### Union types

> ユニオン型

Union types represent a value that can be one of multiple case types.
You declare a union using the `union` keyword.

### Closed hierarchies

> 閉じた階層

You can declare a closed inheritance hierarchy by applying the `closed` modifier to a class.

A `closed` class can only be derived from within the assembly where it is declared, which means the set of direct
 derived classes is determined at compile time.

In the context of pattern matching, this means that if the classes are related within the same assembly,
 you do not need to write a `default` clause provided that a `switch` expression covers all of them.

### Extension indexers

> 拡張機能インデクサ

you can declare indexers in an extension block. Extension indexers let you index into a receiver
 as though the indexer were declared on the receiver type.

### Labeled break and continue

> ラベル付き break と continue

break and continue statements can name a label on an enclosing construct.

Could it be that it was still "goto" all this time?

### Memory safety

> メモリの安全性
