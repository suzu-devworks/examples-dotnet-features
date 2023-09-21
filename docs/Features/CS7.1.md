# C# 7.1 Features

* https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-71

## Since

- 7.1
  - August 2017
  - .NET Core 2.0
  - Visual Studio 2017 version 15.3

## Table of features

### async Main method

> 非同期 Main

アプリケーションのエントリ ポイントに `async` 修飾子を設定できます。

```cs
// CS7.0
static void Main(string[] args)
{
    MainAsync(args).GetAwaiter().GetResult();
}
static async Task MainAsync(string[] args)
{
    // asynchronous code
}

// CS7.1～
public static Task Main();
public static Task<int> Main();
public static Task Main(string[] args);
public static Task<int> Main(string[] args);
```


### default literal expressions

>　default 式

これまでも既定値を作るために、`default(T)` という構文がありましたが、`default` 式になったので簡略化と

```cs
// CS7.0
int numeric = default(int);         // = 0
Object reference = default(Object); // = null
DateTime value = default(DateTime); // = new DateTime()
CancellationToken token = default(CancellationToken);

// CS7.1～
int numeric = default;
Object reference = default;
DateTime value = default;
CancellationToken token = default;
T defaultValue = default;
```


### Inferred tuple element names

> タプル要素名の推論

C# 7.0で追加したタプルの要素名が、タプル構築時に渡した変数から推論できるようになりました。

```cs
// CS7.0
var coords1 = (x: x, y: y);
var x1 = coords1.x;

var coords2 = (x, y);
var x2 = coords2.Item1; // coords2.x didn't compile

// CS7.1～
var coords2 = (x, y);
var x2 = coords2.x;
```


### Pattern matching on generic type parameters

> ジェネリック型に対するパターンマッチング

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7minor1/PatternMatching/UnitTests.cs)

C# 7.0で `is` や `switch` で型を見ての分岐ができるようになりました。
しかし、ジェネリクスが絡む場合にはエラーになっていました。
