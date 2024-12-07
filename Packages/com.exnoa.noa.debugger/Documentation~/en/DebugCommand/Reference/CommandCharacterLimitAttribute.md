# CommandCharacterLimitAttribute

```csharp
public CommandCharacterLimitAttribute(int limit)
```

## Parameters

| Parameter | Description                         |
|-----------|-------------------------------------|
| limit     | Maximum number of input characters. |

## Descriptions

Specifies the maximum number of input characters for a string property.

If a number less than or equal to 0 is set, the maximum number of characters is processed as unlimited.

If it is specified for a property of another type, it will be ignored and cannot be specified for a method.

The setting status will be displayed in the detail view.

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    [CommandCharacterLimit(20)]
    public string ExampleProperty
    {
        get;
        set;
    }
}
#endif
```
