using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RogueSharp;
using RogueSharp.Random;
using UnityEditor;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    #region Singleton
    public static WorldManager Instance;
    #endregion

    public GameMap currentMap { get; private set; }

    public Dictionary<int, GameMap> maps = new Dictionary<int, GameMap>();

    [SerializeField]
    private int currentMapId = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void GenerateNewMap()
    {
        currentMapId++;

        MapObject mapObject = DatabaseManager.Instance.GetMapObjectById(currentMapId);

        if (mapObject == null)
        {
            return;
        }

        switch (mapObject.gameMapType)
        {
            case GameMapTypes.DungeonMap:
                {
                    DungeonMapGenerator mapGenerator = new DungeonMapGenerator(mapObject, GameManager.Instance.WorldRandom);
                    currentMap = mapGenerator.CreateMap();
                    break;
                }
        }

        maps.Add(currentMapId, currentMap);
    }

    public WorldSave SaveWorld()
    {
        WorldSave worldSave = new WorldSave();
        worldSave.maps = new List<MapSave>();

        foreach (KeyValuePair<int, GameMap> gameMap in maps)
        {
            worldSave.maps.Add(gameMap.Value.SaveMap());
        }

        return worldSave;
    }

    public MapSave SaveCurrentMap()
    {
        return currentMap.SaveMap();
    }

    public void LoadWorld(WorldSave worldSave)
    {
        foreach (MapSave mapSave in worldSave.maps)
        {
            GenerateNewMap();
            currentMap.LoadMap(mapSave);
        }
    }
}
