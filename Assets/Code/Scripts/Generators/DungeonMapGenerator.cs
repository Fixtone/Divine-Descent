using System.Reflection;
using UnityEngine;
using RogueSharp.MapCreation;
using System.Linq;

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

        return _map;
    }

    private void PlaceStairs()
    {
        GameObject stairsPrefab = Resources.Load<GameObject>("Prefabs/Scenery/StairsUp");
        stairsPrefab.transform.localPosition = new Vector3(_map.Rooms.LastOrDefault().Center.X, _map.Rooms.LastOrDefault().Center.Y, 0.0f);

        GameObject stairsInstance = GameObject.Instantiate(stairsPrefab);
        _map.AddStairs(stairsInstance);

        stairsPrefab = Resources.Load<GameObject>("Prefabs/Scenery/StairsDown");
        stairsPrefab.transform.localPosition = new Vector3(_map.Rooms.FirstOrDefault().Center.X, _map.Rooms.FirstOrDefault().Center.Y, 0.0f);

        stairsInstance = GameObject.Instantiate(stairsPrefab);
        _map.AddStairs(stairsInstance);
    }

    private void PlacePlayer()
    {
        GameObject player = GameManager.Instance.player;
        if (player == null)
        {
            GameObject playerPrefab = Resources.Load<GameObject>("Prefabs/Actors/Player");
            player = GameObject.Instantiate(playerPrefab);
        }

        player.transform.localPosition = new Vector3(_map.Rooms[0].Center.X, _map.Rooms[0].Center.Y, 0.0f);

        _map.AddPlayer(player);
    }
}