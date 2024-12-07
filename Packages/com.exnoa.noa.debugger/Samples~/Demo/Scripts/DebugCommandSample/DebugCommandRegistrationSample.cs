#if NOA_DEBUGGER
using System.Threading;
using System.Threading.Tasks;
using NoaDebugger;
#endif
using System;
using System.Collections;
using UnityEngine;

namespace NoaDebuggerDemo
{
    public class DebugCommandRegistrationSample
    {
#if NOA_DEBUGGER

        const string CATEGORY1_NAME = "DemoSceneCategory1";
        const string CATEGORY2_NAME = "DemoSceneCategory2";
        const string CATEGORY3_NAME = "DemoSceneCategory3";
        const string CATEGORY_ADD_GROUP_NAME = "DemoSceneCategory_OrderedGroups";
        const string CATEGORY_ADD_DEBUG_COMMAND_NAME = "DemoSceneCategory_AddDebugCommand";
        const string CATEGORY_ADD_DYNAMIC_DEBUG_COMMAND_NAME = "DemoSceneCategory_AddDynamicDebugCommand";
        const string PROPERTY_GROUP_NAME = "Properties";
        const string METHOD_GROUP_NAME = "Methods";

        int GetOnlyInt { get; } = 0;
        int MutableInt { get; set; } = 0;

        readonly MethodHandler handleMethodCompleteHandler = new ();

#endif
        public void Initialize()
        {
#if NOA_DEBUGGER
            AddCategorySample();

            AddGroupSample();

            AddDebugCommandSample();

            AddDynamicDebugCommandSample();

            DebugCommandRegister.RefreshProperty();
#endif
        }

        IEnumerator Wait()
        {
            Debug.Log("Start Dynamic Added Coroutine");
            yield return new WaitForSeconds(1);
            Debug.Log("End Dynamic Added Coroutine");
        }

#if NOA_DEBUGGER
        MethodHandler StartHandler()
        {
            Debug.Log("Start Dynamic Added MethodHandler");
            handleMethodCompleteHandler.IsDone = false;
            WaitAndCompleteHandler().ContinueWith(task => { });
            return handleMethodCompleteHandler;
        }

        async Task WaitAndCompleteHandler()
        {
            await Task.Run(() => { Thread.Sleep(1000); });
            handleMethodCompleteHandler.IsDone = true;
            Debug.Log("Complete Dynamic Added MethodHandler");
        }

        void AddCategorySample()
        {
            DebugCommandRegister.AddCategory<DemoSceneCategory>(
                CATEGORY1_NAME,
                3,
                $"{CATEGORY1_NAME}_Order3");

            DebugCommandRegister.AddCategory<DemoSceneCategory>(
                CATEGORY2_NAME,
                2,
                $"{CATEGORY2_NAME}_Order2");

            DebugCommandRegister.AddCategory<DemoSceneCategory>(
                CATEGORY3_NAME,
                1,
                $"{CATEGORY3_NAME}_Order1");
        }

        void AddGroupSample()
        {
            DebugCommandRegister.AddCategory<DemoSceneCategoryOrderedGroups>(
                CATEGORY_ADD_GROUP_NAME);
        }

        void AddDebugCommandSample()
        {
            DebugCommandRegister.AddCategory<DemoSceneCategoryDebugCommands>(
                DebugCommandRegistrationSample.CATEGORY_ADD_DEBUG_COMMAND_NAME);
        }

        void AddDynamicDebugCommandSample()
        {
            CommandDefinition commandGetOnlyInt = DebugCommandRegister.CreateGetOnlyIntProperty(
                CATEGORY_ADD_DYNAMIC_DEBUG_COMMAND_NAME,
                "GetOnly-Int",
                () => GetOnlyInt,
                new Attribute[]
                {
                    new CommandGroupAttribute(PROPERTY_GROUP_NAME),
                });
            DebugCommandRegister.AddCommand(commandGetOnlyInt);

            CommandDefinition commandMutableInt = DebugCommandRegister.CreateMutableIntProperty(
                CATEGORY_ADD_DYNAMIC_DEBUG_COMMAND_NAME,
                "Mutable-Int",
                () => MutableInt,
                (value) => MutableInt = value,
                new Attribute[]
                {
                    new CommandGroupAttribute(PROPERTY_GROUP_NAME),
                });
            DebugCommandRegister.AddCommand(commandMutableInt);

            CommandDefinition commandMethod = DebugCommandRegister.CreateMethod(
                CATEGORY_ADD_DYNAMIC_DEBUG_COMMAND_NAME,
                "Method",
                () => UnityEngine.Debug.Log("Push Dynamic Added Method Button"),
                new Attribute[]
                {
                    new CommandGroupAttribute(METHOD_GROUP_NAME),
                });
            DebugCommandRegister.AddCommand(commandMethod);

            CommandDefinition commandCoroutine = DebugCommandRegister.CreateCoroutine(
                CATEGORY_ADD_DYNAMIC_DEBUG_COMMAND_NAME,
                "Coroutine",
                Wait,
                new Attribute[]
                {
                    new CommandGroupAttribute(METHOD_GROUP_NAME),
                });
            DebugCommandRegister.AddCommand(commandCoroutine);

            CommandDefinition commandStartHandleMethod = DebugCommandRegister.CreateHandleMethod(
                CATEGORY_ADD_DYNAMIC_DEBUG_COMMAND_NAME,
                "Start HandleMethod",
                StartHandler,
                new Attribute[]
                {
                    new CommandGroupAttribute(METHOD_GROUP_NAME),
                });
            DebugCommandRegister.AddCommand(commandStartHandleMethod);
        }
#endif
    }
}
