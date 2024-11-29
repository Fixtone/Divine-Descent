using System.Collections;
using System.Collections.Generic;
using RogueSharp;
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

    public Tilemap floorTileMap;

    /// <summary>
    /// Draw the map from the tiles array
    /// </summary>
    public void Draw()
    {
        Map currentMap = MapManager.Instance.map;
        for (int x = 0; x < currentMap.Width; x++)
        {
            for (int y = 0; y < currentMap.Height; y++)
            {
                ICell cell = currentMap.GetCell(x,y);
                string tileGraphic = cell.IsWalkable ? "Floor" : "Wall";
                string tileName = string.Format("Tiles/{0}/{1}", GameManager.Instance.TileSet,  tileGraphic);
                Tile tile = Resources.Load(tileName) as Tile;

                floorTileMap.SetTile(new Vector3Int(x, y, 0), tile);
                floorTileMap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None); 
                floorTileMap.SetColor(new Vector3Int(x, y, 0), tile.color);
            }
        }
    }
}
