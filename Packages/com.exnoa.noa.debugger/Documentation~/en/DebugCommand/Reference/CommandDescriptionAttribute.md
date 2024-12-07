# CommandDescriptionAttribute

```csharp
public CommandDescriptionAttribute(string text)
```

## Parameters

| Parameter | Description       |
|-----------|-------------------|
| text      | Description text. |

## Descriptions

Specifies the description text of the command to be displayed in the detail view.

It can be specified for both properties and methods.

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandDescription("This command is a property sample.")]
    public int ExampleProperty
    {
        get;
        set;
    }
}
#endif
```
