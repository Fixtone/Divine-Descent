# CommandTagAttribute

```csharp
public CommandTagAttribute(string tag)
```

## パラメータ

| パラメータ名 | 説明  |
|--------|-----|
| tag    | タグ名 |

## 説明

コマンドのタグを指定します。コマンドに対して何らかの操作を行う際にこのタグを指定します。

プロパティ・メソッド共に指定できます。

詳細ビューに設定状況を表示します。

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandTag("Tag1")]
    public int ExampleProperty
    {
        get;
        set;
    }
}
#endif
```
