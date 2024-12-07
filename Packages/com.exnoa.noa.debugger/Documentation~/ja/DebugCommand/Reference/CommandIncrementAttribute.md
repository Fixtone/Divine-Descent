# CommandIncrementAttribute

```csharp
public CommandIncrementAttribute(object increment)
```

## パラメータ

| パラメータ名    | 説明  |
|:----------|:----|
| increment | 変動値 |

## 説明

数値型プロパティのドラッグ操作による値の増減量を指定します。

指定がない場合や0以下の数値を設定した場合、対象のプロパティの型に変換できなかった場合は1ずつ増減します。

他の型のプロパティに指定した場合は無視し、メソッドに指定することはできません。

詳細ビューに設定状況を表示します。

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandIncrement(0.5f)]
    public float ExampleProperty
    {
        get;
        set;
    }
}
#endif
```
