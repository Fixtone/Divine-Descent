using UnityEngine;
using RogueSharp.MapCreation;
using System.Linq;
using RogueSharp.DiceNotation;

public class DungeonMapGenerator
{
    private readonly int _id;
    private readonly int _width;
    private readonly int _height;
    private readonly int _maxRooms;
    private readonly int _roomMaxSize;
    private readonly int _roomMinSize;
    private readonly float _fogIntensity;
    private readonly MapObject _mapObject;
    private readonly RogueSharp.Random.IRandom _random;
    private readonly MapSave _mapSave;
    private DungeonMap _map;


    public DungeonMapGenerator(MapObject mapObject, RogueSharp.Random.IRandom random)
    {
        _id = mapObject.id;
        _width = mapObject.width;
        _height = mapObject.height;
        _maxRooms = mapObject.maxRooms;
        _roomMaxSize = mapObject.roomMaxSize;
        _roomMinSize = mapObject.roomMinSize;
        _fogIntensity = mapObject.fogIntensity;
        _mapObject = mapObject;
        _random = random;
    }

    public DungeonMapGenerator(MapSave mapSave)
    {
        _mapSave = mapSave;
    }

    public DungeonMap CreateMap()
    {
        RandomRoomsMapCreationStrategy<DungeonMap> mapCreationStrategy = new RandomRoomsMapCreationStrategy<DungeonMap>(_width, _height, _maxRooms, _roomMaxSize, _roomMinSize, _random);
        _map = mapCreationStrategy.CreateMap();

        _map.SetId(_id);
        _map.SetFogIntensity(_fogIntensity);

        PlaceStairs();
        PlacePlayer();
        PlaceMonsters();

        return _map;
    }

    public DungeonMap LoadMap()
    {
        BorderOnlyMapCreationStrategy<DungeonMap> mapCreationStrategy = new BorderOnlyMapCreationStrategy<DungeonMap>(_mapSave.MapState.Width, _mapSave.MapState.Height);
        _map = mapCreationStrategy.CreateMap();
        _map.LoadMap(_mapSave);

        _map.SetId(_mapSave.Id);
        _map.SetFogIntensity(_mapSave.FogIntensity);

        return _map;
    }

    private void PlaceStairs()
    {
        string mapObjectPrefabPath = FileManager.Instance.GetMapObjectPrefabPath();
        GameObject stairsPrefab = Resources.Load<GameObject>(mapObjectPrefabPath);

        foreach (StairsObject stairsObject in _mapObject.stairsObjects)
        {
            Vector3 position = Vector3.zero;
            switch (stairsObject.direction)
            {
                case Stairs.Direction.Up:
                    {
                        position = new Vector3(_map.Rooms.FirstOrDefault().Center.X, _map.Rooms.FirstOrDefault().Center.Y, 0.0f);
                        break;
                    }
                case Stairs.Direction.Down:
                    {
                        position = new Vector3(_map.Rooms.LastOrDefault().Center.X, _map.Rooms.LastOrDefault().Center.Y, 0.0f);
                        break;
                    }
            }

            Stairs stairsInstance = Stairs.Create(stairsObject, position);
            _map.AddStairs(stairsInstance);
        }
    }

    private void PlacePlayer()
    {
        GameObject player = GameManager.Instance.player;
        if (player == null)
        {
            GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Actors/Player");
            player = GameObject.Instantiate(playerPrefab, GameManager.Instance.ActorsParent);
        }

        player.transform.localPosition = new Vector3(_map.Rooms[0].Center.X, _map.Rooms[0].Center.Y, 0.0f);

        CameraManager.Instance.SetFollowTarget(player.transform);

        _map.AddPlayer(player);
    }

    private void PlaceMonsters()
    {
        foreach (var room in _map.Rooms)
        {
            if (Dice.Roll("1D10") < 7)                                                  // Each room has a 60% chance of having monsters
            {
                var numberOfMonsters = Dice.Roll("1D4");                                // Generate between 1 and 4 monsters

                for (int i = 1; i < numberOfMonsters; i++)                              // Not starting at zero. Player is in room zero.
                {
                    Vector3 randomRoomLocation = _map.GetRandomWalkableLocationInRoom(room);// Find a random walkable location in the room to place the monster                       

                    if (randomRoomLocation != Vector3.zero)                               // It's possible that the room doesn't have space to place a monster
                    {                                                                     // In that case skip creating the monster                           
                                                                                          // var monster = Kobold.Create(1, game);                          // Temporarily hard code this monster to be created at level 1


                        Monster monster = Monster.Create(_mapObject.monsterObjects.FirstOrDefault(), randomRoomLocation);
                        _map.AddMonster(monster);
                    }
                }
            }
        }
    }
}