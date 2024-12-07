#if NOA_DEBUGGER
using NoaDebugger;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace NoaDebuggerDemo
{
    public class DemoSceneCategoryDebugCommands : DebugCategoryBase
    {
        public enum SampleEnum
        {
            Value1,
            Value2,
            Value3
        }

        const string GET_ONLY_PROPERTY_GROUP_NAME = "Get-Only Properties";
        const string MUTABLE_PROPERTY_GROUP_NAME = "Mutable Properties";
        const string METHOD_GROUP_NAME = "Methods";

        readonly MethodHandler handleMethodCompleteHandler = new ();

        [CommandGroup(GET_ONLY_PROPERTY_GROUP_NAME), DisplayName("Get-Only Bool"), CommandOrder(1)]
        public bool GetOnlyBool { get; } = false;

        [CommandGroup(GET_ONLY_PROPERTY_GROUP_NAME), DisplayName("Get-Only Byte"), CommandOrder(2)]
        public byte GetOnlyByte { get; } = 0;

        [CommandGroup(GET_ONLY_PROPERTY_GROUP_NAME), DisplayName("Get-Only Enum"), CommandOrder(3)]
        public SampleEnum GetOnlyEnum { get; } = SampleEnum.Value1;

        [CommandGroup(GET_ONLY_PROPERTY_GROUP_NAME), DisplayName("Get-Only Int"), CommandOrder(4)]
        public int GetOnlyInt { get; } = 0;

        [CommandGroup(GET_ONLY_PROPERTY_GROUP_NAME), DisplayName("Get-Only String"), CommandOrder(5)]
        public string GetOnlyString { get; } = "Value1";

        [CommandGroup(MUTABLE_PROPERTY_GROUP_NAME), DisplayName("Bool"), CommandOrder(1)]
        public bool MutableBool { get; set; } = false;

        [CommandGroup(MUTABLE_PROPERTY_GROUP_NAME), DisplayName("Byte"), CommandOrder(2)]
        public byte MutableByte { get; set; } = 0;

        [CommandGroup(MUTABLE_PROPERTY_GROUP_NAME), DisplayName("Enum"), CommandOrder(3)]
        public SampleEnum MutableEnum { get; set; } = SampleEnum.Value1;

        [CommandGroup(MUTABLE_PROPERTY_GROUP_NAME), DisplayName("Int"), CommandOrder(4)]
        public int MutableInt { get; set; } = 0;

        [CommandGroup(MUTABLE_PROPERTY_GROUP_NAME), DisplayName("String"), CommandOrder(5)]
        public string MutableString { get; set; } = "Value1";

        [CommandGroup(METHOD_GROUP_NAME), DisplayName("Method")]
        public void Method() => UnityEngine.Debug.Log("Push Method Button");

        [CommandGroup(METHOD_GROUP_NAME), DisplayName("Coroutine")]
        public IEnumerator CoroutineMethod()
        {
            Debug.Log("Start Coroutine");
            yield return new WaitForSeconds(1f);
            Debug.Log("End Coroutine");
        }

        [CommandGroup(METHOD_GROUP_NAME), DisplayName("Start HandleMethod")]
        public MethodHandler StartHandler()
        {
            Debug.Log("Start MethodHandler");
            handleMethodCompleteHandler.IsDone = false;
            WaitAndCompleteHandler().ContinueWith(task => { });
            return handleMethodCompleteHandler;
        }

        async Task WaitAndCompleteHandler()
        {
            await Task.Run(() => { Thread.Sleep(1000); });
            handleMethodCompleteHandler.IsDone = true;
            Debug.Log("Complete MethodHandler");
        }
    }
}
#endif
