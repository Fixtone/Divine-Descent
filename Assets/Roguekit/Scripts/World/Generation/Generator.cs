using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Entrance
{
    NEW_GAME = 0,
    UP = 1,
    DOWN = 2,
    PORTAL = 3,
}

public class GameTile
{
    public bool Blocked;
    public bool BlockedSight;
    public bool IsExplored = false;
    public string GraphicBlocked = "Wall";
    public string GraphicNotBlocked = "Floor";

    /// <summary>
    /// Gets the tile graphic based on whether it's blocked or not
    /// </summary>
    public string TileGraphic
    {
        get
        {
            return Blocked ? GraphicBlocked : GraphicNotBlocked;
        }
    }

    public GameTile(bool blocked)
    {
        this.Blocked = blocked;
        BlockedSight = true;
    }

    public GameTile(bool blocked, string graphicBlocked, string graphicNotBlocked)
    {
        this.Blocked = blocked;
        BlockedSight = true;
        GraphicBlocked = graphicBlocked;
        GraphicNotBlocked = graphicNotBlocked;
    }

    public GameTile(bool blocked, bool blockedSight)
    {
        this.Blocked = blocked;
        this.BlockedSight = blockedSight;
    }

    public GameTile(bool blocked, bool blockedSight, string graphicBlocked, string graphicNotBlocked)
    {
        this.Blocked = blocked;
        this.BlockedSight = blockedSight;
        GraphicBlocked = graphicBlocked;
        GraphicNotBlocked = graphicNotBlocked;
    }
}


public abstract class Generator
{
    public int width;
    public int height;
    public GameTile[,] Tiles;
    public bool[,] Uncovered;
    public Entrance EntranceUsed;
    public int Seed;

    public Generator(int width, int height, Entrance entranceUsed, int seed)
    {
        this.width = width;
        this.height = height;
        EntranceUsed = entranceUsed;
        Seed = seed;
        Tiles = new GameTile[width, height];
        Uncovered = new bool[width, height];
    }

    /// <summary>
    /// Initialise the tiles in the array
    /// </summary>
    public virtual void InitialiseTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool wall = true;
                Tiles[x, y] = new GameTile(wall);
                Uncovered[x, y] = false;
            }
        }
    }

    /// <summary>
    /// Initialise the tiles in the array
    /// </summary>
    /// <param name="graphicBlocked">The graphic for blocked tiles</param>
    /// <param name="graphicNotBlocked">The graphic for not blocked tiles</param>
    public virtual void InitialiseTiles(string graphicBlocked, string graphicNotBlocked)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool wall = true;
                Tiles[x, y] = new GameTile(wall, graphicBlocked, graphicNotBlocked);
                Uncovered[x, y] = false;
            }
        }
    }

    /// <summary>
    /// Logic to make the map
    /// </summary>
    public virtual void MakeMap()
    {

    }

    /// <summary>
    /// Check if a tile is blocked
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>Is the tile blocked</returns>
    public bool IsBlocked(int x, int y)
    {
        if (Tiles[x, y].Blocked)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Reveals a radius of tiles at a given position
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="radius"></param>
    public virtual void Reveal(int x, int y, int radius)
    {
        int startX = x - radius;
        int startY = y - radius;
        int endX = x + radius;
        int endY = y + radius;
        if (startX < 0) startX = 0;
        if (endX > width) endX = width - 1;
        if (startY < 0) startY = 0;
        if (endY > height) endY = height - 1;

        for (int xx = startX; xx <= endX; xx++)
        {
            for (int yy = startY; yy <= endY; yy++)
            {
                Uncovered[xx, yy] = true;
                Tiles[xx, yy].IsExplored = true;
            }
        }
    }
}
