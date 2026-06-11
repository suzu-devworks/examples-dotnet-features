# C# version 7.3

## Table of contents. <!-- omit in toc -->

- [Overview](#overview)
  - [Since](#since)
- [To study](#to-study)
- [Features](#features)
  - [Accessing fixed fields without pinning](#accessing-fixed-fields-without-pinning)
  - [Reassign ref local variables](#reassign-ref-local-variables)
  - [Using initializers on stackalloc arrays](#using-initializers-on-stackalloc-arrays)
  - [Using fixed statements with any type that supports a pattern](#using-fixed-statements-with-any-type-that-supports-a-pattern)
  - [Using more generic constraints](#using-more-generic-constraints)
  - [Testing == and != with tuple types](#testing--and--with-tuple-types)
  - [Using expression variables in more locations](#using-expression-variables-in-more-locations)
  - [Attach attributes to the backing field of auto-implemented properties](#attach-attributes-to-the-backing-field-of-auto-implemented-properties)
  - [Method resolution when arguments differ by in has been improved](#method-resolution-when-arguments-differ-by-in-has-been-improved)
  - [Overload resolution now has fewer ambiguous cases](#overload-resolution-now-has-fewer-ambiguous-cases)

## Overview

- [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-73)

### Since

- May 2018
- .NET Core 2.1, 2.2
- .NET Framework 4.8
- Visual Studio 2017 version 15.7

## To study

- [Examples.Features.CSharp73.Tests](../../src/Examples.Features.CSharp73.Tests/)

## Features

### Accessing fixed fields without pinning

> ピン留めを使用せずに fixed フィールドにアクセスできます。

- Uses `unsafe`, so I'll skip it for now.

### Reassign ref local variables

> `ref` ローカル変数を再割り当てできます。

In C# 7.2, changing the referenced value was already possible.
Now you can also reuse a `ref` local variable and switch the referenced target.

```cs
// C# 7.3 or later
int x = 10;
int y = 20;
ref int rx = ref x;
// reassign.
rx = ref y
```

### Using initializers on stackalloc arrays

> `stackalloc` 配列で初期化子を使用できます。

You can now use initializers with `stackalloc`, which was added in C# 7.2.

```cs
// C# 7.2
Span<int> arr1 = stackalloc int[3];

// C# 7.3 or later
Span<int> arr2 = stackalloc int[3] { 1, 2, 3 };
Span<int> arr3 = stackalloc int[] { 1, 2, 3 };
Span<int> arr4 = stackalloc[] { 1, 2, 3 };
```

### Using fixed statements with any type that supports a pattern

> パターンをサポートする型と共に fixed ステートメントを使用できます。

Uses `unsafe`, so I'll skip it for now.

`GetPinnableReference()` is the key method for this feature.

### Using more generic constraints

> ジェネリック型制約が追加されました。

- `unmanaged`
- `System.Enum`
- `System.Delegate`
- `System.MulticastDelegate`

### Testing == and != with tuple types

> タプル型を使用して == と != をテストできます。

C# 7.3 and later support `==` and `!=` for tuple types.
This doesn't call `ValueTuple` operators directly; the compiler applies special handling.

These operators compare members from the left operand to corresponding members on the right based on tuple element order.

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

### Using expression variables in more locations

> 式の変数をより多くの場所で使用できます。

Since C# 7.0, variables could be declared in expressions using `is` and `out`,
but there were several restrictions. C# 7.3 also allows declarations
in the following locations:

- Query expressions

```cs
// C# 7.3 or later
var q =
    from s in new[] { "a", "abc", "112", "132", "451", null }
    where s is string x && x.Length > 1
    where int.TryParse(s, out var x) && (x % 3) == 0
    select s;
```

- Constructor initializers
- Field initializers
- Property initializers

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

### Attach attributes to the backing field of auto-implemented properties

> 自動実装プロパティのバッキング フィールドに `field` 指定の属性を指定できます。

```cs
// C# 7.3 or later
[field: NonSerialized]
public double X { get; set; }
```

Apparently this had already been available for `event`.

```cs
// C# 7.2
[field: NonSerialized]
public event PropertyChangedEventHandler PropertyChanged;
```

### Method resolution when arguments differ by in has been improved

> 引数が in によって異なる場合のメソッド解決が改善されました。

- [see ...](https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/keywords/in-parameter-modifier#overload-resolution-rules)

### Overload resolution now has fewer ambiguous cases

> オーバーロードの解決のあいまいなケースが削減されました。

- Ambiguity can now be resolved by static-vs-instance method differences.
- It can also be resolved by differences in generic constraints.
- When passing method groups as arguments, return types are now considered.
