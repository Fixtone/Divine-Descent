using System.IO;
using RogueSharp;
using RogueSharp.Random;
using UnityEditor.PackageManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform ActorsParent;
    public Transform MonstersParent;
    public Transform StairsParent;

    public string TileSet = "ASCII";
    public int WorldSeed = 1234;
    public IRandom WorldRandom;
    public GameObject player;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.GetString("TileSet") != string.Empty)
        {
            TileSet = PlayerPrefs.GetString("TileSet");
        }
    }

    private void Start()
    {
        WorldSeed = PlayerPrefs.GetInt("Seed");
        WorldRandom = new DotNetRandom(WorldSeed);

        StateManager.Instance.PushState(new GenerateMapState());
        StateManager.Instance.ChangeState(new PlayerTurnState());
    }

    private void MoveMapLevelDown()
    {
    }
}