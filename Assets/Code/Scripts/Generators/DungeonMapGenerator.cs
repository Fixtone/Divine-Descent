using System.Reflection;
using UnityEngine;
using RogueSharp.MapCreation;

public class DungeonMapGenerator
{
    private readonly int _width;
    private readonly int _height;
    private readonly int _maxRooms;
    private readonly int _roomMaxSize;
    private readonly int _roomMinSize;
    private readonly RogueSharp.Random.IRandom _random;
    private DungeonMap _map;

    public DungeonMapGenerator(int width, int height, int maxRooms, int roomMaxSize, int roomMinSize, RogueSharp.Random.IRandom random)
    {
        _width = width;
        _height = height;
        _maxRooms = maxRooms;
        _roomMaxSize = roomMaxSize;
        _roomMinSize = roomMinSize;
        _random = random;
    }

    public DungeonMap CreateMap()
    {
        RandomRoomsMapCreationStrategy<DungeonMap> mapCreationStrategy = new RandomRoomsMapCreationStrategy<DungeonMap>(_width, _height, _maxRooms, _roomMaxSize, _roomMinSize, _random);
        _map = mapCreationStrategy.CreateMap();

        PlacePlayer();

        return _map;
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