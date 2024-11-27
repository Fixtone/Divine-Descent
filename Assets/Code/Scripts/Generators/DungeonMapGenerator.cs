using RogueSharp;
using RogueSharp.MapCreation;

public class DungeonMapGenerator
{
    private readonly int _width;
    private readonly int _height;
    private readonly int _maxRooms;
    private readonly int _roomMaxSize;
    private readonly int _roomMinSize;
    private readonly RogueSharp.Random.IRandom _random;

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
        DungeonMap dungeonMap = DungeonMap.Create(mapCreationStrategy);
        return dungeonMap;
    }
}