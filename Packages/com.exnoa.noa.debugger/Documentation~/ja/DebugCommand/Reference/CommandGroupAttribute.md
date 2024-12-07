# CommandGroupAttribute

```csharp
public CommandGroupAttribute(string name)

public CommandGroupAttribute(string name, int order)
```

## パラメータ

| パラメータ名 | 説明    |
|:-------|:------|
| name   | グループ名 |
| order  | 並び順   |

## 説明

コマンドを内包するグループを指定します。

`name`の指定がない場合は「Others」に内包します。

`order`は指定された値の小さいグループから順に表示します。<br>
指定がない場合は読み込み順かつ`order`を指定したグループよりも後ろに並びます。<br>
※`order`は読み込み順で最後に指定した値を適用するため、利用の際は1度だけの指定で問題ありません。

プロパティ・メソッド共に指定できます。

詳細ビューに設定状況を表示します。

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandGroup("Group1")]
    public int ExampleProperty
    {
        get;
        set;
    }

    // この場合、Group2はorderを指定していないGroup1よりも前に並びます
    [CommandGroup("Group2", 1)]
    public int ExampleProperty2
    {
        get;
        set;
    }

    // Group2は既にorderの指定を行っているため、ここで再度指定する必要はありません
    [CommandGroup("Group2")]
    public int ExampleProperty3
    {
        get;
        set;
    }
}
#endif
```
