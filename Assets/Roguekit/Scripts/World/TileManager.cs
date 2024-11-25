using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    #region SINGLETON

    public static TileManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public Tilemap floorMap;
    public Tile emptyTile;
    [SerializeField] private int width;
    [SerializeField] private int height;
    public Generator Map;
    public float LightLevel = 1;
    public Node[,] Grid;

    /// <summary>
    /// Creates a new map
    /// </summary>
    /// <param name="seed">The unique seed</param>
    /// <param name="entrance">The entrance the player came in from</param>
    public void NewMap(int seed, Entrance entrance)
    {
        Map = new RoomsGenerator(width, height, entrance, seed);
        DrawMap();
        CreateAStarGrid();
    }

    /// <summary>
    /// Draw the map from the tiles array
    /// </summary>
    public void DrawMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameTile gameTile = Map.Tiles[x, y];
                Tile tile = Resources.Load(string.Format("Tile Maps/Tiles/{0}/{1}", GameManager.Instance.TileSet, gameTile.TileGraphic)) as Tile;
                Color tileColor = tile.color;
                Color backgroundColour = new Color(tileColor.r, tileColor.g, tileColor.b, 0);
                float shade = GetShade(new Vector3(x, y, 0));
                Color tileShade = Color.Lerp(backgroundColour, tileColor, shade);
                floorMap.SetTile(new Vector3Int(x, y, 0), tile);
                floorMap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
                floorMap.SetColor(new Vector3Int(x, y, 0), tileShade);
            }
        }
    }

    /// <summary>
    /// Gets the shade for a given position
    /// </summary>
    /// <param name="pos"></param>
    /// <returns>Tile shade</returns>
    public float GetShade(Vector3 pos)
    {
        if (!Map.Uncovered[(int)pos.x, (int)pos.y]) return 0;
        return GetShade(Vector3.Distance(Player.Instance.transform.position, new Vector3(pos.x, pos.y, 0)));
    }

    /// <summary>
    /// Gets the shade for a tile based on distance
    /// </summary>
    /// <param name="distance"></param>
    /// <returns>Tile shade</returns>
    public float GetShade(float distance)
    {
        return 1 - ((distance * 0.1f) / LightLevel);
    }

    /// <summary>
    /// Checks if a given tile can be moved to
    /// </summary>
    /// <param name="wx">World X</param>
    /// <param name="wy">World Y</param>
    /// <returns>If tile can be moved to</returns>
    public bool CanMoveTo(int wx, int wy)
    {
        return Map.Tiles[wx, wy].Blocked == false;
    }

    /// <summary>
    /// Gets how far is clear from a certain point and direction
    /// </summary>
    /// <param name="wx">World X</param>
    /// <param name="wy">World Y</param>
    /// <param name="desiredDistance">Distance to check</param>
    /// <param name="direction">Direction to check</param>
    /// <returns>The number of clear tiles</returns>
    public int GetClearDistance(int wx, int wy, int desiredDistance, Direction direction)
    {
        int distance = 0;

        for (int i = 0; i < desiredDistance; i++)
        {
            if (direction == Direction.NORTH)
            {
                if (Map.Tiles[wx, wy + distance].Blocked)
                    break;
            }
            else if (direction == Direction.SOUTH)
            {
                if (Map.Tiles[wx, wy - distance].Blocked)
                    break;
            }
            else if (direction == Direction.EAST)
            {
                if (Map.Tiles[wx + distance, wy].Blocked)
                    break;
            }
            else if (direction == Direction.WEST)
            {
                if (Map.Tiles[wx - distance, wy].Blocked)
                    break;
            }

            distance++;
        }

        return distance;
    }

    #region ASTAR

    public int MaxSize
    {
        get
        {
            return width * height;
        }
    }

    /// <summary>
    /// Creates the A Star grid
    /// </summary>
    private void CreateAStarGrid()
    {
        Grid = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool walkable = Map.Tiles[x, y].Blocked == false;
                Vector3 worldPos = new Vector3(x, y, 0);
                Grid[x, y] = new Node(walkable, worldPos, x, y);
            }
        }
    }

    /// <summary>
    /// Get a list of Nodes that neighbour a given Node
    /// </summary>
    /// <param name="node">Node to check</param>
    /// <returns>List of neighbouring Nodes</returns>
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        int checkX, checkY;
        checkX = node.GridX - 1;
        checkY = node.GridY;
        if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
            neighbours.Add(Grid[checkX, checkY]);

        checkX = node.GridX + 1;
        checkY = node.GridY;
        if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
            neighbours.Add(Grid[checkX, checkY]);

        checkX = node.GridX;
        checkY = node.GridY - 1;
        if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
            neighbours.Add(Grid[checkX, checkY]);

        checkX = node.GridX;
        checkY = node.GridY + 1;
        if (checkX >= 0 && checkX < width && checkY >= 0 && checkY < height)
            neighbours.Add(Grid[checkX, checkY]);

        return neighbours;
    }

    /// <summary>
    /// Get a node from a given world point
    /// </summary>
    /// <param name="worldPosition">World position</param>
    /// <returns>The Node at that position</returns>
    public Node GetNodeFromWorldPoint(Vector3 worldPosition)
    {
        return Grid[(int)worldPosition.x, (int)worldPosition.y];
    }

    #endregion

}
