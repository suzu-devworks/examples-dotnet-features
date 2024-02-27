# Examples.Features.CS71.Tests

## C# version 7.1

### See also

* [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-71)

### Since

- August 2017
- .NET Core 2.0
- Visual Studio 2017 version 15.3


## Table of contents. <!-- omit in toc -->

- [Examples.Features.CS71.Tests](#examplesfeaturescs71tests)
  - [C# version 7.1](#c-version-71)
    - [See also](#see-also)
    - [Since](#since)
  - [Features](#features)
    - [async Main method](#async-main-method)
    - [default literal expressions](#default-literal-expressions)
    - [Inferred tuple element names](#inferred-tuple-element-names)
    - [Pattern matching on generic type parameters](#pattern-matching-on-generic-type-parameters)


## Features

### async Main method

> 非同期 `Main`

アプリケーションのエントリ ポイントに `async` 修飾子を設定できます。

```cs
// C# 7.0
public static void Main(string[] args)
{
    MainAsync().GetAwaiter().GetResult();

    async Task MainAsync()
    {
        Console.WriteLine("Hello world");
        await Task.CompletedTask;
   }
}

// C# 7.1 or later
public static Task Main()
{
}

// or

public static Task<int> Main()
{
}

// or 

public static Task Main(string[] args)
{
}

// or 

public static Task<int> Main(string[] args)
{

}
```


### default literal expressions

> `default` 式

これまでも既定値を作るために、`default(T)` という構文がありましたが、`default` 式になったので簡略化しました。

default リテラルは、次のいずれの場合でも使用できます。

- 変数の代入または初期化。
- 省略可能なメソッド パラメーターの既定値の宣言。
- メソッド呼び出しでの引数値の指定。
- return ステートメント内、または式のようなメンバー内の式として。


### Inferred tuple element names

> タプル要素名の推論

C# 7.0で追加したタプルの要素名が、タプル構築時に渡した変数から推論できるようになりました。


### Pattern matching on generic type parameters

> ジェネリック型に対するパターンマッチング

型がジェネリック型パラメーターである変数にパターン マッチ式を使用できます。

