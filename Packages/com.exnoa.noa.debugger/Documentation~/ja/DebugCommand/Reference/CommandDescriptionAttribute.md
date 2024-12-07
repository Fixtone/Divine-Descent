# CommandDescriptionAttribute

```csharp
public CommandDescriptionAttribute(string text)
```

## パラメータ

| パラメータ名 | 説明  |
|:-------|:----|
| text   | 説明文 |

## 説明

詳細ビューに表示するコマンドの説明文を指定します。

プロパティ・メソッド共に指定できます。

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandDescription("このコマンドはintプロパティのサンプルです。")]
    public int ExampleProperty
    {
        get;
        set;
    }
}
#endif
```
