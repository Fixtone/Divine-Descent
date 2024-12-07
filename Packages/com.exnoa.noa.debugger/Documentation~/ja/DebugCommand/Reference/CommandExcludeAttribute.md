# CommandExcludeAttribute

```csharp
public CommandExcludeAttribute()
```

## パラメータ

パラメータはありません。

## 説明

対象をデバッグコマンドの表示から除外します。

プロパティ・メソッド共に指定できます。

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandExclude]
    public void ExampleMethod()
    {
        // 何らかの処理
    }
}
#endif
```
