using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON

    public static GameManager Instance;



    #endregion

    [SerializeField] private GameObject mobPrefab;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private GameObject sceneryPrefab;
    [SerializeField] private ItemObject[] drops;
    [SerializeField] private MobObject[] mobs;
    [SerializeField] private SceneryObject stairsUp;
    [SerializeField] private SceneryObject stairsDown;
    [SerializeField] private SceneryObject[] scenery;

    public ItemDatabaseObject ItemDatabase;
    public SpellDatabaseObject SpellDatabase;
    public AbilityDatabaseObject AbilityDatabase;

    private Queue<Entity> entitySpawnQueue = new Queue<Entity>();
    private Queue<Entity> entityTickQueue = new Queue<Entity>();

    public List<Entity> Entities = new List<Entity>();
    public float TickSpeed = 0.001f; //The speed ticks should occur in realtime mode
    public bool Realtime = false;
    public int Level = 0;
    public int WorldSeed = 1234; 
    public Mob ShopKeeper = null;
    public string TileSet = "ASCII";
    public bool ColourTiles = true;

    /// <summary>
    /// Have we processed all the entities for this tick?
    /// </summary>
    public bool TickReady
    {
        get
        {
            return entityTickQueue.Count == 0;
        }
    }

    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.GetString("TileSet") != "") TileSet = PlayerPrefs.GetString("TileSet");
    }

    void Start()
    {
        WorldSeed = PlayerPrefs.GetInt("Seed");
        if (TileSet == "ASCII") ColourTiles = true; //Colour the tiles if we're using the ASCII tileset

        ItemDatabase.CreateLookup();
        SpellDatabase.CreateLookup();
        AbilityDatabase.CreateLookup();

        if (Realtime) InvokeRepeating("Tick", TickSpeed, TickSpeed);
        GenerateWorld(WorldSeed + Level, Entrance.NEW_GAME);
    }

    /// <summary>
    /// Generates a world
    /// </summary>
    /// <param name="seed">The unique seed for the world</param>
    /// <param name="entrance">The entrance the player took into the world</param>
    public void GenerateWorld(int seed, Entrance entrance)
    {
        Random.InitState(seed);

        foreach (Entity e in Entities)
            Destroy(e.gameObject);

        entitySpawnQueue = new Queue<Entity>();
        entityTickQueue = new Queue<Entity>();
        Entities = new List<Entity>();

        TileManager.Instance.NewMap(seed, entrance);
    }

    void Update()
    {
        if (entitySpawnQueue.Count > 0)
        {
            Entity entityInstance = entitySpawnQueue.Dequeue();
            entityInstance.SetPositionOnMap();
        }

        while(entityTickQueue.Count > 0)
        {
            Entity entity = entityTickQueue.Dequeue();
            entity.Tick();
        }
    }

    /// <summary>
    /// Force a game tick
    /// </summary>
    public void DoTick()
    {
        if (Realtime) return;
        Tick();
    }

    /// <summary>
    /// Handle a game tick
    /// </summary>
    private void Tick()
    {
        entityTickQueue.Clear();
        
        List<Entity> entitiesCopy = new List<Entity>(Entities);
        foreach (Entity e in entitiesCopy)
        {
            entityTickQueue.Enqueue(e);
        }

        if (Realtime)
        {
            foreach (Entity e in entitiesCopy)
            {
                e.Tick();
            }
        }
    }

    /// <summary>
    /// Move the player up a level and re-generate the world
    /// </summary>
    public void GoToUpperLevel()
    {
        Level++;
        GenerateWorld(WorldSeed + Level, Entrance.UP);
        Camera.main.GetComponent<CameraController>().JumpToPosition();
    }

    /// <summary>
    /// Move the player down a level and re-generate the world
    /// </summary>
    public void GoToLowerLevel()
    {
        Level--;
        GenerateWorld(WorldSeed + Level, Entrance.DOWN);
        Camera.main.GetComponent<CameraController>().JumpToPosition();
    }

    /// <summary>
    /// Move the player to the first level and re-generate the world
    /// </summary>
    public void GoToFirstLevel()
    {
        Level = 0;
        GenerateWorld(WorldSeed + Level, Entrance.NEW_GAME);
        Camera.main.GetComponent<CameraController>().JumpToPosition();
    }

    /// <summary>
    /// Spawns a mob into the world
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SpawnMobAt(int x, int y)
    {
        GameObject mobObject = Instantiate(mobPrefab) as GameObject;
        Mob mobInstance = mobObject.GetComponent<Mob>();
        mobInstance.Populate(mobs[Random.Range(0, mobs.Length)]);
        mobInstance.WorldPosition = new Vector3(x, y, 0);

        entitySpawnQueue.Enqueue(mobInstance);
        Entities.Add(mobInstance);
    }


    /// <summary>
    /// Spawns a drop into the world
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SpawnDropAt(int x, int y)
    {
        GameObject dropOject = Instantiate(dropPrefab) as GameObject;
        Drop dropInstance = dropOject.GetComponent<Drop>();
        dropInstance.Populate(drops[Random.Range(0, drops.Length)]);
        dropInstance.WorldPosition = new Vector3(x, y, 0);

        entitySpawnQueue.Enqueue(dropInstance);
        Entities.Add(dropInstance);
    }

    /// <summary>
    /// Spawns a drop into the world
    /// </summary>
    /// <param name="itemObject"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SpawnDropAt(ItemObject itemObject, int x, int y)
    {
        GameObject dropOject = Instantiate(dropPrefab) as GameObject;
        Drop dropInstance = dropOject.GetComponent<Drop>();
        dropInstance.Populate(itemObject);
        dropInstance.WorldPosition = new Vector3(x, y, 0);

        entitySpawnQueue.Enqueue(dropInstance);
        Entities.Add(dropInstance);
    }

    /// <summary>
    /// Spawns scenery into the world
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SpawnSceneryAt(int x, int y)
    {
        GameObject sceneryObject = Instantiate(sceneryPrefab) as GameObject;
        Scenery sceneryInstance = sceneryObject.GetComponent<Scenery>();
        sceneryInstance.Populate(scenery[Random.Range(0, scenery.Length)]);
        sceneryInstance.WorldPosition = new Vector3(x, y, 0);

        entitySpawnQueue.Enqueue(sceneryInstance);
        Entities.Add(sceneryInstance);
    }

    /// <summary>
    /// Spawns a portal into the world
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="entrance"></param>
    public void SpawnPortalAt(int x, int y, Entrance entrance)
    {
        GameObject sceneryObject = Instantiate(sceneryPrefab) as GameObject;
        Scenery sceneryInstance = sceneryObject.GetComponent<Scenery>();
        if (entrance == Entrance.UP) sceneryInstance.Populate(stairsUp);
        else sceneryInstance.Populate(stairsDown);
        sceneryInstance.WorldPosition = new Vector3(x, y, 0);

        entitySpawnQueue.Enqueue(sceneryInstance);
        Entities.Add(sceneryInstance);
    }

    /// <summary>
    /// Gets a sprite path for loading
    /// </summary>
    /// <param name="spriteName"></param>
    /// <returns></returns>
    public Sprite GetSprite(string spriteName)
    {
        return Resources.Load<Sprite>(string.Format("Sprites/{0}/{1}", TileSet, spriteName));
    }
}
