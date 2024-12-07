# MethodHandler

By using `NoaDebugger.MethodHandler`, you can wait for processes that do not terminate immediately with the DebugCommand
feature.

After the completion of the command process, please change `MethodHandler.IsDone` to true to notify NOA Debugger of the
end of the process.

As long as `MethodHandler.IsDone` is false, the target command element cannot be pressed.

## Sample Code

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    MethodHandler _handler = new MethodHandler();

    public MethodHandler HandleMethod()
    {
        // Do something.

        _handler.IsDone = false;
        return _handler;
    }

    void _Exec()
    {
        // Do something.

        // Raises the flag after finished process.
        _handler.IsDone = true;
    }
}
#endif
```
