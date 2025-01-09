using RogueSharp.Random;
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
    }

    private void Start()
    {
        StateManager.Instance.PushState(new MainMenuState());

        // if (PlayerPrefs.GetString("TileSet") != string.Empty)
        // {
        //     TileSet = PlayerPrefs.GetString("TileSet");
        // }

        // bool isNewGame = FileManager.Instance.IsNewGame();
        // if(isNewGame)
        // {
        //     WorldSeed = 1983; //PlayerPrefs.GetInt("Seed");
        //     WorldRandom = new DotNetRandom(WorldSeed);

        //     StateManager.Instance.PushState(new GenerateNewWorldState());
        // }
        // else
        // {
        //     WorldSave worldSave = FileManager.Instance.GetWorldSave();
        //     WorldSeed = worldSave.worldSeed;
        //     WorldRandom = new DotNetRandom(WorldSeed);

        //     StateManager.Instance.PushState(new GoToLevelState{mapIdGoingTo = worldSave.currentMapId, saveCurrentMap = false});
        // }
    }
}