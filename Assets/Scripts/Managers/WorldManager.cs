using UnityEngine;

public class WorldManager : MonoBehaviour
{
    #region Singleton
    public static WorldManager Instance;
    #endregion

    public GameMap currentMap { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void LoadMap(int mapId)
    {
        MapSave mapSave = FileManager.Instance.GetMapSaved(mapId);
        if (mapSave != null)
        {
            PlayerSave playerSave = FileManager.Instance.GetPlayerSaved();

            DungeonMapGenerator mapGenerator = new DungeonMapGenerator(mapSave);
            currentMap = mapGenerator.LoadMap();
        }
        else
        {
            MapObject mapObject = DatabaseManager.Instance.GetMapObjectById(mapId);
            DungeonMapGenerator mapGenerator = new DungeonMapGenerator(mapObject, GameManager.Instance.WorldRandom);
            currentMap = mapGenerator.CreateMap();
        }
    }

    public MapSave SaveCurrentMap()
    {
        return currentMap.SaveMap();
    }
}
