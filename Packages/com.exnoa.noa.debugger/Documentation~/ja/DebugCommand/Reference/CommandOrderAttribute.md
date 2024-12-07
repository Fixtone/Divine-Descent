# CommandOrderAttribute

```csharp
public CommandOrderAttribute(int order)
```

## パラメータ

| パラメータ名 | 説明  |
|:-------|:----|
| order  | 並び順 |

## 説明

コマンドの並び順を指定します。

指定された値の小さいものから順に表示します。

指定がない場合は読み込み順かつ並び順を指定したコマンドよりも後ろに並びます。

プロパティ・メソッド共に指定できます。

詳細ビューに設定状況を表示します。

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandOrder(1)]
    public int ExampleProperty
    {
        get;
        set;
    }
}
#endif
```
