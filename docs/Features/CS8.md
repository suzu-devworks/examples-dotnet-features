# C# 8.0 Features

* https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-80

## Since

- 8.0
  - September 2019
  - .NET COre 3.0, 3.1
  - Visual Studio 2019 version 16.3

## Table of features.

### Readonly members

> 読み取り専用メンバー


### Default interface methods

> インターフェイスのデフォルトメソッド


### Pattern matching enhancements:

> パターン マッチングの拡張機能

* [Test Code ...](/src/Examples.Features.CS8.Tests/CS8/PatternMatching/UnitTests.cs)

* Switch expressions
* Property patterns
* Tuple patterns
* Positional patterns


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

* [Test Code ...](/src/Examples.Features.CS8.Tests/CS8/RangeAndIndices/UnitTests.cs)

インデックスと範囲には、シーケンス内の 1 つの要素または範囲にアクセスできる簡潔な構文が用意されています。

この言語のサポートでは、次の 2 つの新しい型と 2 つの新しい演算子を使用しています。

* [`System.Index`] はシーケンスとしてインデックスを表します。
* index from end 演算子の `^`、シーケンスの末尾から相対的なインデックスを指定します。
* [`System.Range`] はシーケンスのサブ範囲を表します。
* 範囲演算子の `..`、範囲の先頭と末尾をそのオペランドとして指定します。


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
