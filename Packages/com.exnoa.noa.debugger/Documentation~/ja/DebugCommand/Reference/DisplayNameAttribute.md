# DisplayNameAttribute

```csharp
public DisplayNameAttribute(string name)
```

## パラメータ

| パラメータ名 | 説明    |
|:-------|:------|
| name   | コマンド名 |

## 説明

`name`に指定した文字列をNOA Debugger上で表示します。

指定がない場合はプロパティ名またはメソッド名を表示します。

プロパティ・メソッド共に指定できます。

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [DisplayName("DisplayName")]
    public int ExampleProperty
    {
        get;
        set;
    }
}
#endif

```
