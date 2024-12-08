using System;
using System.Collections.Generic;
using System.IO;
using RogueSharp;
using RogueSharp.Random;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    #region Singleton
    public static WorldManager Instance;
    #endregion

    public GameMap currentMap { get; private set; }

    public Dictionary<int, GameMap> maps = new Dictionary<int, GameMap>();
    private int mapId = 1;

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
        DungeonMapGenerator mapGenerator = new DungeonMapGenerator(mapId, 100, 50, 50, 12, 8, 0.25f, GameManager.Instance.WorldRandom);
        currentMap = mapGenerator.CreateMap();

        maps.Add(mapId, currentMap);
        mapId++;
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

    public void DeSerializeCurrentMap(MapSave mapSave)
    {
        currentMap.LoadMap(mapSave);
    }
}
