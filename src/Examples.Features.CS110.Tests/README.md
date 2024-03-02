# Examples.Features.C110.Tests

## C# version 11.0

### See also

* [What's new in C# 11 - Microsoft Learn](https://learn.microsoft.com/ja-jp/dotnet/csharp/whats-new/csharp-11)

### Since

- November 8, 2022
- .NET 7.0
- Visual Studio 2022 version 17.4


## Table of contents. <!-- omit in toc -->

- [Examples.Features.C110.Tests](#examplesfeaturesc110tests)
  - [C# version 11.0](#c-version-110)
    - [See also](#see-also)
    - [Since](#since)
  - [Features](#features)
    - [Raw string literals](#raw-string-literals)
    - [\*Generic math support](#generic-math-support)
    - [\*Generic attributes](#generic-attributes)
    - [\*UTF-8 string literals](#utf-8-string-literals)
    - [\*Newlines in string interpolation expressions](#newlines-in-string-interpolation-expressions)
    - [List patterns](#list-patterns)
    - [\*File-local types](#file-local-types)
    - [\*Required members](#required-members)
    - [\*Auto-default structs](#auto-default-structs)
    - [\*Pattern match Span on a constant string](#pattern-match-span-on-a-constant-string)
    - [\*Extended nameof scope](#extended-nameof-scope)
    - [\*Numeric IntPtr](#numeric-intptr)
    - [\*ref fields and scoped ref](#ref-fields-and-scoped-ref)
    - [\*Improved method group conversion to delegate](#improved-method-group-conversion-to-delegate)

## Features

### Raw string literals

> 未加工の文字リテラル

未加工の文字リテラルには、エスケープ シーケンスを必要とせずに、空白文字、改行、埋め込み引用符、その他の特殊文字を含む任意のテキストを含めることができます。 
開始の引用符の後と、終わりの引用符の前の改行は、最終的なコンテンツには含まれません。
終わりの二重引用符の左側にある空白文字は、文字列リテラルから削除されます。

```cs
string longMessage = """
    This is a long message.
    It has several lines.
        Some are indented
                more than others.
    Some should start at the first column.
    Some have "quoted text" in them.
    """;

var location = $$"""
   You are at {{{Longitude}}, {{Latitude}}}
   """;
```

### *Generic math support

> ジェネリック型数値演算のサポート

### *Generic attributes

> 汎用属性

### *UTF-8 string literals

> UTF-8 の文字列リテラル

### *Newlines in string interpolation expressions

> 文字列補間式の改行

### List patterns

> リスト パターン

シーケンス要素が対応する入れ子になったパターンと一致するかどうかをテストします。

```cs
    if (value is [ pattern (, pattern)* ])
    {
        // ...
    }
```

シーケンス要素を評価するのは定数ではなく *入れ子になったパターン* なので

- 任意のパターンを評価するために破棄パターンを使用する。
- `var` パターンで変数をキャプチャする
- `and/or` や `>`,`>=` などの論理パターンを評価する
- プロパティパターンを評価する。

などができます。

### *File-local types

> ファイルローカル型

### *Required members

> 必須メンバー

### *Auto-default structs

> auto-default 構造体

### *Pattern match Span<char> on a constant string

> string 定数での Span<char> のパターン マッチ

### *Extended nameof scope

> 拡張 nameof スコープ

### *Numeric IntPtr

> 数値 IntPtr

### *ref fields and scoped ref

> ref フィールドと scoped ref

### *Improved method group conversion to delegate

> 改善された、メソッド グループからデリゲートへの変換
