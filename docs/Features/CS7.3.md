# C# 7.3 Features

* https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-73

## Since

- 7.3
  - May 2018
  - .NET Core 2.1, 2.2, .NET Framework 4.8
  - Visual Studio 2017 version 15.7

## Table of features

### ? Accessing fixed fields without pinning.

> ピン留めを使用せずに fixed フィールドにアクセスできます。

`unsafe`なので保留。


### Reassigning ref local variables.

> ref ローカル変数を再割り当てできます。

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7minor3/RefLocalVariables/UnitTests.cs)

```cs
// CS7.3～

int x = 1;
int y = 2;

ref var r = ref x;
r = ref y;
```

### Using initializers on stackalloc arrays.

> `stackalloc` 配列で初期化子を使用できます。

C#7.2で追加された `stackalloc` に初期化子を使用できるようになりました。

```cs
// CS7.2
Span<int> arr1 = stackalloc int[3];

// CS7.3～
Span<int> arr2 = stackalloc int[3] { 1, 2, 3 };
Span<int> arr3 = stackalloc int[] { 1, 2, 3 };
Span<int> arr4 = stackalloc[] { 1, 2, 3 };
```


### ? Using fixed statements with any type that supports a pattern.

> パターンをサポートする型と共に fixed ステートメントを使用できます。

`unsafe` なので保留。

`GetPinnableReference()`というメソッドが重要らしい。


### Using more generic constraints.

> ジェネリック型制約が追加されました。

* `unmanaged`
* `System.Enum`
* `System.Delegate`
* `System.MulticastDelegate`


### Testing == and != with tuple types.

> タプル型を使用して == と != をテストできます。

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7minor3/Tuples/UnitTests.cs)

C# 7.3 以降では、タプル型で == および != 演算子がサポートされます。 これらの演算子により、左側のオペランドのメンバーが、"タプル要素の順序"に従って、右側のオペランドの対応するメンバーと比較されます。

```cs
// CS7.3～

  // illegal types.
  (int a, byte b) left = (5, 10);
  (long a, int b) right = (5, 10);
  (left == right).IsTrue();
  (left != right).IsFalse();

  // illigal names.
  var t1 = (A: 5, B: 10);
  var t2 = (B: 5, A: 10);
  (t1 == t2).IsTrue();
  (t1 != t2).IsFalse();
```

### Using expression variables in more locations.

> 式の変数をより多くの場所で使用できます。

C# 7.0から式中で、 `is` 演算子や `out` 変数宣言を使って、 式中でも変数宣言できるようになりましたが、 いくつか制限がありました。 C# 7.3で、これまではできなかった以下の個所でも変数宣言ができるようになりました。

* クエリ式

```cs
// CS7.3～
var q =
    from s in new[] { "a", "abc", "112", "132", "451", null }
    where s is string x && x.Length > 1
    where int.TryParse(s, out var x) && (x % 3) == 0
    select s;
```

* コンストラクター初期子、フィールド初期化子、プロパティ初期化子

```cs
// CS7.3～
public class Derived
{
    public Derived(string s) : this(int.TryParse(s, out var x) ? x : -1)
    {
      ...
    }

    public Derived(int a) : base(out var x)
    {
      ...
    }

    public int Field = int.TryParse("123", out var x) ? x : -1;
    public int Property{ get; set; } = int.TryParse("123", out var x) ? x : -1;

}
```


### Attach attributes to the backing field of auto-implemented properties.

> 自動実装プロパティのバッキング フィールドに属性を指定できます。

```cs
// CS7.2
[NonSerialized]
private double x;
public double X
{
    get => x;
    set => x = value;
}

// CS7.3～
[field: NonSerialized]
public double X { get; set; }
```

実は `event` では以前から使えていたようです。

```cs
// CS7.2
[field: NonSerialized]
public event PropertyChangedEventHandler PropertyChanged;
```


### Method resolution when arguments differ by in has been improved.

> 引数が in によって異なる場合のメソッド解決が改善されました。

* [see ...](https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/keywords/in-parameter-modifier#overload-resolution-rules)


### Overload resolution now has fewer ambiguous cases.

> オーバーロードの解決のあいまいなケースが削減されました。

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7minor3/OverloadResolution/UnitTests.cs)

* 静的メソッドかインスタンス メソッドかの違いで解決できるようになった
* ジェネリック型制約の違いで解決できるようになった
* メソッド グループを引数にするとき、メソッドの戻り値を見るようになった

