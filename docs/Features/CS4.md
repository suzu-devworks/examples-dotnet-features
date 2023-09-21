# C# 4.0 Features

* https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-40

## Since

- 4.0
  - April 2010
  - .NET Framework 4
  - Visual Studio 2010

## Table of features.

### Dynamic binding

> 動的型付け変数

* [Test Code ...](/src/Examples.Features.CS4.Tests/CS4/DynamicBinding/UnitTests.cs)

**C# 4.0 の主役。**

DLR(Dynamic Language Runtime)の流行を見越して追加された機能と思われますが・・・。

`dynamic` で次のようなことができるようになります。

* 遅延バインド
* ダックタイピング
* ジェネリクス利用時の静的メソッド呼び出し
* 多重ディスパッチ


### Named and optional arguments

> オプション引数、名前付き引数

```cs
// CS4.0～
PrintOrderDetails(orderNum: 31, productName: "Red Mug", sellerName: "Gift Shop");
PrintOrderDetails(productName: "Red Mug", sellerName: "Gift Shop", orderNum: 31);

public void ExampleMethod(int required, string optionalstr = "default string",
    int optionalint = 10);

anExample.ExampleMethod(3, optionalint: 4);
```


### Generic Co- and Contra- variance

> ジェネリックの共変性と反変性

* [Test Code ...](/src/Examples.Features.CS4.Tests/CS4/GenericVariance/UnitTests.cs)

型システムにおいては、共変性、反変性、不変性は、次のように定義されます。

* `Covariance`
  * 最初に指定された型よりも限定的な（強い)派生型を使用できるようにします。
  * for example, IEnumerable<T>, IEnumerator<T>, IQueryable<T>, and IGrouping<TKey,TElement>.
* `Contravariance`
  * 最初に指定された型よりも一般的な (弱い) 型を使用できるようにします。
  * for example: IComparer<T>, IComparable<T>, and IEqualityComparer<T>.
* `Invariance`
  * 最初に指定された型のみを使用できることを意味します。


### Embedded interop types

> COM(Embedded) 相互運用型の特別処理

COM 相互運用用のクラス(Runtime Callable Wrapper) を特別あつかいして COM 呼び出しをやりやすくしたそうです。

* COM インタフェースの引数は `ref` キーワードはオプションになった。（ref ばっかりになるので）
* COM オブジェクトのインデックス付きプロパティが使えるようになった。（`get_X(index)`, `Set_X(index, value)` => `X[index]`)
