using System.IO;
using RogueSharp;
using RogueSharp.Random;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    #region Singleton
    public static MapManager Instance;
    #endregion

    public GameMap currentMap { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //string mapData = map.ToString();
        //File.WriteAllText(Application.persistentDataPath + "/MapData.txt", mapData);
    }

    public void GenerateNewMap()
    {
        DungeonMapGenerator mapGenerator = new DungeonMapGenerator(100, 50, 50, 12, 8, 0.25f, GameManager.Instance.WorldRandom);
        currentMap = mapGenerator.CreateMap();
    }

    public MapSave SerializeCurrentMap()
    {
        return currentMap.Serialize();
    }

    public void DeSerializeCurrentMap(MapSave mapSave)
    {
        currentMap.DeSerialize(mapSave);
    }
}
