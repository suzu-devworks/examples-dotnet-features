# Pattern matching <!-- omit in toc -->

is 式、switch ステートメント、switch 式を使って、入力式を任意の数の特性と照合します。

## Pattern types <!-- omit in toc -->

- [Declaration and type patterns](#declaration-and-type-patterns)
- [Constant pattern](#constant-pattern)
- [var pattern](#var-pattern)
- [Discard pattern](#discard-pattern)
- [Positional pattern](#positional-pattern)
- [Property pattern](#property-pattern)
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

### Constant pattern

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

### var pattern

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

### Discard pattern

> 破棄パターン

任意の式に一致させます。

- C# 8.0 から導入 

### Positional pattern

> 位置指定パターン

式の結果を分解し、結果の値が入れ子になったパターンに一致するかどうかをテストします。

- C# 8.0 から導入 

### Property pattern

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
