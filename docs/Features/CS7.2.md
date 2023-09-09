# C# 7.2 Features

* https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-72

## Since

- 7.2
  - November 2017
  - Visual Studio 2017 version 15.5

## Table of features

### Initializers on stackalloc arrays.

> `stackalloc` 配列初期化子

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7minor2/StackallocArrays/UnitTests.cs)

C# 7.2 以降では、`Span<T>` 構造体と併用することで、`unsafe` なしで `stackalloc` を使えるようになりました。
`stackalloc` は"[アンマネージ型](https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/builtin-types/unmanaged-types)"のみで使用できます、

stack size は 4MB(64-bit)または1MB(32-bit)しかないのでサイズに注意して使用しましょう。

### - Use fixed statements with any type that supports a pattern.

> パターンをサポートする任意のタイプで `fixed` を使用できます。

※7.3 になった気がします。

### - Access fixed fields without pinning.

> ピン留めを使用せずに `fixed` フィールドにアクセスできます。

※7.3 になった気がします。

### - Reassign ref local variables.

> `ref` ローカル変数を再割り当てすることができます。

※7.3 になった気がします。

### Declare readonly struct types, to indicate that a struct is immutable and should be passed as an in parameter to its member methods.

> `readonly struct` 型を宣言し、構造体が不変であり、inパラメーターとしてそのメンバーメソッドに渡される必要があることを示します。

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7minor2/ReadonlyStructTypes/UnitTests.cs)

C# 7.2 以降では、`readonly` 修飾子を使用して、構造体型が変更不可であることを宣言します。 readonly 構造体のすべてのデータ メンバーを、次のように読み取り専用にする必要があります。

* すべてのフィールド宣言には、`readonly` が必要です
* 自動的に実装されるものも含めて、すべてのプロパティは、読み取り専用である必要があります。


### Add the in modifier on parameters, to specify that an argument is passed by reference but not modified by the called method.

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7minor2/InModifierParameters/UnitTests.cs)

> `in` メソッドパラメータにより、参照渡し引数が呼び出されたメソッドによって変更されないように指定します。

`in` は参照渡しの書き込み不可なるので大きな構造体のコピーを避ける目的で使われます。
が、実際のところ `readonly struct` で渡さないと暗黙的なコピーが作成され、さらに厄介なことになるようです。

また `in` は非同期( `async` )やイテレータ( `yield` )には使用できません。

### Use the ref readonly modifier on method returns, to indicate that a method returns its value by reference but doesn't allow writes to that object.

> メソッド戻りの `ref readonly` 修飾子を使用し、メソッドが参照によってその値を戻しますが、そのオブジェクトに対する書き込みを許可しないことを指定します。

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7minor2/RefReadonlyReturnMethods/UnitTests.cs)

次の両方の条件に当てはまる場合は、`ref readonly` の戻り値を使用します。

* 戻り値が、`IntPtr.Size` より大きい `struct` である。
* ストレージの有効期間が、値を返すメソッドより長い。

```cs
// CS7.2～

ref readonly var point2 = ref SamplePoint.Origin;
```


### Declare ref struct types, to indicate that a struct type accesses managed memory directly and must always be stack allocated.

> `ref struct`を宣言し、構造体がマネージド対象メモリに直接アクセスでき、常にスタックにアロケートする必要があることを示します。

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7minor2/ReadonlyStructTypes/UnitTests.cs)


C# 7.2 で `Span<T>` 構造体という型が提供されましたが、この機能において「スタック上に置かれている必要がある」(ヒープに置けない)という制限が重要になります。この機能をコンパイラが `Span<T>` だけ特別扱いしないよう ref構造体 ( `ref struct` )というデータ型を導入しました。


### - Use additional generic constraints.

> 追加のジェネリック制約を使用できます。

※7.3 になった気がします。


### Non-trailing named arguments

> 末尾以外で名前付き引数を使用できる。

名前付き引数の後ろに位置引数を続けることができます。

```cs
  static void PrintOrderDetails(string sellerName , int orderNum, string productName)
  {
    ...
  }

  // The method can be called in the normal way, by using positional arguments.
  PrintOrderDetails("Gift Shop", 31, "Red Mug");

  // CS4.0～
  PrintOrderDetails(orderNum: 31, productName: "Red Mug", sellerName: "Gift Shop");
  PrintOrderDetails(productName: "Red Mug", sellerName: "Gift Shop", orderNum: 31);
  PrintOrderDetails("Gift Shop", 31, productName: "Red Mug");

  // CS7.2～
  PrintOrderDetails(sellerName: "Gift Shop", 31, productName: "Red Mug");
  PrintOrderDetails("Gift Shop", orderNum: 31, "Red Mug");
```


### Leading underscores in numeric literals

> 数値リテラルの先頭のアンダースコア ( `_` )

C# 7.0で数値リテラルの区切り文字 ( `_` ) が追加されましたが、
C# 7.2 以降では、先頭（といっても `0x`, `0b` の後ろ）に区切り文字を含めることができるようになりました。

```cs
// CS7.2～
int h = 0x_00ff_0001;
int b = 0b_1001_1111;
```


### private protected access modifier

> `private protected` アクセス修飾子

public > protected internal > ( internal:同一アセンブリ | protected|派生 ) > **private protected** > private.

```cs
class Outer
{
  // CS7.1
  private class AAA { }

  protected class BBB { }

  protected internal class CCC { }

  // CS7.2～
  private protected class ZZZ { }

}
```

派生かつ同一アセンブリで参照可能です。


### Conditional ref expressions

> 条件演算子( `?:` )での `ref` 利用

条件式 ( `?:` ) の結果を参照にすることができるようになりました。

```cs
// CS7.2～
var result = ref ((a < b) ? ref b : ref a);
```
