# Excluding the Tool from Compile

Explains how to compile by removing the tool in a specific environment.

**In a release environment, always build the application by excluding NOA Debugger.**

## Command Line

By executing `NoaDebugger.NoaPackageManager.ExcludeFromCompile` from the command line before building the application,
you can exclude NOA Debugger from the compilation target.

For details about the command line provided by Unity, please refer to
the [Unity Manual](https://docs.unity3d.com/Manual/CommandLineArguments.html).

```Bash
# Windows
"C:\Program Files\Unity\Hub\Editor\<unity-version>\Editor\Unity.exe" -quit -batchmode -nographics -projectPath <project-path> -executeMethod NoaDebugger.NoaPackageManager.ExcludeFromCompile

# Mac
/Applications/Unity/Hub/Editor/<unity-version>/Unity.app/Contents/MacOS/Unity -quit -batchmode -nographics -projectPath <project-path> -executeMethod NoaDebugger.NoaPackageManager.ExcludeFromCompile
```

### How to Restore NOA Debugger Excluded from Compilation

Explains how to build after excluding NOA Debugger from the compilation target and return to the state where the
original NOA Debugger was included.

By executing `NoaDebugger.NoaPackageManager.IncludeInCompile` from the command line after building the application, you
can restore it.

```Bash
# Windows
"C:\Program Files\Unity\Hub\Editor\<unity-version>\Editor\Unity.exe" -quit -batchmode -nographics -projectPath <project-path> -executeMethod NoaDebugger.NoaPackageManager.IncludeInCompile

# Mac
/Applications/Unity/Hub/Editor/<unity-version>/Unity.app/Contents/MacOS/Unity -quit -batchmode -nographics -projectPath <project-path> -executeMethod NoaDebugger.NoaPackageManager.IncludeInCompile
```

## NOA Debugger Editor

Select `Window -> NOA Debugger` from the Unity menu to launch the NOA Debugger Editor.

You can exclude it from the compilation target by pressing the [Exclude from compile] button in the `Package` item in
the window.

**Note:** You can use this if you want to check on the Unity Editor whether any compilation errors, etc. will occur
before removing it from the command line in the CI/CD environment.

![noa-debugger-package-exclude-from-compile](../img/noa-debugger-package-exclude-from-compile.png)

### How to Restore NOA Debugger Excluded from Compilation

Explains how to build after excluding NOA Debugger from the compilation target and return to the state where the
original NOA Debugger was included.

Select `Window -> NOA Debugger` from the Unity menu to launch the NOA Debugger Editor.

You can restore it by pressing the [Include in compile] button in the `Package` item in the window.

## Other Notes

When NOA Debugger is introduced, it automatically sets the `NOA_DEBUGGER` symbol definition in the Scripting Define
Symbols. Just removing this symbol definition will not completely remove NOA Debugger, as some resource data may still
be included in the application. Therefore, always execute the above command to exclude it from the compilation.
