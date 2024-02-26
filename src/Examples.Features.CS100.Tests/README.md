# Examples.Features.CS100.Tests

## C# version 10.0

### See also

* [What's new in C# 10 - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-10)

### Since

- November 2021
- .NET 6.0
- Visual Studio 2022 version 17.0 Preview


## Table of contents. <!-- omit in toc -->

- [Examples.Features.CS100.Tests](#examplesfeaturescs100tests)
  - [C# version 10.0](#c-version-100)
    - [See also](#see-also)
    - [Since](#since)
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


## Features

### Record structs

> レコード構造体

### Improvements of structure types

> 構造体型の機能強化

* パラメータなしのコンストラクタ


### Interpolated string handlers

> 補間された文字列ハンドラー


### global using directives

> グローバルな using ディレクティブ

* [Implicit Using directives](https://learn.microsoft.com/ja-jp/dotnet/core/tutorials/top-level-templates#implicit-using-directives)

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

ネストが一階層減りました。

```cs
namespace MyNamespace;

```


### Extended property patterns

> 拡張プロパティのパターン

入れ子になったプロパティまたはプロパティ パターン内のフィールドを参照できます。

```cs
// C# 8.0 or later
data is { Prop1: { Prop2: pattern } }

// C# 10.0 or later
data is { Prop1.Prop2: pattern }

```


### Improvements on lambda expressions

> ラムダ式の機能強化


### Allow const interpolated strings

> 定数の補間文字列

使用するのが const ならば文字列補完がつかえるようになりました。

```cs
// C# 10.0 or later
const string Language = "C#";
const string Platform = ".NET";
const string Version = "10.0";
const string FullProductName = $"{Platform} - Language: {Language} Version: {Version}";
```


### Record types can seal ToString()

> レコードの型で ToString を sealed することができる。


### Assignment and declaration in same deconstruction

> 同じ Deconstractor 内で宣言と代入を同時に実行できる。

```cs
// C# 9.0 or later
// Initialization:
(int x, int y) = point;

// assignment:
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

引数のテキスト表現に置き換えられるパラメーターを指定できます。

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

