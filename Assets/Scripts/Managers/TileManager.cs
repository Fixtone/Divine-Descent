using System.Collections;
using System.Collections.Generic;
using RogueSharp;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{

    public static TileManager Instance;

    private void Awake()
    {
       if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public Tilemap floorTileMap;

    /// <summary>
    /// Draw the map from the tiles array
    /// </summary>
    public void SetTile(int x, int y, string tileSet, string tileGraphic, Color color)
    {
        string tileName = string.Format("Tiles/{0}/{1}", tileSet, tileGraphic);

        Tile tile = Resources.Load(tileName) as Tile;
        floorTileMap.SetTile(new Vector3Int(x, y, 0), tile);
        floorTileMap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
        floorTileMap.SetColor(new Vector3Int(x, y, 0), tile.color * color);
    }

    public void Clear()
    {
        floorTileMap.ClearAllTiles();
    }

}
