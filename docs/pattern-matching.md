# Pattern matching <!-- omit in toc -->

is 式、switch ステートメント、switch 式を使って、入力式を任意の数の特性と照合します。

## Pattern types <!-- omit in toc -->

- [Declaration and type patterns](#declaration-and-type-patterns)
- [Constant patterns](#constant-patterns)
- [var patterns](#var-patterns)
- [Discard patterns](#discard-patterns)
- [Positional patterns](#positional-patterns)
- [Property patterns](#property-patterns)
- [Logical patterns](#logical-patterns)
- [Relational patterns](#relational-patterns)
- [List patterns](#list-patterns)

### Declaration and type patterns

> 宣言パターンと型パターン

式のランタイム型をチェックし、一致が成功した場合は、宣言された変数に式の結果を代入します。

- C# 7.0 から導入 
- `switch` ステートメントの　型パターンは C# 9.0

```cs
    if (value is <type> <variable>)
    {
        // ...
    }
```

```cs
    switch (value)
    {
        case <type> <variable>:
            // ...
    }

    //CS 9.0 〜
    switch (value)
    {
        case <type>: 
            // ...
    }
```

### Constant patterns

> 定数パターン

式の結果が指定された定数と等しいかどうかをテストします。

- C# 7.0 から導入 

定数パターンでは、次のような任意の定数式を使用できます。

- 整数または浮動小数点数値リテラル
- char
- 文字列リテラル
- ブール値 true または false
- 列挙型値
- 宣言された定数フィールドまたはローカルの名前
- null

```cs
    if (value is <constant>)
    {
        // ...
    }

    if (value is null)
    {
        // ...
    }
```

```cs
    switch (value)
    {
        case <constant>:
            // ...
        case null: 
            // ...
    }
```

### var patterns

> var パターン

nullを含む任意の式に一致させ、その結果を宣言された変数に代入します。

- C# 7.0 から導入 

```cs
    if (value is var <variable> && <variable> ... )
    {
        // ...
    }
```

```cs
    switch (value)
    {
        case var <variable> when <variable> ...:
            // ...
    }
```

### Discard patterns

> 破棄パターン

任意の式に一致させます。

- C# 8.0 から導入 

### Positional patterns

> 位置指定パターン

式の結果を分解し、結果の値が入れ子になったパターンに一致するかどうかをテストします。
タプルをつかったパターンもこのパターンです。

- C# 8.0 から導入 

### Property patterns

> プロパティ パターン

 式のプロパティまたはフィールドが入れ子になったパターンに一致するかどうかをテストします。

- C# 8.0 から導入 

### Logical patterns

> 論理パターン

式がパターンの論理的な組み合わせと一致するかどうかをテストします。

- C# 9.0 から導入 

### Relational patterns

> リレーショナル パターン

式の結果と指定された定数を比較します。

- C# 9.0 から導入 

### List patterns

> リスト パターン

シーケンス要素が対応する入れ子になったパターンと一致するかどうかをテストします。

- C# 11.0 から導入 

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
