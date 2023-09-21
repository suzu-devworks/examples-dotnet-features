# C# 7.0 Features

* https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-70

## Since

- 7.0
  - March 2017
  - .NET Framework 4.7
  - Visual Studio 2017 version 15.0

## Table of features

### Out variables

> `out` 変数宣言

`out` 引数を受け取ると同時に、式の中で変数宣言できるようになりました。

```CS
// CS6.0
int converted;
if (int.TryParse(numOfText, out converted))
{
}

// CS7.0～
if (int.TryParse(numOfText, out int converted))
{
}
// converted scope is here.
var newValue = converted;
```


### Tuples and deconstruction

> タプルと分解

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7/Tuples/UnitTests.cs)

軽量データ構造で複数のデータ要素をグループ化するための簡潔な構文を提供します。

タプルの一番の目的はメソッドの戻り値です。out引数はやっぱり不自然に思います。

```cs
// CS7.0～

var t = (Sum: 4.5, Count: 3);
(double Sum, int Count) d = (4.5, 3);

// Deconstruct
(double sum, int count) = t;
```

分解はタプル専用の構文ではなく、以下のように、`Deconstruct` という名前のメソッド(拡張メソッドでも可)を持っている型なら何にでも使うことができます。


### Pattern matching

> パターン マッチング

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7/PatternMatching/UnitTests.cs)

C# 7.0で、is 演算子と swtich ステートメントが拡張されて、`is`/`case` の後ろにパターンを書けるようになりました。

パターンは次のようなものがあります。

* 宣言パターン、型パターン ( `is <Type> <variable>` )
* 定数パターン ( `is <value>`)
* 定数パターン、null チェック ( `is null` )
* var パターン ( `is var <variable>` )


### Local functions

> ローカル関数

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7/LocalFunctions/UnitTests.cs)

C#6.0以前にも匿名関数やラムダ式など、ローカルスコープのみで使用する関数を定義する方法はありましたが、できないことや素直に書きづらいものがありました。

具体的には、
* 再帰呼び出しが素直に書けない（書けないわけではない）
* イテレータが書けない
* ジェネリックにできない
* オプション引数が書けない

これらをローカル関数で解消しました。

そしてラムダ式などの `Func<T>` や `Action<T>` オブジェクトの生成がなくなり、効率の良い最適化がかかりパフォーマンスが上がることもあるようです。


### Expanded expression bodied members

> 拡張された式形式のメンバー

`=>` 形式のメンバー定義に、コンストラクタ―、ファイナライザ―、プロパティのgetter, setter が許可されました。


### Ref locals and returns

> `ref` 戻り値と `ref` ローカル変数

* [Test Code ...](/src/Examples.Features.CS7.Tests/CS7/RefLocalsAndReturns/UnitTests.cs)

戻り値とローカル変数でも参照渡しを使えるようになりました。
これにより、巨大な値型を無駄なコピーなく取り扱えるようになります。

参照型には意味無いと思うのだがどうなのかな？

### Discards

> 破棄

タプルと分解が入ったので、`_`でイラナイものを明示することができるようになってます。

```cs
// CS7.0～

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

`0x`, `0X` 以外に `0b`, `0B` が増えました。`0o` は作られないようです。

また `_` はあくまで桁区切りなので余計な所には書けません。

```cs
// CS6.0
int value1 = 0xFF21;

// CS7.0～
int value2 = 0b01011;
uint value3 = 0b101011100u;
long value4 = 0b101011100L;

long value5 = 0xdead_beaf;
var value6 = 123_456_789.987_654;// 123456789.987654
```


### Throw expressions

> Throw 式

C# 7.0 以降、`throw` は、式およびステートメントとして使用できます。 これにより、以前サポートされていなかったコンテキストでの例外のスローが可能になります。

* 条件演算子(`?` や `:` の後ろ)
* Null 合体演算子(`??` の後ろ)
* ラムダ式や式形式メンバーの中(`=>` の後ろ)

```cs
// CS7.0～

  string arg = args.Length >= 1
                ? args[0]
                : throw new ArgumentException("You must supply an argument");

  var x = obj as string ?? throw new ArgumentException(nameof(obj));

  DateTime ToDateTime(IFormatProvider provider) =>
         throw new InvalidCastException("Conversion to a DateTime is not supported.");
```


### Generalized async return types

> asyncメソッドの返り値型の一般化

asyncメソッドの戻り値で指定できるのは、`void`、`Task`、`Task<TResult>` のみでしたが、特定の条件を満たすように作れば任意の型を指定できるようになりました。

とは言っても「特定の条件を満たすように作る」のもめんどくさいです。([see ...](https://ufcpp.net/study/csharp/sp5_async.html#task-like))

非同期メソッドで、大部分が実際には非同期処理を行わないような場合。Taskオブジェクトをいちいち作成するオーバーヘッドを軽減するため、`ValueTask` 構造体が導入されています。

`ValueTask` は Taskよりメソッドの提供が少なく。`WhenAll()` や `WhenAny()` するには `.AsTask()`する必要があります。

`ValueTask<TResult> ` は、インスタンスに対して次の操作を実行した場合の結果は未定義になります。

* 複数回 `await` する。
* 複数回 `AsTask()` を呼び出す。
* 操作がまだ完了していないときに `.Result` または `.GetAwaiter().GetResult()` を使用する
* これらの手法の 1 つ以上を使用して複数回インスタンスを操作します。
