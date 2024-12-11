using UnityEngine;
using UnityEditor;

public class StairsObject : ScriptableObject
{
    [MenuItem("Assets/Create/Game Database/Stairs Object")]
    public static void CreateMyAsset()
    {
        StairsObject asset = ScriptableObject.CreateInstance<StairsObject>();

        string name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/Database/StairsObjects/StairsObject.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    public int id;
    public Stairs.Type type;
    public Sprite sprite;
    public int goToLevelId;
}
