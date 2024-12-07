# CommandExcludeAttribute

```csharp
public CommandExcludeAttribute()
```

## Parameters

This attribute has no parameters

## Descriptions

Excludes the target from the display of the debug command.

It can be specified for both properties and methods.

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandExclude]
    public void ExampleMethod()
    {
        // Do something
    }
}
#endif
```
