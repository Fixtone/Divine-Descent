# CommandCharacterLimitAttribute

```csharp
public CommandCharacterLimitAttribute(int limit)
```

## パラメータ

| パラメータ名 | 説明       |
|:-------|:---------|
| limit  | 入力文字数の上限 |

## 説明

stringプロパティの入力文字数の上限を指定します。

0以下の数値を設定した場合、文字数の上限は無制限として処理します。

他の型のプロパティに指定した場合は無視し、メソッドに指定することはできません。

詳細ビューに設定状況を表示します。

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandCharacterLimit(20)]
    public string ExampleProperty
    {
        get;
        set;
    }
}
#endif
```
