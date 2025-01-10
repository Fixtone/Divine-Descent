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
    }

    public void ClearMap(bool removePlayer = false)
    {
        if(removePlayer)
        {
            GameObject.Destroy(player.gameObject);
        }

        foreach (Transform child in GameManager.Instance.StairsParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in GameManager.Instance.MonstersParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        SchedulingManager.Instance.Clear();
        TileManager.Instance.Clear();
    }
}