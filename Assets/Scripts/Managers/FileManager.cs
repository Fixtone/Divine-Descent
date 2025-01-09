using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.WSA;

public class FileManager : MonoBehaviour
{
    public static FileManager Instance;

    public static string SAVE_FOLDER_NAME = "Saves";
    public static string TEXTURES_FOLDER_NAME = "Textures";

    [SerializeField]
    private string persistentDataPath;

    [SerializeField]
    private string playerDirectoryName;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        persistentDataPath = UnityEngine.Application.persistentDataPath + "/" + SAVE_FOLDER_NAME;
    }

    public void SaveGame()
    {
        // string worldPath = Application.persistentDataPath + "/World.json";

        // WorldSave worldSave;
        // if (File.Exists(worldPath))
        // {
        //     worldSave = new WorldSave();
        //     string saveString = File.ReadAllText(worldPath);
        //     worldSave = JsonUtility.FromJson<WorldSave>(saveString);

        //     MapSave currentMapSave = WorldManager.Instance.SaveCurrentMap();

        //     bool existMapSaved = worldSave.maps.Exists(map => map.Id == currentMapSave.Id);
        //     if (existMapSaved)
        //     {
        //         int savedMapSaveIndex = worldSave.maps.FindIndex(map => map.Id == currentMapSave.Id);
        //         worldSave.maps[savedMapSaveIndex] = currentMapSave;
        //     }
        //     else
        //     {
        //         worldSave.maps.Add(currentMapSave);
        //     }
        // }
        // else
        // {
        //     worldSave = WorldManager.Instance.SaveWorld();
        // }

        // string json = JsonUtility.ToJson(worldSave);
        // File.WriteAllText(worldPath, json);

        // string playerPath = Application.persistentDataPath + "/Player.json";
        // PlayerSave playerSave = GameManager.Instance.player.GetComponent<Player>().Save();

        // json = JsonUtility.ToJson(playerSave);
        // File.WriteAllText(playerPath, json);
    }

    public void LoadGame()
    {
        // string mapPath = Application.persistentDataPath + "/World.json";

        // if (!File.Exists(mapPath))
        // {
        //     return;
        // }

        // WorldSave worldSave = new WorldSave();
        // string saveString = File.ReadAllText(mapPath);
        // worldSave = JsonUtility.FromJson<WorldSave>(saveString);
        // WorldManager.Instance.LoadWorld(worldSave);

        // PlayerSave playerSave = new PlayerSave();
        // string playerPath = Application.persistentDataPath + "/Player.json";

        // if (!File.Exists(playerPath))
        // {
        //     return;
        // }

        // saveString = File.ReadAllText(playerPath);
        // playerSave = JsonUtility.FromJson<PlayerSave>(saveString);

        // Player playerComponent = GameManager.Instance.player.GetComponent<Player>();
        // playerComponent.Load(playerSave);

        // WorldManager.Instance.currentMap.UpdatePlayerFieldOfView(playerComponent);
        // WorldManager.Instance.currentMap.Draw();

        // CameraManager.Instance.SetFollowTarget(GameManager.Instance.player.transform);
        // CameraManager.Instance.UpdateCamera();
    }

    public string[] GetGameSavesNames()
    {
        return Directory.GetDirectories(persistentDataPath);
    }

    public void createPlayerDirectoryName()
    {
        string playerSaveFolderNamePath = persistentDataPath + "/" + playerDirectoryName;

        if (Directory.Exists(playerSaveFolderNamePath))
        {
            removeDirectory(playerSaveFolderNamePath);
        }

        Directory.CreateDirectory(playerSaveFolderNamePath);
    }

    public void setPlayerDirectoryName(string directoryName)
    {
        this.playerDirectoryName = directoryName;
    }

    private void removeDirectory(string directoryPath)
    {
        Directory.Delete(directoryPath, true);
    }

    public bool IsNewGame()
    {
        string playerSaveFolderNamePath = persistentDataPath + "/" + playerDirectoryName;
        string worldPath = playerSaveFolderNamePath + "/World.json";

        return !File.Exists(worldPath);
    }

    public WorldSave GetWorldSave()
    {
        string worldPath = persistentDataPath + "/" + playerDirectoryName + "/World.json";

        if (!File.Exists(worldPath))
        {
            CreateWorldSaveFile();
        }

        string saveString = File.ReadAllText(worldPath);
        return JsonUtility.FromJson<WorldSave>(saveString);
    }

    public bool SaveWorldSave(WorldSave worldSave)
    {
        string worldPath = persistentDataPath + "/" + playerDirectoryName + "/World.json";

        if (!File.Exists(worldPath))
        {
            return false;
        }

        string json = JsonUtility.ToJson(worldSave);
        File.WriteAllText(worldPath, json);
        return true;
    }

    public void SavePlayer()
    {
        string playerPath = persistentDataPath + "/" + playerDirectoryName + "/Player.json";
        PlayerSave playerSave = GameManager.Instance.player.GetComponent<Player>().Save();

        string json = JsonUtility.ToJson(playerSave);
        File.WriteAllText(playerPath, json);
    }

    public PlayerSave GetPlayerSaved()
    {
        string mapPath = persistentDataPath + "/Player.json";

        if (!File.Exists(mapPath))
        {
            return null;
        }

        PlayerSave playerSave = new PlayerSave();
        string saveString = File.ReadAllText(mapPath);
        playerSave = JsonUtility.FromJson<PlayerSave>(saveString);

        return playerSave;
    }

    public void SaveCurrentMap()
    {
        WorldSave worldSave = GetWorldSave();

        worldSave.worldSeed = GameManager.Instance.WorldSeed;

        int currentMapId = WorldManager.Instance.currentMap.GetId();
        worldSave.currentMapId = currentMapId;

        bool mapExists = worldSave.maps.Exists(map => map.Id.Equals(currentMapId));
        if (mapExists)
        {
            int savedMapSaveIndex = worldSave.maps.FindIndex(map => map.Id.Equals(currentMapId));
            worldSave.maps[savedMapSaveIndex] = WorldManager.Instance.SaveCurrentMap();
        }
        else
        {
            worldSave.maps.Add(WorldManager.Instance.SaveCurrentMap());
        }

        SaveWorldSave(worldSave);
    }

    public MapSave GetMapSaved(int mapId)
    {
        string mapPath = persistentDataPath + "/" + playerDirectoryName + "/World.json";

        if (!File.Exists(mapPath))
        {
            return null;
        }

        WorldSave worldSave = new WorldSave();
        string saveString = File.ReadAllText(mapPath);
        worldSave = JsonUtility.FromJson<WorldSave>(saveString);

        bool existMapSaved = worldSave.maps.Exists(map => map.Id == mapId);
        if (!existMapSaved)
        {
            return null;
        }

        return worldSave.maps.Find(map => map.Id == mapId);
    }

    public void CreateWorldSaveFile()
    {
        string worldPath = persistentDataPath + "/" + playerDirectoryName + "/World.json";
        if (File.Exists(worldPath))
        {
            return;
        }

        FileStream file = File.Create(worldPath);
        file.Close();
        File.WriteAllText(worldPath, "{}");
    }

    public string GetMapObjectPrefabPath()
    {
        return "Prefabs/Scenery/MapObject";
    }

    public string GetMonsterObjectPrefabPath()
    {
        return "Prefabs/Actors/MonsterObject";
    }
}

