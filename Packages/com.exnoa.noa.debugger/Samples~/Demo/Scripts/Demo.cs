#if NOA_DEBUGGER
using UnityEngine.Profiling;
using NoaDebugger;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace NoaDebuggerDemo
{
    public class Demo : MonoBehaviour
    {
        static bool isInitialized = false;

        [SerializeField]
        Button button;

    #if NOA_DEBUGGER
        const string KEY_UI_ELEMENT_BUTTON_HIDE_PROFILER_INFO = "HideProfilerMemoryInfoKey";
        const string KEY_UI_ELEMENT_TEXT_TOTAL_ALLOCATE_MEMORY = "TotalAllocateMemoryKey";
        const string KEY_UI_ELEMENT_TEXT_MONO_USED_SIZE = "MonoUsedSizeKey";
        const string KEY_UI_ELEMENT_TEXT_TOTAL_RESERVED_MEMORY = "TotalReservedMemoryKey";
        const string KEY_UI_ELEMENT_TEXT_MONO_HEAP_SIZE = "MonoHeapSizeKey";
        const string KEY_UI_ELEMENT_TEXT_RUNTIME_MEMORY_SIZE = "RuntimeMemorySizeKey";
        const int UI_ELEMENT_FONT_SIZE = 120;
    #endif

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
                new DebugCommandRegistrationSample().Initialize();
                isInitialized = true;
            }
    #endif
            button.onClick.AddListener(OnExecuteProfilerApi);
        }

        void OnExecuteProfilerApi()
        {
    #if NOA_DEBUGGER
            NoaUIElement.RegisterUIElement(NoaUITextElement.Create(KEY_UI_ELEMENT_TEXT_TOTAL_ALLOCATE_MEMORY, () => {
                var totalAllocatedMemory = Profiler.GetTotalAllocatedMemoryLong();
                string memory = $"{((double)totalAllocatedMemory / 1024f / 1024f):0.0}MB";
                return $"<size={UI_ELEMENT_FONT_SIZE}%>Total AllocatedMemory:{memory}";
            }, AnchorType.UpperRight));

            NoaUIElement.RegisterUIElement(NoaUITextElement.Create(KEY_UI_ELEMENT_TEXT_MONO_USED_SIZE, () => {
                var monoUsedSize = Profiler.GetMonoUsedSizeLong();
                string size = $"{((double)monoUsedSize / 1024f / 1024f):0.0}MB";
                return $"<size={UI_ELEMENT_FONT_SIZE}%>MonoUsedSize:{size}";
            }, AnchorType.UpperRight));

            NoaUIElement.RegisterUIElement(NoaUITextElement.Create(KEY_UI_ELEMENT_TEXT_TOTAL_RESERVED_MEMORY, () => {
                var totalReservedMemory = Profiler.GetTotalReservedMemoryLong();
                string memory = $"{((double)totalReservedMemory / 1024f / 1024f):0.0}MB";
                return $"<size={UI_ELEMENT_FONT_SIZE}%>Total ReservedMemory:{memory}";
            }, AnchorType.UpperRight));

            NoaUIElement.RegisterUIElement(NoaUITextElement.Create(KEY_UI_ELEMENT_TEXT_MONO_HEAP_SIZE, () => {
                var monoHeapSize = Profiler.GetMonoHeapSizeLong();
                string memory = $"{((double)monoHeapSize / 1024f / 1024f):0.0}MB";
                return $"<size={UI_ELEMENT_FONT_SIZE}%>MonoHeapSize:{memory}";
            }, AnchorType.UpperRight));

            long runtimeMemorySize = 0;
            foreach (var o in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                runtimeMemorySize += Profiler.GetRuntimeMemorySizeLong(o);
            }
            string memory = $"{((double)runtimeMemorySize / 1024f):0.0}KB";
            NoaUIElement.RegisterUIElement(NoaUITextElement.Create(KEY_UI_ELEMENT_TEXT_RUNTIME_MEMORY_SIZE, $"<size={UI_ELEMENT_FONT_SIZE}%>RuntimeMemorySize:{memory}", AnchorType.UpperRight));

            NoaUIElement.RegisterUIElement(NoaUIButtonElement.Create(KEY_UI_ELEMENT_BUTTON_HIDE_PROFILER_INFO, $"<size={UI_ELEMENT_FONT_SIZE}%>HideProfilerInfo", HideProfilerMemoryInfo, AnchorType.UpperRight));
    #endif
        }

        void HideProfilerMemoryInfo()
        {
    #if NOA_DEBUGGER
            NoaUIElement.UnregisterUIElement(KEY_UI_ELEMENT_TEXT_TOTAL_ALLOCATE_MEMORY);
            NoaUIElement.UnregisterUIElement(KEY_UI_ELEMENT_TEXT_MONO_USED_SIZE);
            NoaUIElement.UnregisterUIElement(KEY_UI_ELEMENT_TEXT_TOTAL_RESERVED_MEMORY);
            NoaUIElement.UnregisterUIElement(KEY_UI_ELEMENT_TEXT_MONO_HEAP_SIZE);
            NoaUIElement.UnregisterUIElement(KEY_UI_ELEMENT_TEXT_RUNTIME_MEMORY_SIZE);
            NoaUIElement.UnregisterUIElement(KEY_UI_ELEMENT_BUTTON_HIDE_PROFILER_INFO);
    #endif
        }
    }
}
