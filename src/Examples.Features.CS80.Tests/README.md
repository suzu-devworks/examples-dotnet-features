# Examples.Features.CS80.Tests

## C# version 8.0

### See also

* [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-80)

### Since

- September 2019
- .NET Core 3.0
- .NET Core 3.1
- Visual Studio 2019 version 16.3


## Table of contents. <!-- omit in toc -->

- [Examples.Features.CS80.Tests](#examplesfeaturescs80tests)
  - [C# version 8.0](#c-version-80)
    - [See also](#see-also)
    - [Since](#since)
  - [Features](#features)
    - [\*Readonly members](#readonly-members)
    - [\*Default interface methods](#default-interface-methods)
    - [Pattern matching enhancements](#pattern-matching-enhancements)
    - [\*Using declarations](#using-declarations)
    - [\*Static local functions](#static-local-functions)
    - [\*Disposable ref structs](#disposable-ref-structs)
    - [\*Nullable reference types](#nullable-reference-types)
    - [\*Asynchronous streams](#asynchronous-streams)
    - [\*Asynchronous disposable](#asynchronous-disposable)
    - [Indices and ranges](#indices-and-ranges)
    - [\*Null-coalescing assignment](#null-coalescing-assignment)
    - [\*Unmanaged constructed types](#unmanaged-constructed-types)
    - [\*Stackalloc in nested expressions](#stackalloc-in-nested-expressions)
    - [\*Enhancement of interpolated verbatim strings](#enhancement-of-interpolated-verbatim-strings)


## Features

### *Readonly members

> 読み取り専用メンバー


### *Default interface methods

> インターフェイスのデフォルトメソッド


### Pattern matching enhancements

> パターン マッチングの拡張機能

* Switch expressions
* Property patterns
* Tuple patterns
* Positional patterns


### *Using declarations

> using 宣言 (IDisposable)


### *Static local functions

> 静的ローカル関数


### *Disposable ref structs

> 破棄可能な `ref` 構造体


### *Nullable reference types

> Null 許容参照型


### *Asynchronous streams

> 非同期ストリーム


### *Asynchronous disposable

> 非同期の破棄可能


### Indices and ranges

> インデックスと範囲

インデックスと範囲には、シーケンス内の 1 つの要素または範囲にアクセスできる簡潔な構文が用意されています。

この言語のサポートでは、次の 2 つの新しい型と 2 つの新しい演算子を使用しています。

* [`System.Index`] はシーケンスとしてインデックスを表します。
* index from end 演算子の `^`、シーケンスの末尾から相対的なインデックスを指定します。
* [`System.Range`] はシーケンスのサブ範囲を表します。
* 範囲演算子の `..`、範囲の先頭と末尾をそのオペランドとして指定します。


### *Null-coalescing assignment

> null 合体割り当て

```cs
List<int> numbers = null;
int? i = null;

numbers ??= new List<int>();  // create List.
numbers.Add(i ??= 17);        // set 17.
numbers.Add(i ??= 20);        // not set 20 (i == 17).
```


### *Unmanaged constructed types

> 構築されたアンマネージド型

### *Stackalloc in nested expressions

> 入れ子になった式の stackalloc

### *Enhancement of interpolated verbatim strings

> verbatim 補間文字列の拡張
