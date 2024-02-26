# Examples.Features.CS60.Tests

## C# version 6.0

### See also

* [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-60)

### Since

- July 2015
- .NET Framework 4.6
- .NET Core 1.0
- .NET Core 1.1
- Visual Studio 2015


## Table of contents. <!-- omit in toc -->

- [Examples.Features.CS60.Tests](#examplesfeaturescs60tests)
  - [C# version 6.0](#c-version-60)
    - [See also](#see-also)
    - [Since](#since)
  - [Features](#features)
    - [Compiler-as-a-service (Roslyn)](#compiler-as-a-service-roslyn)
    - [Static Imports](#static-imports)
    - [Exception filters](#exception-filters)
    - [Await in catch/finally blocks](#await-in-catchfinally-blocks)
    - [Auto property initializers](#auto-property-initializers)
    - [Default values for getter-only properties](#default-values-for-getter-only-properties)
    - [Expression-bodied members](#expression-bodied-members)
    - [Null propagator (null-conditional operator, succinct null checking)](#null-propagator-null-conditional-operator-succinct-null-checking)
    - [String interpolation](#string-interpolation)
    - [nameof operator](#nameof-operator)
    - [Index initializers](#index-initializers)
    - [Extension Add methods in collection initializers](#extension-add-methods-in-collection-initializers)


## Features

### Compiler-as-a-service (Roslyn)

> .NET Compiler Platform, better known by its codename "Roslyn", is a set of open-source compilers and code analysis APIs for C# and Visual Basic .NET languages from Microsoft.

* https://github.com/dotnet/roslyn

**C#6.0はこれが主役**

コンパイラをC#で作り直したことにより、コード解析などいろいろできるようになったらしい。


### Static Imports

> `using static` ステートメント

```cs
// C# 6.0 or later
using static System.Console;
using static System.Math;

public static void Main(string[] args)
{
  WriteLine(Sqrt(3*3 + 4*4));
}
```


### Exception filters

> 例外フィルタ

```cs
// C# 6.0 or later
  var client = new HttpClient();
  try
  {
      var responseText = await client.GetStringAsync("https://localHost:10000");

      return responseText;
  }
  catch (HttpRequestException e) when (e.Message.Contains("301"))
  {
      return "Site Moved";
  }
  catch (HttpRequestException e) when (e.Message.Contains("404"))
  {
      return "Page Not Found";
  }
  catch (HttpRequestException e)
  {
      return e.Message;
  }
```

これは使える・・・


### Await in catch/finally blocks

> `catch`/`finally` での `await`

```cs
// C# 6.0 or later
  try
  {
      await SomeAsyncMethod();
  }
  catch (InvalidOperationException e)
  {
      using (var s = new StreamWriter("error.txt"))
          await s.WriteAsync(e.ToString());
  }
  finally
  {
      using (var s = new StreamWriter("trace.txt"))
          await s.WriteAsync("XAsync done.");
  }
```

制限あったんだ・・・


### Auto property initializers

> 自動実装プロパティの初期化子

```cs
// Older
private int _value = 100;
public int Value
{
  get { return _value; }
  set { this._value = value; }
}

// C# 6.0 or later
public Constructor()
{
  this.Value = 100;
}
public int Value { get; private set; }

// C# 6.0 or later
public int Value { get; } = 100;

```


### Default values for getter-only properties

> get のみの自動実装プロパティおよびコンストラクタ代入

```cs
// C# 6.0 or later
public Constructor()
{
  this.Value = 100;
}

public int Value { get; }

```


### Expression-bodied members

> Expression-bodied メンバ

`=>` を使った簡易文法で関数定義できるようになりました。

関数、読み取り専用 (getter のみ) のプロパティで使えるます。

```cs
// C# 6.0 or later
public int Count
{
    get { return _vertexes.Length; }
}

public int InnerProduct(Point p)
{
    return X * p.X + Y * p.Y;
}

// C# 6.0 or later
public int Count => _vertexes.Length;

public int InnerProduct(Point p) => X * p.X + Y * p.Y;

```


### Null propagator (null-conditional operator, succinct null checking)

> null 条件演算子

```cs
// C# 6.0 or later
int? len = null;
if (obj != null)
{
  if (obj.Name != null)
  {
    len = obj.Name.Length
  }
}

// C# 6.0 or later
var len = obj?.Name?.Length

var element = arr?[index];

var result = func?.Invoke();
```

nullチェックを書くのがめんどくさくなってきた。


### String interpolation

> 文字列補間（テンプレート文字列）

```cs
// C# 6.0 or later
var formatted = string.Format("({0}, {1})", x, y);

// C# 6.0 or later
var formatted = $"({x}, {y})";
```


### nameof operator

> nameof 演算子

```cs
// C# 6.0 or later

nameof(MyClass));
// output "MyClass"

nameof(MyNamespace.MyClass))
// output "MyClass" muu...
```

よく使う！

`PropertyChangedEventArgs` は `nameof` と C#5.0の `[CallerMemberName]`とどっちがいいだろう・・・


### Index initializers

> Index 初期化子

Dictionary の初期化が直観的になった。"Index"　なので Dictionary に限ったことではない。


### Extension Add methods in collection initializers

> コレクション初期化子内でのAdd拡張メソッドの利用

C# 3.0 で「コレクション初期化子」が追加されましたが、これは、Add メソッドの呼び出しに展開されるものです。これまでは、Add は通常のメソッドでないといけませんでした。 これが、C# 6 で、拡張メソッドでもよくなりました。

