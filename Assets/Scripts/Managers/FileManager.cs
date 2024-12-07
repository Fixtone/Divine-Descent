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

        string playerPath = Application.persistentDataPath + "/Player.json";
        PlayerSave playerSave = GameManager.Instance.player.GetComponent<Player>().Save();
        string playerJson = JsonUtility.ToJson(playerSave);
        File.WriteAllText(playerPath, playerJson);
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

        MapManager.Instance.DeSerializeCurrentMap(mapSave);

        PlayerSave playerSave = new PlayerSave();
        string playerPath = Application.persistentDataPath + "/Player.json";

        if (File.Exists(playerPath))
        {
            string saveString = File.ReadAllText(playerPath);
            playerSave = JsonUtility.FromJson<PlayerSave>(saveString);
        }

        Player playerComponent = GameManager.Instance.player.GetComponent<Player>();
        playerComponent.Load(playerSave);

        MapManager.Instance.currentMap.UpdatePlayerFieldOfView(playerComponent);
        MapManager.Instance.currentMap.Draw();

        CameraManager.Instance.SetFollowTarget(GameManager.Instance.player.transform);
        CameraManager.Instance.UpdateCamera();
    }
}
