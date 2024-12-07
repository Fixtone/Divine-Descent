# SaveOnUpdateAttribute

```csharp
public SaveOnUpdateAttribute()
```

## パラメータ

パラメータはありません。

## 説明

プロパティに指定することで、値の変更を保存します。

メソッドに指定することはできません。

詳細ビューに設定状況を表示します。

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [SaveOnUpdate]
    public int ExampleProperty
    {
        get;
        set;
    }
}
#endif
```
