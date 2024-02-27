# Examples.Features.CS72.Tests

##  C# version 7.2

### See also

* [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-72)

### Since

- November 2017
- .NET Core 2.1
- Visual Studio 2017 version 15.5


## Table of contents. <!-- omit in toc -->

- [Examples.Features.CS72.Tests](#examplesfeaturescs72tests)
  - [C# version 7.2](#c-version-72)
    - [See also](#see-also)
    - [Since](#since)
  - [Features](#features)
    - [Initializers on stackalloc arrays.](#initializers-on-stackalloc-arrays)
    - [Use fixed statements with any type that supports a pattern.](#use-fixed-statements-with-any-type-that-supports-a-pattern)
    - [Access fixed fields without pinning.](#access-fixed-fields-without-pinning)
    - [Reassign ref local variables.](#reassign-ref-local-variables)
    - [Declare readonly struct types, to indicate that a struct is immutable and should be passed as an in parameter to its member methods.](#declare-readonly-struct-types-to-indicate-that-a-struct-is-immutable-and-should-be-passed-as-an-in-parameter-to-its-member-methods)
    - [Add the in modifier on parameters, to specify that an argument is passed by reference but not modified by the called method.](#add-the-in-modifier-on-parameters-to-specify-that-an-argument-is-passed-by-reference-but-not-modified-by-the-called-method)
    - [Use the ref readonly modifier on method returns, to indicate that a method returns its value by reference but doesn't allow writes to that object.](#use-the-ref-readonly-modifier-on-method-returns-to-indicate-that-a-method-returns-its-value-by-reference-but-doesnt-allow-writes-to-that-object)
    - [Declare ref struct types, to indicate that a struct type accesses managed memory directly and must always be stack allocated.](#declare-ref-struct-types-to-indicate-that-a-struct-type-accesses-managed-memory-directly-and-must-always-be-stack-allocated)
    - [Use additional generic constraints.](#use-additional-generic-constraints)
    - [Non-trailing named arguments](#non-trailing-named-arguments)
    - [Leading underscores in numeric literals](#leading-underscores-in-numeric-literals)
    - [private protected access modifier](#private-protected-access-modifier)
    - [Conditional ref expressions](#conditional-ref-expressions)


## Features

### Initializers on stackalloc arrays.

> `stackalloc` 配列初期化子

C# 7.2 以降では、`Span<T>` 構造体と併用することで、`unsafe` なしで `stackalloc` を使えるようになりました。
`stackalloc` は"[アンマネージ型](https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/builtin-types/unmanaged-types)"のみで使用できます、

stack size は 4MB(64-bit) または 1MB(32-bit) しかないのでサイズに注意して使用しましょう。


### Use fixed statements with any type that supports a pattern.

> パターンをサポートする任意のタイプで `fixed` を使用できます。

※ C# 7.3


### Access fixed fields without pinning.

> ピン留めを使用せずに `fixed` フィールドにアクセスできます。

※ C# 7.3


### Reassign ref local variables.

> `ref` ローカル変数を再割り当てすることができます。

※ C# 7.3


### Declare readonly struct types, to indicate that a struct is immutable and should be passed as an in parameter to its member methods.

> `readonly struct` 型を宣言し、構造体が不変であり、inパラメーターとしてそのメンバーメソッドに渡される必要があることを示します。

C# 7.2 以降では、`readonly` 修飾子を使用して、構造体型が変更不可であることを宣言します。

* すべてのフィールド宣言には、`readonly` が必要です
* 自動的に実装されるものも含めて、すべてのプロパティは、読み取り専用である必要があります。


### Add the in modifier on parameters, to specify that an argument is passed by reference but not modified by the called method.

> `in` メソッドパラメータにより、参照渡し引数が呼び出されたメソッドによって変更されないように指定します。

`in` は参照渡しの書き込み不可なるので大きな構造体のコピーを避ける目的で使われます。

ただし `in` については `readonly` と同様に少し注意が必要のようです。

`readonly` ではない構造体の `readonly` フィールドに対してメソッドを呼ぶと、防衛的コピーが発生するという問題があるようです。
この問題は、`in` でも起こります。`readonly struct` を使えば回避できる点も `readonly` フィールドと同様です。

 `in` は非同期( `async` )やイテレータ( `yield` )には使用できません。


### Use the ref readonly modifier on method returns, to indicate that a method returns its value by reference but doesn't allow writes to that object.

> メソッド戻りの `ref readonly` 修飾子を使用し、メソッドが参照によってその値を戻しますが、そのオブジェクトに対する書き込みを許可しないことを指定します。

次の両方の条件に当てはまる場合は、`ref readonly` の戻り値を使用します。

* 戻り値が、`IntPtr.Size` より大きい `struct` である。
* ストレージの有効期間が、値を返すメソッドより長い。


### Declare ref struct types, to indicate that a struct type accesses managed memory directly and must always be stack allocated.

> `ref struct`を宣言し、構造体がマネージド対象メモリに直接アクセスでき、常にスタックにアロケートする必要があることを示します。

C# 7.2 で `Span<T>` 構造体という型が提供されましたが、この機能において「スタック上に置かれている必要がある」(ヒープに置けない)という制限が重要になります。この機能をコンパイラが `Span<T>` だけ特別扱いしないよう ref構造体 ( `ref struct` )というデータ型を導入しました。


### Use additional generic constraints.

> 追加のジェネリック制約を使用できます。

※ C# 7.3


### Non-trailing named arguments

> 末尾以外で名前付き引数を使用できる。

名前付き引数は、位置引数と共に使用するとき、次の場合において有効となります

- 後ろに位置引数が続かない場合。
- 正しい位置で使用される場合。（これが追加されました）

数値リテラルや、`true`, `false`, `null` リテラルなどを直接指定する場合、呼び出し元からは何を設定しているかが分かりづらかったのを変数を使わずに解決することができるようになりました。 

```cs
    // C# 7.2 or later
    PrintOrderDetails(sellerName: "Gift Shop", 31, productName: "Red Mug");
    PrintOrderDetails("Gift Shop", orderNum: 31, "Red Mug");
```


### Leading underscores in numeric literals

> 数値リテラルの先頭のアンダースコア ( `_` )

C# 7.0で数値リテラルの区切り文字 ( `_` ) が追加されましたが、
C# 7.2 以降では、先頭（といっても `0x`, `0b` の後ろ）に区切り文字を含めることができるようになりました。

```cs
// C# 7.2 or later
int h = 0x_00ff_0001;
int b = 0b_1001_1111;
```


### private protected access modifier

> `private protected` アクセス修飾子

`public` > `protected internal` > ( `internal`: 同一アセンブリ | `protected` : 派生 ) > **`private protected`** > `private`.

派生かつ同一アセンブリで参照可能です。

| Accessibility Levels | the containing class | current derived | current assembly | another derived | another assembly |
| -------------------- | :------------------: | :-------------: | :--------------: | :-------------: | :--------------: |
| `public`             |        allow         |      allow      |      allow       |      allow      |      allow:      |
| `protected`          |        allow         |      allow      |       deny       |      allow      |       deny       |
| `internal`           |        allow         |      allow      |      allow       |      deny       |       deny       |
| `protected internal` |        allow         |      allow      |      allow       |      allow      |       deny       |
| `private`            |        allow         |      deny       |       deny       |      deny       |       deny       |
| <Unspecified>        |        allow         |      deny       |       deny       |      deny       |       deny       |
| `private protected`  |        allow         |      allow      |       deny       |      deny       |       deny       |

- current derived : types derived from the containing class within the current assembly.
- current assembly : the current assembly.
- another derived : types derived from the containing class within the another assembly.
- another assembly : the another assembly


### Conditional ref expressions

> 条件演算子( `?:` )での `ref` 利用

条件式 ( `?:` ) の結果を参照にすることができるようになりました。

```cs
// C# 7.2 or later
ref var result = ref ((a < b) ? ref b : ref a);
```
