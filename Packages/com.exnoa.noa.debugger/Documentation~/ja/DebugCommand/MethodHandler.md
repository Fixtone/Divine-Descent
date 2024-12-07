# MethodHandlerについて

`NoaDebugger.MethodHandler`を利用して、DebugCommand機能で即時終了しない処理の待機を行うことができます。<br>
当該コマンドの処理完了後、`MethodHandler.IsDone`を`true`に変更し処理の終了をNOA Debuggerに伝えてください。<br>
`MethodHandler.IsDone`が`false`の間、対象コマンド要素は押下できません。

## サンプルコード

```csharp
#if NOA_DEBUGGER
using NoaDebugger;

public class DebugCommandSample : DebugCategoryBase
{
    MethodHandler _handler = new MethodHandler();

    public MethodHandler HandleMethod()
    {
        // 何らかの処理

        _handler.IsDone = false;
        return _handler;
    }

    void _Exec()
    {
        // 何らかの処理

        // 処理完了後フラグを上げます
        _handler.IsDone = true;
    }
}
#endif
```
