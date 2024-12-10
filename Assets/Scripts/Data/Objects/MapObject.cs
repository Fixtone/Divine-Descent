using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapObject : ScriptableObject
{
    [MenuItem("Assets/Create/Game Database/Map Object")]
    public static void CreateMyAsset()
    {
        MapObject asset = ScriptableObject.CreateInstance<MapObject>();

        string name = UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/Database/MapObjects/MapObject.asset");
        AssetDatabase.CreateAsset(asset, name);
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    public int id;
    public GameMapTypes gameMapType;
    public int width;
    public int height;
    public int maxRooms;
    public int roomMaxSize;
    public int roomMinSize;
    [Range(0.0f, 1.0f)]
    public float fogIntensity;
}
