# Examples.Features.CS73.Tests

## C# version 7.3

### See also

* [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-73)

### Since

- May 2018
- .NET Core 2.1, 2.2
- .NET Framework 4.8
- Visual Studio 2017 version 15.7


## Table of contents. <!-- omit in toc -->

- [Examples.Features.CS73.Tests](#examplesfeaturescs73tests)
  - [C# version 7.3](#c-version-73)
    - [See also](#see-also)
    - [Since](#since)
  - [Features](#features)
    - [Accessing fixed fields without pinning.](#accessing-fixed-fields-without-pinning)
    - [Reassign ref local variables.](#reassign-ref-local-variables)
    - [Using initializers on stackalloc arrays.](#using-initializers-on-stackalloc-arrays)
    - [Using fixed statements with any type that supports a pattern.](#using-fixed-statements-with-any-type-that-supports-a-pattern)
    - [Using more generic constraints.](#using-more-generic-constraints)
    - [Testing == and != with tuple types.](#testing--and--with-tuple-types)
    - [Using expression variables in more locations.](#using-expression-variables-in-more-locations)
    - [Attach attributes to the backing field of auto-implemented properties.](#attach-attributes-to-the-backing-field-of-auto-implemented-properties)
    - [Method resolution when arguments differ by in has been improved.](#method-resolution-when-arguments-differ-by-in-has-been-improved)
    - [Overload resolution now has fewer ambiguous cases.](#overload-resolution-now-has-fewer-ambiguous-cases)


## Features

### Accessing fixed fields without pinning.

> ピン留めを使用せずに fixed フィールドにアクセスできます。

* `unsafe` なので保留。


### Reassign ref local variables.

> `ref` ローカル変数を再割り当てできます。

参照先の値の書き換えは C#7.2 でもできていましたが、`ref` ローカル変数を使い回して参照を切り替えることができるようになりました。

```cs
// C# 7.3 or later
int x = 10;
int y = 20;
ref int rx = ref x;
// reassign.
rx = ref y
```

### Using initializers on stackalloc arrays.

> `stackalloc` 配列で初期化子を使用できます。

C#7.2で追加された `stackalloc` に初期化子を使用できるようになりました。

```cs
// C# 7.2
Span<int> arr1 = stackalloc int[3];

// C# 7.3 or later
Span<int> arr2 = stackalloc int[3] { 1, 2, 3 };
Span<int> arr3 = stackalloc int[] { 1, 2, 3 };
Span<int> arr4 = stackalloc[] { 1, 2, 3 };
```


### Using fixed statements with any type that supports a pattern.

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

C# 7.3 以降では、タプル型で == および != 演算子がサポートされます。
これは、後述するValueTupleの演算子が呼ばれるわけではなく、 コンパイラーによる特別な処理が入ります。

これらの演算子により、左側のオペランドのメンバーが、"タプル要素の順序"に従って、右側のオペランドの対応するメンバーと比較されます。

```cs
// C# 7.3 or later

// different types.
(int a, byte b) left = (5, 10);
(long a, int b) right = (5, 10);
(left == right).IsTrue();
(left != right).IsFalse();

// different names.
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
// C# 7.3 or later
var q =
    from s in new[] { "a", "abc", "112", "132", "451", null }
    where s is string x && x.Length > 1
    where int.TryParse(s, out var x) && (x % 3) == 0
    select s;
```

* コンストラクター初期子
* フィールド初期化子
* プロパティ初期化子

```cs
// C# 7.3 or later
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

> 自動実装プロパティのバッキング フィールドに `field` 指定の属性を指定できます。

```cs
// C# 7.3 or later
[field: NonSerialized]
public double X { get; set; }
```

実は `event` では以前から使えていたようです。

```cs
// C# 7.2
[field: NonSerialized]
public event PropertyChangedEventHandler PropertyChanged;
```


### Method resolution when arguments differ by in has been improved.

> 引数が in によって異なる場合のメソッド解決が改善されました。

* [see ...](https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/keywords/in-parameter-modifier#overload-resolution-rules)


### Overload resolution now has fewer ambiguous cases.

> オーバーロードの解決のあいまいなケースが削減されました。

* 静的メソッドかインスタンス メソッドかの違いで解決できるようになった
* ジェネリック型制約の違いで解決できるようになった
* メソッド グループを引数にするとき、メソッドの戻り値を見るようになった
