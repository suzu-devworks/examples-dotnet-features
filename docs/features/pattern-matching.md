# Pattern matching <!-- omit in toc -->

Use `is` expressions, `switch` statements, and `switch` expressions to match an input expression against
any number of patterns.

## Pattern types <!-- omit in toc -->

- [Declaration and type patterns](#declaration-and-type-patterns)
- [Constant patterns](#constant-patterns)
- [Discard patterns](#discard-patterns)
- [Positional patterns](#positional-patterns)
- [Property patterns](#property-patterns)
- [Logical patterns](#logical-patterns)
- [Relational patterns](#relational-patterns)
- [List patterns](#list-patterns)

### Declaration and type patterns

> 宣言パターンと型パターン

Checks the runtime type of an expression and, if the match succeeds, assigns the result to the declared
variable.

C# 7.0 introduced this.

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
```

C# 7.1 added pattern matching for generic type parameters.

```cs
public void Method<T>(T value)
{
    if (value is <type> <variable>)
    {
        // ...
    }
}
```

C# 8.0 introduced `switch` expressions.

```cs
return value switch 
{
    <type> <variable> => // ...
    // ...
}
```

C# 9.0 lets `switch` statements and `switch` expressions omit discard variables.

```cs
    switch (value)
    {
        case <type>:
            // ...
    }
```

```cs
    return value switch 
    {
        <type> => // ...
        // ...
    }
```

### Constant patterns

> 定数パターン

Tests whether the result of an expression is equal to a specified constant.

Constant patterns can use any of the following constant expressions:

- Integer or floating-point numeric literals
- `char`
- String literals
- Boolean values `true` or `false`
- Enumeration values
- The name of a declared constant field or local
- `null`

C# 7.0 introduced this.

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

C# 8.0 introduced `switch` expressions.

```cs
return value switch 
{
    <constant> => // ...
    // ...
}
```

> var パターン

Matches any expression, including `null`, and assigns the result to the declared variable.

- Introduced in C# 7.0

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

Matches any expression in a `switch`.

C# 8.0 introduced this.

```cs
return value switch 
{
    _ => // ...
}
```

### Positional patterns

> 位置指定パターン

Deconstructs the result of an expression and tests whether the resulting values match nested patterns.

Tuple-based patterns are also positional patterns.

C# 8.0 introduced this.

```cs
return value switch
{
    <type>(item1, item2, item3 ... ) => //...
    //...]
};
```

### Property patterns

> プロパティ パターン

Tests whether the properties or fields of an expression match nested patterns.

C# 8.0 introduced this.

```cs
if ( value is { property: <constant>} <variable>)
{
    //...
}
```

```cs
switch (value)
{
    case value is { property: <constant>} <variable>:
        // ...
}
```

```cs
return value switch 
{
    <type> { property: <constant>} <variable> => //...
    // ...
}
```

You can also use empty `{ }` patterns instead of `is not null` when you want to capture the value
into a variable.

```cs
if (value is { } <variable>)
{
    //...
}
```

### Logical patterns

> 論理パターン

Tests whether an expression matches a logical combination of patterns.

C# 9.0 introduced this.

### Relational patterns

> リレーショナル パターン

Compares the result of an expression against a specified constant.

C# 9.0 introduced this.

### List patterns

> リスト パターン

Tests whether sequence elements match the corresponding nested patterns.

C# 11.0 introduced this.

```cs
    if (value is [ pattern (, pattern)* ])
    {
        // ...
    }
```

Because sequence elements are matched against *nested patterns* rather than constants alone, you can:

- Use discard patterns to match any element.
- Capture variables with `var` patterns.
- Evaluate logical patterns such as `and` / `or` and relational patterns such as `>` and `>=`.
- Evaluate property patterns.

and so on.
