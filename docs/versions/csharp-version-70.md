# C# version 7.0

## Table of contents. <!-- omit in toc -->

- [Overview](#overview)
  - [Since](#since)
- [To study](#to-study)
- [Features](#features)
  - [Out variables](#out-variables)
  - [Tuples and deconstruction](#tuples-and-deconstruction)
  - [Pattern matching](#pattern-matching)
  - [Local functions](#local-functions)
  - [Expanded expression bodied members](#expanded-expression-bodied-members)
  - [Ref locals and returns](#ref-locals-and-returns)
  - [Discards](#discards)
  - [Binary literals and Digit separators](#binary-literals-and-digit-separators)
  - [Throw expressions](#throw-expressions)
  - [Generalized async return types](#generalized-async-return-types)

## Overview

- [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-70)

### Since

- March 2017
- .NET Framework 4.7
- Visual Studio 2017 version 15.0

## To study

- [Examples.Features.CSharp70.Tests](../../src/Examples.Features.CSharp70.Tests/)

## Features

### Out variables

> `out` 変数宣言

You can now declare a variable right inside the expression while passing an `out` argument.

```CS
// C# 7.0 or later
if (int.TryParse(numOfText, out int converted))
{
}
// converted scope is here.
var newValue = converted;
```

### Tuples and deconstruction

> タプルと分解

This gives you a concise way to group multiple values in a lightweight data structure.

The primary use case for tuples is method return values.
Compared with tuples, `out` arguments are often less readable.

Deconstruction is not limited to tuples.
It can be used with any type that defines a `Deconstruct` method,
including extension methods.

`System.ValueTuple` and `System.Tuple` are different. Main differences:

- `System.ValueTuple` is a value type, while `System.Tuple` is a reference type.
- `System.ValueTuple` is mutable, while `System.Tuple` is immutable.
- `System.ValueTuple` stores data as fields, while `System.Tuple` uses properties.

### Pattern matching

> パターン マッチング

In C# 7.0, the `is` operator and `switch` statement were expanded so you can write patterns after `is` and `case`.

Pattern types include:

- Declaration/type patterns ( `is <Type> <variable>` )
- Constant patterns ( `is <value>` )
- `var` patterns ( `is var <variable>` )

### Local functions

> ローカル関数

Before C# 6.0, local-scope logic could be expressed using anonymous functions and lambdas,
but several scenarios remained cumbersome.

For example:

- Recursive calls were possible, but not straightforward.
- You couldn't write iterators.
- You couldn't make them generic.
- You couldn't use optional parameters.

Local functions solve those issues.

In addition, avoiding allocations of `Func<T>` / `Action<T>` objects can enable
better compiler optimizations and may improve performance.

### Expanded expression bodied members

> 拡張された式形式のメンバー

`=>` member syntax now allows constructors, finalizers, and property getters/setters.

### Ref locals and returns

> `ref` 戻り値と `ref` ローカル変数

You can now use by-reference semantics with return values and local variables too.
That helps handle large value types without unnecessary copying.

### Discards

> 破棄

With tuples and deconstruction, you can explicitly mark unused values with `_`.

```cs
// C# 7.0 or later

  (_, _, area) = city.GetCityInformation(cityName);

  switch (a) {
      case int _ when a > 10:
          ...
          break;
      default:
          ...
          break;
  }

  Func<object, int, int> func = (_, n) => 10 * n;
```

### Binary literals and Digit separators

> バイナリ、数値リテラル表記の拡張

In addition to `0x`/`0X`, C# now supports `0b`/`0B` for binary literals. Looks like `0o` wasn't introduced.

Also, `_` is just a digit separator, so you can't place it arbitrarily.

### Throw expressions

> `throw` 式

From C# 7.0 onward, `throw` can be used as both an expression and a statement.
This allows exceptions to be thrown in previously unsupported expression contexts.

- In conditional operators (after `?` or `:`)
- In null-coalescing operators (after `??`)
- Inside lambdas and expression-bodied members (after `=>`)

```cs
// C# 7.0 or later

  string arg = args.Length >= 1
                ? args[0]
                : throw new ArgumentException("You must supply an argument");

  var x = obj as string ?? throw new ArgumentException(nameof(obj));

  DateTime ToDateTime(IFormatProvider provider) =>
         throw new InvalidCastException("Conversion to a DateTime is not supported.");
```

### Generalized async return types

> `async` メソッドの返り値型の一般化

Async methods previously allowed only `void`, `Task`, and `Task<TResult>` as return types,
but custom types are now supported when they satisfy specific requirements.

Designing a type that satisfies those requirements can still be non-trivial.
([see ...](https://ufcpp.net/study/csharp/sp5_async.html#task-like))

For async methods where most calls do not perform asynchronous work,
`ValueTask` was introduced to reduce `Task` allocation overhead.

`ValueTask` has fewer helper APIs than `Task`. If you want `WhenAll()` or `WhenAny()`, you need to call `.AsTask()`.

For `ValueTask<TResult>`, behavior is undefined if you do the following on the same instance:

- `await` it multiple times.
- Call `AsTask()` multiple times.
- Use `.Result` or `.GetAwaiter().GetResult()` before the operation completes.
- Combine one or more of these patterns and operate on the same instance multiple times.
