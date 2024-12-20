using System.Reflection;
using UnityEngine;
using RogueSharp.MapCreation;
using System.Linq;
using RogueSharp.DiceNotation;

public class DungeonMapGenerator
{
    private readonly int _width;
    private readonly int _height;
    private readonly int _maxRooms;
    private readonly int _roomMaxSize;
    private readonly int _roomMinSize;
    private readonly float _fogIntensity;
    private readonly RogueSharp.Random.IRandom _random;
    private DungeonMap _map;

    public DungeonMapGenerator(int width, int height, int maxRooms, int roomMaxSize, int roomMinSize, float fogIntensity, RogueSharp.Random.IRandom random)
    {
        _width = width;
        _height = height;
        _maxRooms = maxRooms;
        _roomMaxSize = roomMaxSize;
        _roomMinSize = roomMinSize;
        _fogIntensity = fogIntensity;
        _random = random;
    }

    public DungeonMap CreateMap()
    {
        RandomRoomsMapCreationStrategy<DungeonMap> mapCreationStrategy = new RandomRoomsMapCreationStrategy<DungeonMap>(_width, _height, _maxRooms, _roomMaxSize, _roomMinSize, _random);
        _map = mapCreationStrategy.CreateMap();

        _map.SetFogIntensity(_fogIntensity);

        PlaceStairs();
        PlacePlayer();
        PlaceMonsters();

        return _map;
    }

    private void PlaceStairs()
    {
        GameObject stairsPrefab = Resources.Load<GameObject>("Prefabs/Scenery/StairsDown");
        stairsPrefab.transform.localPosition = new Vector3(_map.Rooms.LastOrDefault().Center.X, _map.Rooms.LastOrDefault().Center.Y, 0.0f);

        GameObject stairsInstance = GameObject.Instantiate(stairsPrefab, GameManager.Instance.StairsParent);

        _map.AddStairs(stairsInstance);

        stairsPrefab = Resources.Load<GameObject>("Prefabs/Scenery/StairsUp");
        stairsPrefab.transform.localPosition = new Vector3(_map.Rooms.FirstOrDefault().Center.X, _map.Rooms.FirstOrDefault().Center.Y, 0.0f);

        stairsInstance = GameObject.Instantiate(stairsPrefab, GameManager.Instance.StairsParent);

        _map.AddStairs(stairsInstance);
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

                        GameObject kobolPrefab = Resources.Load<GameObject>("Prefabs/Actors/Kobol");
                        GameObject kobolGOInstance = GameObject.Instantiate(kobolPrefab, GameManager.Instance.MonstersParent);
                        kobolGOInstance.transform.localPosition = randomRoomLocation;

                        _map.AddMonster(kobolGOInstance);
                    }
                }
            }
        }
    }
}