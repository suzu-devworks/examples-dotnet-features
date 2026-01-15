# C# version 4.0 (Examples.Features.CSharp40.Tests)

## Table of contents. <!-- omit in toc -->

- [Overview](#overview)
  - [Since](#since)
- [Features](#features)
  - [Dynamic binding](#dynamic-binding)
  - [Named and optional arguments](#named-and-optional-arguments)
  - [Generic Co- and Contra- variance](#generic-co--and-contra--variance)
  - [Embedded interop types](#embedded-interop-types)

## Overview

- [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-40)

### Since

- April 2010
- .NET Framework 4
- Visual Studio 2010

## Features

### Dynamic binding

> 動的型付け変数

**C# 4.0 の主役。**

DLR(Dynamic Language Runtime)の流行を見越して追加された機能と思われますが・・・。

`dynamic` で次のようなことができるようになります。

- 遅延バインド
- ダックタイピング
- ジェネリクス利用時の静的メソッド呼び出し
- 多重ディスパッチ

### Named and optional arguments

> オプション引数、名前付き引数

### Generic Co- and Contra- variance

> ジェネリックの共変性と反変性

型システムにおいては、共変性、反変性、不変性は、次のように定義されます。

- `Covariance`
  - 最初に指定された型よりも限定的な（強い)派生型を使用できるようにします。
  - for example, `IEnumerable<T>`, `IEnumerator<T>`, `IQueryable<T>`, and `IGrouping<TKey,TElement>`.
- `Contravariance`
  - 最初に指定された型よりも一般的な (弱い) 型を使用できるようにします。
  - for example: `IComparer<T>`, `IComparable<T>`, and `IEqualityComparer<T>`.
- `Invariance`
  - 最初に指定された型のみを使用できることを意味します。

### Embedded interop types

> COM(Embedded) 相互運用型の特別処理

COM 相互運用用のクラス(Runtime Callable Wrapper) を特別あつかいして COM 呼び出しをやりやすくしたそうです。

- COM インタフェースの引数は `ref` キーワードはオプションになった。（ref ばっかりになるので）
- COM オブジェクトのインデックス付きプロパティが使えるようになった。（`get_X(index)`, `Set_X(index, value)` => `X[index]`)
