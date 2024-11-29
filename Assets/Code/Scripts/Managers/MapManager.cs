using System.IO;
using RogueSharp;
using RogueSharp.Random;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    #region Singleton
    public static MapManager Instance;
    #endregion

    public Map map { get; private set; }

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
        DungeonMapGenerator mapGenerator = new DungeonMapGenerator(100, 50, 50, 12, 8, GameManager.WorldRandom);
        map = mapGenerator.CreateMap();
    }
}
