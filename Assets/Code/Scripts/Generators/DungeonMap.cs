using System;
using System.Collections.Generic;
using RogueSharp;
using RogueSharp.MapCreation;

public class DungeonMap : Map
{
    public List<Rectangle> Rooms;

    public DungeonMap()
    {
        Rooms = new List<Rectangle>();
    }

    public static DungeonMap Create(RandomRoomsMapCreationStrategy<DungeonMap> mapCreationStrategy)
    {
        if (mapCreationStrategy == null)
        {
            throw new ArgumentNullException("mapCreationStrategy", "Map creation strategy cannot be null");
        }

        DungeonMap dungeonMap = mapCreationStrategy.CreateMap();
        dungeonMap.Rooms = mapCreationStrategy.Rooms;

        return dungeonMap;
    }

}