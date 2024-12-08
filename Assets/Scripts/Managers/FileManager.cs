using System;
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

    public void LoadGame()
    {
        MapSave mapSave = new MapSave();
        string mapPath = Application.persistentDataPath + "/Map.json";

        if (File.Exists(mapPath))
        {
            string saveString = File.ReadAllText(mapPath);
            mapSave = JsonUtility.FromJson<MapSave>(saveString);
        }

        WorldManager.Instance.DeSerializeCurrentMap(mapSave);

        PlayerSave playerSave = new PlayerSave();
        string playerPath = Application.persistentDataPath + "/Player.json";

        if (File.Exists(playerPath))
        {
            string saveString = File.ReadAllText(playerPath);
            playerSave = JsonUtility.FromJson<PlayerSave>(saveString);
        }

        Player playerComponent = GameManager.Instance.player.GetComponent<Player>();
        playerComponent.Load(playerSave);

        WorldManager.Instance.currentMap.UpdatePlayerFieldOfView(playerComponent);
        WorldManager.Instance.currentMap.Draw();

        CameraManager.Instance.SetFollowTarget(GameManager.Instance.player.transform);
        CameraManager.Instance.UpdateCamera();
    }

    public void SaveGame()
    {
        string worldPath = Application.persistentDataPath + "/World.json";

        WorldSave worldSave;
        if (File.Exists(worldPath))
        {
            worldSave = new WorldSave();
            string saveString = File.ReadAllText(worldPath);
            worldSave = JsonUtility.FromJson<WorldSave>(saveString);

            MapSave currentMapSave = WorldManager.Instance.SaveCurrentMap();

            bool existMapSaved = worldSave.maps.Exists(map => map.Id == currentMapSave.Id);
            if (existMapSaved)
            {
                int savedMapSaveIndex = worldSave.maps.FindIndex(map => map.Id == currentMapSave.Id);
                worldSave.maps[savedMapSaveIndex] = currentMapSave;
            }
            else
            {
                worldSave.maps.Add(currentMapSave);
            }
        }
        else
        {
            worldSave = WorldManager.Instance.SaveWorld();
        }

        string json = JsonUtility.ToJson(worldSave);
        File.WriteAllText(worldPath, json);
    }
}
