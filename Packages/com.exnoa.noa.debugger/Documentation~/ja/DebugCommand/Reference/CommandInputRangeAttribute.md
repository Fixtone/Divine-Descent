# CommandInputRangeAttribute

```csharp
public CommandInputRangeAttribute(object min, object max)
```

## パラメータ

| パラメータ名 | 説明     |
|:-------|:-------|
| min    | 入力値の下限 |
| max    | 入力値の上限 |

## 説明

数値型プロパティの入力可能範囲を指定します。

対象のプロパティの型に変換できなかった場合はそのデータ型の最小値と最大値を設定します。

他の型のプロパティに指定した場合は無視し、メソッドに指定することはできません。

詳細ビューに設定状況を表示します。

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandInputRange(-10, 100)]
    public int ExampleProperty
    {
        get;
        set;
    }
}
#endif
```
