# Examples.Features.CS90.Tests

## C# version 9.0

### See also

* [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-9)

### Since

- September 2020
- .NET 5.0
- Visual Studio 2019 version 16.8, Preview 4


## Table of contents. <!-- omit in toc -->

- [Examples.Features.CS90.Tests](#examplesfeaturescs90tests)
  - [C# version 9.0](#c-version-90)
    - [See also](#see-also)
    - [Since](#since)
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


## Features

### Records

> レコード型

データをカプセル化するための組み込み機能を提供する参照型を定義します。

* レコード型には次の機能があります。
  * 不変プロパティを持つ参照型を作成するための簡潔な構文
  * データ中心の参照型に役立つ Behavior:
    * Value equality (operator ==)
    * Concise syntax for nondestructive mutation (with式)
    * Built-in formatting for display (ToStringメソッド)
  * 継承階層のサポート


### Init only setters

> 初期化専用セッター

init のみのセッターでは、オブジェクトの構築時のみ、プロパティまたはインデクサー要素に値が割り当てられます。with 式も含まれるようです。


### Top-level statements

> トップレベルステートメント

小規模なコンソール プログラムとユーティリティの Main() として使用できるようです。

```cs
// C# 9.0 or later
using System;

Console.WriteLine("Hello World!");
```


### Pattern matching enhancements

> パターンマッチングの拡張


### Performance and interop

> パフォーマンスと相互運用

* Native sized integers (ネイティブ サイズの整数)
* Function pointers (関数ポインター)
* Suppress emitting localsinit flag (localsinit フラグの出力を抑制する)


<!-- ----- -->
### Fit and finish features

> 適合性と完成度の機能

#### Target-typed new expressions

> new式 の型推論

型がわかっている場合は、コンストラクターの型指定を必要としません。

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

 モジュール初期化子は、ModuleInitializerAttribute 属性が関連付けられているメソッドです。 これらのメソッドは、全体モジュール内の他のフィールド アクセスまたはメソッド呼び出しの前にランタイムによって呼び出されます。

  * モジュール初期化子メソッドは次のようなものです。
    * Must be ```static```
    * Must be parameter-less
    * Must return ```void```
    * Must not be a generic method
    * Must not be contained in a generic class
    * Must be accessible from the containing module


#### New features for partial methods

> 部分メソッドの新機能

