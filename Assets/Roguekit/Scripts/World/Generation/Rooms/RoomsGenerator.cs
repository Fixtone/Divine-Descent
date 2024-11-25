using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a room
/// </summary>
public class Room
{
    public int X1, X2, Y1, Y2;
    public bool Lit = false;

    public Room(int x, int y, int w, int h)
    {
        this.X1 = x;
        this.Y1 = y;
        this.X2 = x + w;
        this.Y2 = y + h;
    }

    public Vector2 Center()
    {
        var center_x = (this.X1 + this.X2) / 2;
        var center_y = (this.Y1 + this.Y2) / 2;
        return new Vector2(center_x, center_y);
    }

    public bool Intersect(Room other)
    {
        // returns true if this rectangle intersects with another one
        return this.X1 <= other.X2 && this.X2 >= other.X1 && this.Y1 <= other.Y2 && this.Y2 >= other.Y1;
    }

    public bool HasCoord(int x, int y)
    {
        return x >= X1 && x <= X2 && y >= Y1 && y <= Y2;
    }
}

/// <summary>
/// A Generator to create rooms and link them with corridoors
/// </summary>
public class RoomsGenerator : Generator
{
    private int roomMaxSize = 10;
    private int roomMinSize = 6;
    private int maxRooms = 30;
    List<Room> rooms = new List<Room>();

    public RoomsGenerator(int width, int height, Entrance entranceUsed, int seed) : base(width, height, entranceUsed, seed)
    {
        InitialiseTiles("Wall","Floor");
        MakeMap();
        SpawnEntities();
    }

    /// <summary>
    /// Initialise the tiles in the array
    /// </summary>
    public override void InitialiseTiles()
    {
        base.InitialiseTiles();
    }

    /// <summary>
    /// Initialise the tiles in the array
    /// </summary>
    /// <param name="graphicBlocked">The graphic for blocked tiles</param>
    /// <param name="graphicNotBlocked">The graphic for not blocked tiles</param>
    public override void InitialiseTiles(string graphicBlocked, string graphicNotBlocked)
    {
        base.InitialiseTiles(graphicBlocked, graphicNotBlocked);
    }

    /// <summary>
    /// Logic to make the map
    /// </summary>
    public override void MakeMap()
    {
        base.MakeMap();

        rooms = new List<Room>();
        int roomNo = 0;

        Room previousRoom = null;

        Room entranceRoom = null;
        Room exitRoom = null;

        for (int i = 0; i < maxRooms; i++)
        {
            int w = Random.Range(roomMinSize, roomMaxSize + 1);
            int h = Random.Range(roomMinSize, roomMaxSize + 1);
            int x = Random.Range(0, width - w - 1);
            int y = Random.Range(0, height - w - 1);

            Room newRoom = new Room(x, y, w, h);

            bool makeRoom = true;

            foreach(Room otherRoom in rooms)
            {
                if (newRoom.Intersect(otherRoom))
                {
                    makeRoom = false;
                    break;
                }
            }

            if(makeRoom)
            {
                CreateRoom(newRoom, false);
                
                if(roomNo == 0)
                {
                    entranceRoom = newRoom;
                }
                else
                {
                    //Connect it to the previous room with a tunnel

                    //Centre coordinates from previous room
                    float prevX = previousRoom.Center().x;
                    float prevY = previousRoom.Center().y;
                    float newX = newRoom.Center().x;
                    float newY = newRoom.Center().y;

                    if (Random.Range(0, 2) == 0)
                    {
                        //First move horizontally, then vertically
                        CreateHorizontalTunnel((int)prevX, (int)newX, (int)prevY);
                        CreateVerticalTunnel((int)prevY, (int)newY, (int)newX);
                    }
                    else
                    {
                        //First move vertically, then horizontally
                        CreateVerticalTunnel((int)prevY, (int)newY, (int)prevX);
                        CreateHorizontalTunnel((int)prevX, (int)newX, (int)newY);
                    }
                }

                previousRoom = newRoom;
                rooms.Add(newRoom);
                roomNo++;
                exitRoom = newRoom; //Will be the last one
            }
        }

        if (EntranceUsed == Entrance.NEW_GAME)
        {
            Player.Instance.SetPosition(entranceRoom.Center().x, entranceRoom.Center().y);
            Player.Instance.StartPos = entranceRoom.Center();
            GameManager.Instance.SpawnPortalAt((int)(exitRoom.Center().x), (int)(exitRoom.Center().y), Entrance.DOWN);
            LightRoom(entranceRoom);
        }
        else if (EntranceUsed == Entrance.DOWN)
        {
            Player.Instance.SetPosition(entranceRoom.Center().x, entranceRoom.Center().y);
            GameManager.Instance.SpawnPortalAt((int)(entranceRoom.Center().x + 1), (int)(entranceRoom.Center().y + 1), Entrance.UP);
            GameManager.Instance.SpawnPortalAt((int)(exitRoom.Center().x), (int)(exitRoom.Center().y), Entrance.DOWN);
            LightRoom(entranceRoom);
        }
        else if (EntranceUsed == Entrance.UP)
        {
            Player.Instance.SetPosition(exitRoom.Center().x, exitRoom.Center().y);
            GameManager.Instance.SpawnPortalAt((int)(exitRoom.Center().x + 1), (int)(exitRoom.Center().y + 1), Entrance.DOWN);
            GameManager.Instance.SpawnPortalAt((int)(entranceRoom.Center().x), (int)(entranceRoom.Center().y), Entrance.UP);
            LightRoom(exitRoom);
        }
    }

    /// <summary>
    /// Creates a room in the map
    /// </summary>
    /// <param name="room">The room to create</param>
    /// <param name="reveal">Whether it should be revealed</param>
    private void CreateRoom(Room room, bool reveal = false)
    {
        for(int x = room.X1 + 1; x < room.X2 && x < width-1; x++)
        {
            for (int y = room.Y1 + 1; y < room.Y2 && y < height-1; y++)
            {
                //Unblock every tile in the room
                Tiles[x, y].Blocked = false;
                Tiles[x, y].BlockedSight = false;
            }
        }

        if(reveal)
        {
            LightRoom(room);
        }
    }

    /// <summary>
    /// Light a room up
    /// </summary>
    /// <param name="room"></param>
    private void LightRoom(Room room)
    {
        for (int x = room.X1; x < room.X2 + 1 && x < width - 1; x++)
        {
            for (int y = room.Y1; y < room.Y2 + 1 && y < height - 1; y++)
            {
                Uncovered[x, y] = true;
            }
        }

        room.Lit = true;
    }

    /// <summary>
    /// Reveals a radius of tiles at a given position
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="radius"></param>
    public override void Reveal(int x, int y, int radius)
    {
        base.Reveal(x, y, radius);

        foreach(Room r in rooms)
        {
            if (!r.Lit && r.HasCoord(x, y))
                LightRoom(r);
        }
    }

    /// <summary>
    /// Creates a horizontal tunnel
    /// </summary>
    /// <param name="x1">First x position</param>
    /// <param name="x2">Second x position</param>
    /// <param name="y">Y position</param>
    private void CreateHorizontalTunnel(int x1, int x2, int y)
    {
        for (int x = Mathf.Min(x1,x2); x < Mathf.Max(x1,x2) + 1; x++)
        {
            Tiles[x, y].Blocked = false;
            Tiles[x, y].BlockedSight = false;
        }
    }

    /// <summary>
    /// Creates a vertical tunnel
    /// </summary>
    /// <param name="y1">First y position</param>
    /// <param name="y2">Second y position</param>
    /// <param name="x">X position</param>
    private void CreateVerticalTunnel(int y1, int y2, int x)
    {
        for (int y = Mathf.Min(y1, y2); y < Mathf.Max(y1, y2) + 1; y++)
        {
            Tiles[x, y].Blocked = false;
            Tiles[x, y].BlockedSight = false;
        }
    }

    /// <summary>
    /// Loops through the rooms and spawns entites in them
    /// </summary>
    private void SpawnEntities()
    {
        for(int i=1; i < rooms.Count; i++)
        {
            Room r = rooms[i];
            SpawnMobsInRoom(r, 3);
            SpawnDropsInRoom(r, 1);
            SpawnSceneryInRoom(r, 3);
        }
    }

    /// <summary>
    /// Spawns a random number of mobs in a room
    /// </summary>
    /// <param name="room">The room to spawn in</param>
    /// <param name="maxNumMobs">The maximum number of mobs</param>
    private void SpawnMobsInRoom(Room room, int maxNumMobs)
    {
        int numMobs = Random.Range(1, maxNumMobs + 1);

        for(int i = 0; i < numMobs; i++)
        {
            int x = Random.Range(room.X1 + 1, room.X2 - 1);
            int y = Random.Range(room.Y1 + 1, room.Y2 - 1);

            GameManager.Instance.SpawnMobAt(x, y);
        }
    }

    /// <summary>
    /// Spawns a random number of drops in a room
    /// </summary>
    /// <param name="room">The room to spawn in</param>
    /// <param name="maxDrops">The maximum number of mobs</param>
    private void SpawnDropsInRoom(Room room, int maxDrops)
    {
        int numDrops = Random.Range(0, maxDrops + 1);

        for (int i = 0; i < numDrops; i++)
        {
            int x = Random.Range(room.X1 + 1, room.X2 - 1);
            int y = Random.Range(room.Y1 + 1, room.Y2 - 1);

            GameManager.Instance.SpawnDropAt(x, y);
        }
    }

    /// <summary>
    /// Spawns a random amount of scenery in a room
    /// </summary>
    /// <param name="room">The room to spawn in</param>
    /// <param name="maxScenery">The maximum amount of scenery</param>
    private void SpawnSceneryInRoom(Room room, int maxScenery)
    {
        int numScenery = Random.Range(1, maxScenery + 1);

        for (int i = 0; i < numScenery; i++)
        {
            int x = Random.Range(room.X1 + 1, room.X2 - 1);
            int y = Random.Range(room.Y1 + 1, room.Y2 - 1);

            GameManager.Instance.SpawnSceneryAt(x, y);
        }
    }
}
