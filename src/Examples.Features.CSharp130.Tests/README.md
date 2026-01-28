# C# version 13.0 (Examples.Features.CSharp130.Tests)

## Table of contents. <!-- omit in toc -->

- [Overview](#overview)
  - [Since](#since)
- [Features](#features)
  - [params collections](#params-collections)
  - [New lock type and semantics](#new-lock-type-and-semantics)
  - [New escape sequence - `\e`](#new-escape-sequence---e)
  - [Method group natural type improvements](#method-group-natural-type-improvements)
  - [Implicit indexer access in object initializers](#implicit-indexer-access-in-object-initializers)
  - [Enable ref locals and unsafe contexts in iterators and async methods](#enable-ref-locals-and-unsafe-contexts-in-iterators-and-async-methods)
  - [Enable ref struct types to implement interfaces](#enable-ref-struct-types-to-implement-interfaces)
  - [Allow ref struct types as arguments for type parameters in generics](#allow-ref-struct-types-as-arguments-for-type-parameters-in-generics)
  - [Partial properties and indexers are now allowed in partial types](#partial-properties-and-indexers-are-now-allowed-in-partial-types)
  - [Overload resolution priority allows library authors to designate one overload as better than others](#overload-resolution-priority-allows-library-authors-to-designate-one-overload-as-better-than-others)

## Overview

- [What's new in C# 13 - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-13)
- [The history of C# - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-version-history#c-version-13)

### Since

- version 13.0
  - November 2024
  - .NET 9.0
  - Visual Studio 2022 version 17.12

## Features

### params collections

> params コレクション

### New lock type and semantics

> 新しい lock 型とセマンティクス。

### New escape sequence - `\e`

> 新しいエスケープ シーケンス - `\e`

### Method group natural type improvements

> メソッド グループ自然型の改善

### Implicit indexer access in object initializers

> オブジェクト初期化子での暗黙的インデクサー アクセス

### Enable ref locals and unsafe contexts in iterators and async methods

> イテレーターと非同期メソッドで、ref ローカル変数と unsafe コンテキストを有効にする

### Enable ref struct types to implement interfaces

> ref struct 型を有効にして、インターフェイスを実装します。

### Allow ref struct types as arguments for type parameters in generics

> ジェネリックの型パラメーターの引数として ref 構造体型を許可します。

### Partial properties and indexers are now allowed in partial types

> 部分プロパティとインデクサー が partial 型で許可されるようになりました。

### Overload resolution priority allows library authors to designate one overload as better than others

> オーバーロード解決の優先順位 を使用すると、ライブラリ作成者は 1 つのオーバーロードを他のオーバーロードよりも優れたオーバーロードとして指定できます。
