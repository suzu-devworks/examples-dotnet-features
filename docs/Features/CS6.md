# C# 6.0 Features

* https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-60

## Since

- 6.0
  - July 2015
  - .NET Framework 4.6
  - Visual Studio 2015

## Table of features.

### Compiler-as-a-service (Roslyn)

> .NET Compiler Platform, better known by its codename "Roslyn", is a set of open-source compilers and code analysis APIs for C# and Visual Basic .NET languages from Microsoft.

* https://github.com/dotnet/roslyn

**C#6.0はこれが主役**

コンパイラをC#で作り直したことにより、コード解析などいろいろできるようになりました。


### Static Imports

> `using static` ステートメント

```cs
// CS6.0～
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
// CS6.0～
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
// CS6.0～
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

// CS3.0～
public Constructor()
{
  this.Value = 100;
}
public int Value { get; private set; }

// CS6.0～
public int Value { get; } = 100;

```


### Default values for getter-only properties

> get のみの自動実装プロパティおよびコンストラクタ代入

```cs
// CS6.0～
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
// CS5.0
public int Count
{
    get { return _vertexes.Length; }
}

public int InnerProduct(Point p)
{
    return X * p.X + Y * p.Y;
}

// CS6.0～
public int Count => _vertexes.Length;

public int InnerProduct(Point p) => X * p.X + Y * p.Y;

```


### Null propagator (null-conditional operator, succinct null checking)

> null 条件演算子

```cs
// CS5.0
int? len = null;
if (obj != null)
{
  if (obj.Name != null)
  {
    len = obj.Name.Length
  }
}

// CS6.0～
var len = obj?.Name?.Length

var element = arr?[index];

var result = func?.Invoke();
```

nullチェックを書くのがめんどくさくなってきた。


### String interpolation

> 文字列補間（テンプレート文字列）

```cs
// CS5.0
var formatted = string.Format("({0}, {1})", x, y);

// CS6.0～
var formatted = $"({x}, {y})";
```


### nameof operator

> nameof 演算子

```cs
// CS6.0～

nameof(MyClass));
// output "MyClass"

nameof(MyNamespace.MyClass))
// output "MyClass" muu...
```

よく使う！

`PropertyChangedEventArgs` は `nameof` と C#5.0の `[CallerMemberName]`とどっちがいいだろう・・・


### Index initializers

> Index 初期化子

* [Test Code ...](/src/Examples.Features.CS6.Tests/CS6/IndexInitializers/UnitTests.cs)

Dictionalyの初期化が直観的になった。"Index"なのでDictionalyに限ったことではない。


### Extension Add methods in collection initializers

> コレクション初期化子内でのAdd拡張メソッドの利用

* [Test Code ...](/src/Examples.Features.CS6.Tests/CS6/CollectionInitilizers/UnitTests.cs)

C# 3.0 で「コレクション初期化子」が追加されましたが、これは、Add メソッドの呼び出しに展開されるものです。これまでは、Add は通常のメソッドでないといけませんでした。 これが、C# 6 で、拡張メソッドでもよくなりました。

