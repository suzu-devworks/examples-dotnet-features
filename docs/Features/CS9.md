# C# 9.0 Features

* https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-9

## Since

- 9.0
  - September 2020
  - .NET 5.0
  - Visual Studio 2019 version 16.8, Preview 4

## Table of features.

### Records

> レコード型

* [Test Code ...](/src/Examples.Features.CS9.Tests/CS9/Records/UnitTests.cs)

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

* [Test Code ...](/src/Examples.Features.CS9.Tests/CS9/InitOnlySetters/UnitTests.cs)

init のみのセッターでは、オブジェクトの構築時のみ、プロパティまたはインデクサー要素に値が割り当てられます。with 式も含まれるようです。


### Top-level statements

> トップレベルステートメント

小規模なコンソール プログラムとユーティリティの Main() として使用できるようです。

```cs
// CS9.0
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
// CS9.0
List<string> lines = new();

//C#8.0
var lines = new List<string>();
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

> コード ジェネレーターのサポート

#### Module initializers

> モジュールの初期化子

* [Test Code ...](/src/Examples.Features.CS9.Tests/CS9/ModuleInitializers/UnitTests.cs)

 モジュール初期化子は、ModuleInitializerAttribute 属性が関連付けられているメソッドです。 これらのメソッドは、全体モジュール内の他のフィールド アクセスまたはメソッド呼び出しの前にランタイムによって呼び出されます。

  * モジュール初期化子メソッドは次のようなものです。
    * Must be ```static```
    * Must be parameterless
    * Must return ```void```
    * Must not be a generic method
    * Must not be contained in a generic class
    * Must be accessible from the containing module


#### New features for partial methods

> 部分メソッドの新機能

