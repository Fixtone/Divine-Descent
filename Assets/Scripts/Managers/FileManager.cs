using System.IO;
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

        string playerPath = Application.persistentDataPath + "/Player.json";
        PlayerSave playerSave = GameManager.Instance.player.GetComponent<Player>().Save();

        json = JsonUtility.ToJson(playerSave);
        File.WriteAllText(playerPath, json);
    }

    public void LoadGame()
    {
        string mapPath = Application.persistentDataPath + "/World.json";

        if (!File.Exists(mapPath))
        {
            return;
        }

        WorldSave worldSave = new WorldSave();
        string saveString = File.ReadAllText(mapPath);
        worldSave = JsonUtility.FromJson<WorldSave>(saveString);
        WorldManager.Instance.LoadWorld(worldSave);

        PlayerSave playerSave = new PlayerSave();
        string playerPath = Application.persistentDataPath + "/Player.json";

        if (!File.Exists(playerPath))
        {
            return;
        }

        saveString = File.ReadAllText(playerPath);
        playerSave = JsonUtility.FromJson<PlayerSave>(saveString);

        Player playerComponent = GameManager.Instance.player.GetComponent<Player>();
        playerComponent.Load(playerSave);

        WorldManager.Instance.currentMap.UpdatePlayerFieldOfView(playerComponent);
        WorldManager.Instance.currentMap.Draw();

        CameraManager.Instance.SetFollowTarget(GameManager.Instance.player.transform);
        CameraManager.Instance.UpdateCamera();
    }

    public string GetMapObjectPrefabPath()
    {
        return "Prefabs/Scenery/MapObject";
    }
}

