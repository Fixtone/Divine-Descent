# CommandTagAttribute

```csharp
public CommandTagAttribute(string tag)
```

## Parameters

| Parameter | Description |
|-----------|-------------|
| tag       | Tag name.   |

## Descriptions

Specifies the tag of the command. This tag is specified when performing any operations to the command.

It can be specified for both properties and methods.

The setting status will be displayed in the detail view.

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandTag("Tag1")]
    public int ExampleProperty
    {
        get;
        set;
    }
}
#endif
```
