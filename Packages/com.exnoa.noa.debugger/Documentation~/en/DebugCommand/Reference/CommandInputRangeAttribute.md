# CommandInputRangeAttribute

```csharp
public CommandInputRangeAttribute(object min, object max)
```

## Parameters

| Parameter | Description                 |
|-----------|-----------------------------|
| min       | Lower limit of input value. |
| max       | Upper limit of input value. |

## Descriptions

Specifies the input range for numeric type properties.

If the type of the target property could not be casted, the minimum and maximum values of that data type will be set.

If it is specified for a property of another type, it will be ignored and cannot be specified for a method.

The setting status will be displayed in the detail view.

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
