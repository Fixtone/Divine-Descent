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
        GameMap currentMap = MapManager.Instance.currentMap;
        for (int x = 0; x < currentMap.Width; x++)
        {
            for (int y = 0; y < currentMap.Height; y++)
            {
                ICell cell = currentMap.GetCell(x, y);
                
                string tileGraphic = "Unknown";
                if(currentMap.PositionHasAnActor(x, y))
                {
                    tileGraphic = "Floor";
                }
                else
                {
                    tileGraphic = cell.IsWalkable ? "Floor" : "Wall";
                }

                string tileName = string.Format("Tiles/{0}/{1}", GameManager.Instance.TileSet, tileGraphic);
                
                Tile tile = Resources.Load(tileName) as Tile;

                Color color = Color.white;
                if (currentMap.IsInFov(cell.X, cell.Y))
                {
                    if (cell.IsExplored)
                    {
                        color = tile.color;
                    }
                    else
                    {
                        color = Color.black;
                    }
                }
                else
                {
                    if (cell.IsExplored)
                    {
                        color = new Color(tile.color.r, tile.color.g, tile.color.b, 0.35f);
                    }
                    else
                    {
                        color = Color.black;
                    }

                }

                floorTileMap.SetTile(new Vector3Int(x, y, 0), tile);
                floorTileMap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
                floorTileMap.SetColor(new Vector3Int(x, y, 0), color);
            }
        }
    }
}
