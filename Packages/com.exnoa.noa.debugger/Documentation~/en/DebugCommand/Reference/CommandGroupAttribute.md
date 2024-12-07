# CommandGroupAttribute

```csharp
public CommandGroupAttribute(string name)

public CommandGroupAttribute(string name, int order)
```

## Parameters

| Parameter | Description         |
|-----------|---------------------|
| name      | Group name.         |
| order     | Order of the group. |

## Descriptions

Specifies the group that contains the command.

If `name` is not specified, it will be included in 'Others'.

`order` displays groups in order from the smallest specified value.

If not specified, it will be arranged after the groups in the order in which they were read and the groups with a specified `order`.

**Note:** Since `order` applies the last specified value in the reading order, it is sufficient to specify it only once during use.

It can be specified for both properties and methods.

The setting status will be displayed in the detail view.

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

    // In this case, Group2 will be arranged before Group1 which has no specified order.
    [CommandGroup("Group2", 1)]
    public int ExampleProperty2
    {
        get;
        set;
    }

    // Since Group2 already has an order specified, there is no need to specify it again here.
    [CommandGroup("Group2")]
    public int ExampleProperty3
    {
        get;
        set;
    }
}
#endif
```
