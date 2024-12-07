#if NOA_DEBUGGER
using UnityEngine.Profiling;
using NoaDebugger;
#endif

using UnityEngine;

public class GameDebugger : MonoBehaviour
{
    static bool isInitialized = false;
    // Start is called before the first frame update
    void Awake()
    {
#if NOA_DEBUGGER
        if (!NoaDebug.IsInitialized)
        {
            NoaDebug.Initialize();
            isInitialized = false;
        }

        if (!isInitialized)
        {
            new MapDebugCommands().Initialize();
            isInitialized = true;
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
