#if NOA_DEBUGGER
using NoaDebugger;
namespace NoaDebuggerDemo
{
    public class DemoSceneCategoryOrderedGroups : DebugCategoryBase
    {
        [CommandGroup("Group1_Order3", 3), DisplayName("MethodA")]
        public void Group1Method() { }

        [CommandGroup("Group2_Order2", 2), DisplayName("MethodB")]
        public void Group2Method() { }

        [CommandGroup("Group3_Order1", 1), DisplayName("MethodC")]
        public void Group3Method() { }
    }
}
#endif
