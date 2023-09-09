# C# 5.0 Features

* https://docs.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-50

## Since

- 5.0
  - August 2012
  - .NET Framework 4.5
  - Visual Studio 2012, 2013


## Table of features.

### Asynchronous methods

> `async` / `await` キーワードによる非同期関数の実装

**C# 5.0 の主役。**

```cs
// CS5.0～
public async Task FunctionAsync(CancellationToken cencelToken)
{
    ...
    var result = await InnerAsync();
    ...
}
```


### Caller info attributes

> Caller Info 属性

Caller Information can help us in tracing, debugging and creating diagnose tools.

* [`CallerFilePathAttribute`](https://docs.microsoft.com/ja-jp/dotnet/api/system.runtime.compilerservices.callerfilepathattribute)
  * Full path of the source file that contains the caller. This is the file path at compile time.
* [`CallerLineNumberAttribute`](https://docs.microsoft.com/ja-jp/dotnet/api/system.runtime.compilerservices.CallerLineNumberAttribute)
  * Line number in the source file at which the method is called.
* [`CallerMemberNameAttribute`](https://docs.microsoft.com/ja-jp/dotnet/api/system.runtime.compilerservices.CallerMemberNameAttribute)
  * Method or property name of the caller.


### Change of foreach

> `foreach` の破壊的変更

```cs
Action a = null;
foreach (var x in Enumerable.Range(1, 5))
{
    a += () => Console.WriteLine(x);
}

a(); // output to ???
```

```cs
// C# 4.0
Action a = null;
using (var e = Enumerable.Range(1, 5).GetEnumerator())
{
    T x;
    while(e.MoveNext())
    {
        x = e.Current;
        a += () => Console.WriteLine(x);
    }
}

a(); // output to 5, 5, 5, 5, 5

// C# 5.0～
Action a = null;
using (var e = Enumerable.Range(1, 5).GetEnumerator())
{
    while(e.MoveNext())
    {
        T x = e.Current;
        a += () => Console.WriteLine(x);
    }
}

a();  // output to 1, 2, 3, 4, 5
```
