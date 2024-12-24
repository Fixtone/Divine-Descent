using UnityEngine;
using UnityEditor;

public class MonsterObject : ScriptableObject
{
    [MenuItem("Assets/Create/Game Database/Monster Object")]
    public static void CreateMyAsset()
    {
        MonsterObject asset = ScriptableObject.CreateInstance<MonsterObject>();

        string name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/Database/MonsterObjects/MonsterObject.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    public Entity.Type type;
    public Monster.SubType subtype;
    public Sprite sprite;
}
