using System.IO;
using RogueSharp;
using RogueSharp.Random;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    #endregion

    public string TileSet = "ASCII";
    public int WorldSeed = 1234;
    public IRandom WorldRandom;
    public Map map { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;

        if (PlayerPrefs.GetString("TileSet") != string.Empty)
        {
            TileSet = PlayerPrefs.GetString("TileSet");
        }
    }

    private void Start()
    {
        WorldSeed = PlayerPrefs.GetInt("Seed");
        WorldRandom = new DotNetRandom(WorldSeed);

        DungeonMapGenerator mapGenerator = new DungeonMapGenerator(100, 50, 50, 12, 8, WorldRandom);
        map = mapGenerator.CreateMap();

        //string mapData = map.ToString();
        //File.WriteAllText(Application.persistentDataPath + "/MapData.txt", mapData);
    }
}
