# CommandOrderAttribute

```csharp
public CommandOrderAttribute(int order)
```

## Parameters

| Parameter | Description           |
|-----------|-----------------------|
| order     | Order of the command. |

## Descriptions

Specifies the order of the commands. They are displayed in order from the smallest specified value.

If not specified, it will be arranged after the commands in the order in which they were read and the commands with the
specified order.

It can be specified for both properties and methods.

The setting status will be displayed in the detail view.

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
