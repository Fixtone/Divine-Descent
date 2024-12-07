# DisplayNameAttribute

```csharp
public DisplayNameAttribute(string name)
```

## Parameters

| Parameter | Description           |
|-----------|-----------------------|
| name      | Command display name. |

## Descriptions

Displays the string specified in `name` on the NOA Debugger.

If not specified, the property name or method name will be displayed.

It can be specified for both properties and methods.

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
