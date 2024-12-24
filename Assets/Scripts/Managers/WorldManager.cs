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
        // currentMapId++;

        // MapObject mapObject = DatabaseManager.Instance.GetMapObjectById(currentMapId);

        // if (mapObject == null)
        // {
        //     return;
        // }

        // switch (mapObject.gameMapType)
        // {
        //     case GameMapTypes.DungeonMap:
        //         {
        //             DungeonMapGenerator mapGenerator = new DungeonMapGenerator(mapObject, GameManager.Instance.WorldRandom);
        //             currentMap = mapGenerator.CreateMap();
        //             break;
        //         }
        // }

        //maps.Add(currentMapId, currentMap);
    }

    public void LoadMap(int mapId)
    {
        MapSave? mapSave = FileManager.Instance.GetMapSaved(mapId);
        if (mapSave != null)
        {
            DungeonMapGenerator mapGenerator = new DungeonMapGenerator(mapSave.Value);
            currentMap = mapGenerator.LoadMap();
        }
        else
        {
            MapObject mapObject = DatabaseManager.Instance.GetMapObjectById(mapId);
            DungeonMapGenerator mapGenerator = new DungeonMapGenerator(mapObject, GameManager.Instance.WorldRandom);
            currentMap = mapGenerator.CreateMap();
        }
    }

    public WorldSave SaveWorld()
    {
        WorldSave worldSave = new WorldSave();
        worldSave.maps = new List<MapSave>();

        // foreach (KeyValuePair<int, GameMap> gameMap in maps)
        // {
        //     worldSave.maps.Add(gameMap.Value.SaveMap());
        // }

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
