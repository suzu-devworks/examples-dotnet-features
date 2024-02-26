# Examples.Features.CS120.Tests

## C# 12.0 Features

* [What's new in C# 12 - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-12)

## Since

- version 12.0
  - November 2023
  - .NET 8.0
  - Visual Studio 2022 version 17.8

## Table of contents. <!-- omit in toc -->

- [Examples.Features.CS120.Tests](#examplesfeaturescs120tests)
  - [C# 12.0 Features](#c-120-features)
  - [Since](#since)
  - [Features](#features)
    - [Primary constructors](#primary-constructors)
    - [Collection expressions](#collection-expressions)
    - [ref readonly parameters](#ref-readonly-parameters)
    - [Default lambda parameters](#default-lambda-parameters)
    - [Alias any type](#alias-any-type)
    - [Inline arrays](#inline-arrays)
    - [Experimental attribute](#experimental-attribute)
    - [Interceptors](#interceptors)

## Features

### Primary constructors

> プライマリ コンストラクター

すべての class と struct で、プライマリ コンストラクターを作成できるようになりました。

プライマリコンストラクターを使用する場合には面倒なメンバー変数の宣言を省略するだけではなく、
他のコンストラクターを宣言する場合にプライマリコンストラクターの呼び出しを強制することができます。

プライマリ コンストラクター パラメーターは、クラス定義全体のスコープ内にある場合でも、パラメーターとして扱うためのルールがあります。

- プライマリ コンストラクター パラメーターは、必要がない場合には保存されない場合があります。
- プライマリ コンストラクター パラメーターはクラスのメンバーではありません。`this.` ではアクセスできません。
- プライマリ コンストラクター パラメーターは割当先になることができます。
- `record` 型の場合を除き、プライマリ コンストラクター パラメーターはプロパティにはなりません。

プライマリ コンストラクター パラメーターの主な用途は次のとおりです。

- base()コンストラクターを呼び出す際の引数とします。
- メンバーフィールドまたはプロパティを初期化します。
- インスタンスメンバーからコンストラクターパラメータを参照します。


### Collection expressions

> コレクション式

### ref readonly parameters

> `ref readonly` パラメーター

### Default lambda parameters

> 既定のラムダ パラメーター

### Alias any type

> 任意の型の別名設定

### Inline arrays

> インライン配列

### Experimental attribute

> 試験段階の属性

### Interceptors

> インターセプター
