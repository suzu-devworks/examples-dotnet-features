# Examples.Features.CS120.Tests

## C# 12.0 Features

* [What's new in C# 12 - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-12)

## Since

- version 12.0
  - November 2023
  - .NET 8.0
  - Visual Studio 2022 version 17.8

## Table of contents. <!-- omit in toc -->

- [Examples.Features.CS120.Tests](#examplesfeaturescs120tests)
  - [C# 12.0 Features](#c-120-features)
  - [Since](#since)
  - [Features](#features)
    - [Primary constructors](#primary-constructors)
    - [Collection expressions](#collection-expressions)
    - [ref readonly parameters](#ref-readonly-parameters)
    - [Default lambda parameters](#default-lambda-parameters)
    - [Alias any type](#alias-any-type)
    - [Inline arrays](#inline-arrays)
    - [Experimental attribute](#experimental-attribute)
    - [\*Interceptors](#interceptors)

## Features

### Primary constructors

> プライマリ コンストラクター

すべての class と struct で、プライマリ コンストラクターを作成できるようになりました。

プライマリコンストラクターを使用する場合には面倒なメンバー変数の宣言を省略するだけではなく、
他のコンストラクターを宣言する場合にプライマリコンストラクターの呼び出しを強制することができます。

プライマリ コンストラクター パラメーターは、クラス定義全体のスコープ内にある場合でも、パラメーターとして扱うためのルールがあります。

- プライマリ コンストラクター パラメーターは、必要がない場合には保存されない場合があります。
- プライマリ コンストラクター パラメーターはクラスのメンバーではありません。`this.` ではアクセスできません。
- プライマリ コンストラクター パラメーターは割当先になることができます。
- `record` 型の場合を除き、プライマリ コンストラクター パラメーターはプロパティにはなりません。

プライマリ コンストラクター パラメーターの主な用途は次のとおりです。

- base()コンストラクターを呼び出す際の引数とします。
- メンバーフィールドまたはプロパティを初期化します。
- インスタンスメンバーからコンストラクターパラメータを参照します。


### Collection expressions

> コレクション式

コレクション式は、共通のコレクション値を作成するための新しい簡潔な構文を導入します。

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

コレクション式のスプレッド演算子である `..` は、その引数をコレクションの要素に置き換えます。

```cs
int[] row0 = [1, 2, 3];
int[] row1 = [4, 5, 6];
int[] row2 = [7, 8, 9];
int[] single = [.. row0, .. row1, .. row2];
```


### ref readonly parameters

> `ref readonly` パラメーター

`ref readonly` パラメーターが追加されたことにより、`ref` パラメーターまたは `in` パラメーターを使用する API がより明確になります。

これによりコールサイト ルールは次のようになります:

| Callsite annotation | ref parameter | ref readonly parameter | in parameter | out parameter |
| ------------------- | ------------- | ---------------------- | ------------ | ------------- |
| ref                 | Allowed       | Allowed                | Warning      | Error         |
| in                  | Error         | Allowed                | Allowed      | Error         |
| out                 | Error         | Error                  | Error        | Allowed       |
| No annotation       | Error         | Warning                | Allowed      | Error         |

`ref readonly` を指定すると `ref` または `in` が無い場合には警告が表示されます。

右辺値、左辺値のルールは次のとおりです：

| Value kind | ref parameter | ref readonly parameter | in parameter | out parameter |
| ---------- | ------------- | ---------------------- | ------------ | ------------- |
| rvalue     | Error         | Warning                | Allowed      | Error         |
| lvalue     | Allowed       | Allowed                | Allowed      | Allowed       |

`ref readonly` を指定すると 右辺値参照で警告が表示されます。


### Default lambda parameters

> 既定のラムダ パラメーター

ラムダ式のパラメーターに既定値を定義できるようになりました。


### Alias any type

> 任意の型の別名設定

using 別名ディレクティブを使うと、名前付き型だけでなく、任意の型に別名を設定できます。

何ができるようになったかというと:

- `int`, `string` などキーワードを using エイリアスの右辺に書けるようになった
- タプル型、ポインター型、配列型などが、C# の専用構文を使って書けるようになった


### Inline arrays

> インライン配列

インライン配列は、同じ型の N 個の要素によるブロックが連続して含まれている構造体のことです。 
これは、安全でないコードでのみ使用できる 固定バッファー 宣言を、安全なコードとして記述し直したものです。 
インライン配列は、 次の特徴を持つ struct です:

- 含まれているフィールドは 1 つです。
- この構造体では、レイアウトが明示的に指定されません。

ランタイムやライブラリ作成者向きのパフォーマンス向上を目的とした機能です。


### Experimental attribute

> 試験段階の属性

型、メソッド、またはアセンブリには、試験的機能を示す `System.Diagnostics.CodeAnalysis.ExperimentalAttribute` マークを付けることができます。

### *Interceptors

> インターセプター

> *インターセプターは試験的な機能であり、C# 12 のプレビュー モードで使用できます。*

保留。
