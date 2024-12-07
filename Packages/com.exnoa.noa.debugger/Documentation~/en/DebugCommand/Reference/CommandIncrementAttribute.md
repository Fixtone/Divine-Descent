# CommandIncrementAttribute

```csharp
public CommandIncrementAttribute(object increment)
```

## Parameters

| Parameter | Description                        |
|-----------|------------------------------------|
| increment | Amount of increment and decrement. |

## Descriptions

Specifies the amount of increase or decrease in value by the drag operation of a numeric type property.

If no specification is given, a number less than or equal to 0 is set, or if the type of the target property could not be casted, it will increment or decrement by 1.

If it is specified for a property of another type, it will be ignored and cannot be specified for a method.

The setting status will be displayed in the detail view.

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandIncrement(0.5f)]
    public float ExampleProperty
    {
        get;
        set;
    }
}
#endif
```
