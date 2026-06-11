# C# version 7.2

## Table of contents. <!-- omit in toc -->

- [Overview](#overview)
  - [Since](#since)
- [To study](#to-study)
- [Features](#features)
  - [Initializers on stackalloc arrays](#initializers-on-stackalloc-arrays)
  - [Use fixed statements with any type that supports a pattern](#use-fixed-statements-with-any-type-that-supports-a-pattern)
  - [Access fixed fields without pinning](#access-fixed-fields-without-pinning)
  - [Reassign ref local variables](#reassign-ref-local-variables)
  - [Declare readonly struct types, to indicate that a struct is immutable and should be passed as an in parameter to its member methods](#declare-readonly-struct-types-to-indicate-that-a-struct-is-immutable-and-should-be-passed-as-an-in-parameter-to-its-member-methods)
  - [Add the in modifier on parameters, to specify that an argument is passed by reference but not modified by the called method](#add-the-in-modifier-on-parameters-to-specify-that-an-argument-is-passed-by-reference-but-not-modified-by-the-called-method)
  - [Use the ref readonly modifier on method returns, to indicate that a method returns its value by reference but doesn't allow writes to that object](#use-the-ref-readonly-modifier-on-method-returns-to-indicate-that-a-method-returns-its-value-by-reference-but-doesnt-allow-writes-to-that-object)
  - [Declare ref struct types, to indicate that a struct type accesses managed memory directly and must always be stack allocated](#declare-ref-struct-types-to-indicate-that-a-struct-type-accesses-managed-memory-directly-and-must-always-be-stack-allocated)
  - [Use additional generic constraints](#use-additional-generic-constraints)
  - [Non-trailing named arguments](#non-trailing-named-arguments)
  - [Leading underscores in numeric literals](#leading-underscores-in-numeric-literals)
  - [private protected access modifier](#private-protected-access-modifier)
  - [Conditional ref expressions](#conditional-ref-expressions)

## Overview

- [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-72)

### Since

- November 2017
- .NET Core 2.1
- Visual Studio 2017 version 15.5

## To study

- [Examples.Features.CSharp72.Tests](../../src/Examples.Features.CSharp72.Tests/)

## Features

### Initializers on stackalloc arrays

> `stackalloc` 配列初期化子

C# 7.2 and later let you use `stackalloc` without `unsafe` when combined with `Span<T>`.
`stackalloc` can only be used with "[unmanaged types](https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/builtin-types/unmanaged-types)".

Stack size is only about 4MB (64-bit) or 1MB (32-bit), so watch your allocation size.

### Use fixed statements with any type that supports a pattern

> パターンをサポートする任意のタイプで `fixed` を使用できます。

See C# 7.3.

### Access fixed fields without pinning

> ピン留めを使用せずに `fixed` フィールドにアクセスできます。

See C# 7.3.

### Reassign ref local variables

> `ref` ローカル変数を再割り当てすることができます。

See C# 7.3.

### Declare readonly struct types, to indicate that a struct is immutable and should be passed as an in parameter to its member methods

> `readonly struct` 型を宣言し、構造体が不変であり、inパラメーターとしてそのメンバーメソッドに渡される必要があることを示します。

In C# 7.2 and later, you can use the `readonly` modifier to declare that a struct type is immutable.

- All field declarations must be `readonly`.
- All properties, including auto-implemented ones, must be read-only.

### Add the in modifier on parameters, to specify that an argument is passed by reference but not modified by the called method

> `in` メソッドパラメータにより、参照渡し引数が呼び出されたメソッドによって変更されないように指定します。

`in` is pass-by-reference and read-only, so it's useful to avoid copying large structs.

That said, `in` has a few caveats, similar to `readonly`.

Calling methods on a `readonly` field of a non-`readonly` struct can trigger defensive copies.
The same issue can happen with `in`. As with `readonly` fields, using `readonly struct` helps avoid it.

`in` can't be used in async (`async`) methods or iterators (`yield`).

### Use the ref readonly modifier on method returns, to indicate that a method returns its value by reference but doesn't allow writes to that object

> メソッド戻りの `ref readonly` 修飾子を使用し、メソッドが参照によってその値を戻しますが、そのオブジェクトに対する書き込みを許可しないことを指定します。

Use `ref readonly` returns when both of these apply:

- The return value is a `struct` larger than `IntPtr.Size`.
- The storage lifetime is longer than the method returning the value.

### Declare ref struct types, to indicate that a struct type accesses managed memory directly and must always be stack allocated

> `ref struct`を宣言し、構造体がマネージド対象メモリに直接アクセスでき、常にスタックにアロケートする必要があることを示します。

C# 7.2 introduced `Span<T>`, and an important part of this feature is that it must live on the stack (not the heap).
To avoid hardcoding special handling just for `Span<T>`, C# introduced the `ref struct` type category.

### Use additional generic constraints

> 追加のジェネリック制約を使用できます。

See C# 7.3.

### Non-trailing named arguments

> 末尾以外で名前付き引数を使用できる。

Named arguments are valid with positional arguments in the following cases:

- No positional arguments appear after them.
- They are used in the correct position (this part was added).

This improves call-site readability when passing literals such as numbers,
`true`, `false`, or `null`, without introducing temporary variables.

```cs
    // C# 7.2 or later
    PrintOrderDetails(sellerName: "Gift Shop", 31, productName: "Red Mug");
    PrintOrderDetails("Gift Shop", orderNum: 31, "Red Mug");
```

### Leading underscores in numeric literals

> 数値リテラルの先頭のアンダースコア ( `_` )

C# 7.0 introduced `_` as a digit separator in numeric literals.
From C# 7.2 onward, you can also place separators at the beginning (right after `0x` or `0b`).

```cs
// C# 7.2 or later
int h = 0x_00ff_0001;
int b = 0b_1001_1111;
```

### private protected access modifier

> `private protected` アクセス修飾子

`public` > `protected internal` >
( `internal`: same assembly | `protected`: derived types ) >
**`private protected`** > `private`.

Accessible from derived types within the same assembly.

| Accessibility Levels | the containing class | current derived | current assembly | another derived | another assembly |
| -------------------- | :------------------: | :-------------: | :--------------: | :-------------: | :--------------: |
| `public`             |        allow         |      allow      |      allow       |      allow      |      allow:      |
| `protected`          |        allow         |      allow      |       deny       |      allow      |       deny       |
| `internal`           |        allow         |      allow      |      allow       |      deny       |       deny       |
| `protected internal` |        allow         |      allow      |      allow       |      allow      |       deny       |
| `private`            |        allow         |      deny       |       deny       |      deny       |       deny       |
| Unspecified          |        allow         |      deny       |       deny       |      deny       |       deny       |
| `private protected`  |        allow         |      allow      |       deny       |      deny       |       deny       |

- current derived : types derived from the containing class within the current assembly.
- current assembly : the current assembly.
- another derived : types derived from the containing class within the another assembly.
- another assembly : the another assembly

### Conditional ref expressions

> 条件演算子( `?:` )での `ref` 利用

You can now make the result of a conditional expression (`?:`) a reference.

```cs
// C# 7.2 or later
ref var result = ref ((a < b) ? ref b : ref a);
```
