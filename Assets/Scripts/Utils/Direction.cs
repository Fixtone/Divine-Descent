using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    None = 0,
    DownLeft = 1,
    Down = 2,
    DownRight = 3,
    Left = 4,
    Center = 5,
    Right = 6,
    UpLeft = 7,
    Up = 8,
    UpRight = 9
}

public static class DirectionMap
{
    public static readonly Dictionary<Vector2, Direction> Vector2ToDirection;

    static DirectionMap()
    {
        Vector2ToDirection = new Dictionary<Vector2, Direction>
        {
            { new Vector2(0, 0), Direction.Center },
            { new Vector2(0, 1), Direction.Up },
            { new Vector2(0, -1), Direction.Down },
            { new Vector2(-1, 0), Direction.Left },
            { new Vector2(1, 0), Direction.Right },
            { new Vector2(-1, 1), Direction.UpLeft },
            { new Vector2(1, 1), Direction.UpRight },
            { new Vector2(-1, -1), Direction.DownLeft },
            { new Vector2(1, -1), Direction.DownRight }
        };
    }
}
