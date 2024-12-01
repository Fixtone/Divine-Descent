using System.Collections;
using System.Collections.Generic;
using System.IO;
using RogueSharp;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public static FileManager Instance;

    [SerializeField]
    private string persistentDataPath;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        persistentDataPath = Application.persistentDataPath;
    }

    public void SaveGame()
    {
        string mapPath = Application.persistentDataPath + "/Map.json";
        
        MapSave mapSave = MapManager.Instance.SerializeCurrentMap();
        
        string json = JsonUtility.ToJson(mapSave);

        File.WriteAllText(mapPath, json);
    }

    public void LoadGame()
    {
        MapSave mapSave = new MapSave();
        string mapPath = Application.persistentDataPath + "/Map.json";

        if (File.Exists(mapPath))
        {
            string saveString = File.ReadAllText(mapPath);
            mapSave = JsonUtility.FromJson<MapSave>(saveString);
        }
        else
        {
            return;
        }

        MapManager.Instance.DeSerializeCurrentMap(mapSave);
        MapManager.Instance.currentMap.UpdatePlayerFieldOfView(GameManager.Instance.player.GetComponent<Player>());
        TileManager.Instance.Draw();
    }
}
