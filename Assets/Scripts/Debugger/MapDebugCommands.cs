using NoaDebugger;
using Unity.VisualScripting;
using UnityEngine;

public class MapDebugCommands
{
    public static string MAP_CATEGORY_NAME = "Map";
    //const string MAP_ACTIONS_CATEGORY_NAME = "Actions";
    //const string MAP_ADD_DEBUG_COMMANDS_NAME = "Commands";

    public void Initialize()
    {
#if NOA_DEBUGGER
        //AddCategory();
        //AddGroup();
        AddDebugCommand();

        DebugCommandRegister.RefreshProperty();
#endif
    }

#if NOA_DEBUGGER
    // void AddCategory()
    // {
    //     DebugCommandRegister.AddCategory<MapCategory>(MAP_CATEGORY_NAME, 1, MAP_CATEGORY_NAME);
    // }

    // void AddGroup()
    // {

    // }

    void AddDebugCommand()
    {
        DebugCommandRegister.AddCategory<MapCategoryDebugCommands>(
                MapDebugCommands.MAP_CATEGORY_NAME);
    }
#endif
}
