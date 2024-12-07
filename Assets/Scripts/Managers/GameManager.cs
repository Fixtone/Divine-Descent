using System.IO;
using RogueSharp;
using RogueSharp.Random;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;
    #endregion

    public string TileSet = "ASCII";
    public int WorldSeed = 1234;
    public static IRandom WorldRandom;

    public GameObject player;

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

        StateManager.Instance.AddState(new GenerateMapState());
    }
}
