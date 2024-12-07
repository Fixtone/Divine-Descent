# SaveOnUpdateAttribute

```csharp
public SaveOnUpdateAttribute()
```

## Parameters

This attribute has no parameters

## Descriptions

By specifying it for a property, the value change will be saved.

It cannot be specified for a method.

The setting status will be displayed in the detail view.

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [SaveOnUpdate]
    public int ExampleProperty
    {
        get;
        set;
    }
}
#endif
```
