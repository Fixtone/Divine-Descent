using System.Collections.Generic;
using NoaDebugger;
using RogueSharp;
using UnityEngine;

public class DungeonMap : GameMap
{
    public override void UpdatePlayerFieldOfView(Player player)
    {
        if (DebugCommandRegister.GetCategoryInstance<MapCategoryDebugCommands>(MapDebugCommands.MAP_CATEGORY_NAME).FieldOfView)
        {
            // Compute the field-of-view based on the player's location and awareness
            ComputeFov((int)player.transform.localPosition.x, (int)player.transform.localPosition.y, 8, /*player.Awareness*/ true);
            // Mark all cells in field-of-view as having been explored
            foreach (Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }
        else
        {
            foreach (Cell cell in GetAllCells())
            {
                SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
            }
        }
    }

    public override void AddStairs(Stairs stairs)
    {
        DebugCommandRegister.GetCategoryInstance<MapCategoryDebugCommands>(MapDebugCommands.MAP_CATEGORY_NAME).AddStairsPosition(stairs.transform.localPosition);

        this.stairs.Add(stairs);
    }

    public override void AddPlayer(GameObject player)
    {
        GameManager.Instance.player = player;
        Player playerComponent = player.GetComponent<Player>();

        SetActorPosition(playerComponent, (int)playerComponent.transform.localPosition.x, (int)playerComponent.transform.localPosition.y);

        SchedulingManager.Instance.Add(player.GetComponent<Player>());
    }

    public override void AddMonster(Monster monster)
    {
        monsters.Add(monster);

        SetActorPosition(monster, (int)monster.transform.localPosition.x, (int)monster.transform.localPosition.y);

        SchedulingManager.Instance.Add(monster.GetComponent<Monster>());
    }

    public override void SetIsWalkable(int x, int y, bool isWalkable)
    {
        Cell cell = GetCell(x, y) as Cell;
        SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
    }

    public override bool SetActorPosition(Actor actor, int x, int y)
    {
        if (GetCell(x, y).IsWalkable)                                                       // Only allow actor placement if the cell is walkable
        {
            //PickUpTreasure(actor, x, y);
            // The cell the actor was previously on is now walkable
            SetIsWalkable((int)actor.transform.localPosition.x, (int)actor.transform.localPosition.y, true);

            // Update the actor's position
            actor.transform.localPosition = new Vector3(x, y, 0);

            // The new cell the actor is on is now not walkable
            SetIsWalkable((int)actor.transform.localPosition.x, (int)actor.transform.localPosition.y, false);

            //OpenDoor(actor, x, y);

            if (actor is Player)                                                            // Don't forget to update the field of view if we just repositioned the player
            {
                UpdatePlayerFieldOfView(actor as Player);
            }

            return true;
        }

        return false;
    }

    public override bool PositionHasAnActor(int x, int y)
    {
        if (GameManager.Instance.player.transform.localPosition == new Vector3(x, y, 0))
        {
            return true;
        }

        return monsters.Find(monster => monster.transform.localPosition == new Vector3(x, y, 0)) != null;
    }

    public override Vector3 GetRandomWalkableLocationInRoom(Rectangle room)
    {
        if (DoesRoomHaveWalkableSpace(room))
        {
            for (int i = 0; i < 100; i++)
            {
                int x = GameManager.Instance.WorldRandom.Next(1, room.Width - 2) + room.X;
                int y = GameManager.Instance.WorldRandom.Next(1, room.Height - 2) + room.Y;

                if (IsWalkable(x, y))
                {
                    return new Vector3(x, y, 0);
                }
            }
        }

        return Vector3.zero;
    }

    public override bool DoesRoomHaveWalkableSpace(Rectangle room)
    {
        for (int x = 1; x <= room.Width - 2; x++)
        {
            for (int y = 1; y <= room.Height - 2; y++)
            {
                if (IsWalkable(x + room.X, y + room.Y))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public override MapSave SaveMap()
    {
        MapSave mapSave = new MapSave();
        mapSave.Id = id;
        mapSave.FogIntensity = fogIntensity;
        mapSave.MapState = Save();
        mapSave.Monsters = new List<MonsterSave>();

        mapSave.Stairs = new List<StairsSave>();
        foreach (Stairs stairs in stairs)
        {
            mapSave.Stairs.Add(stairs.Save());
        }

        foreach (Monster monster in monsters)
        {
            mapSave.Monsters.Add(monster.Save());
        }

        return mapSave;
    }

    public override void LoadMap(MapSave mapSave)
    {
        Restore(mapSave.MapState);
    }

    public override void Draw()
    {
        DrawTileMaps();
        DrawStairs();
        DrawMonsters();
    }

    private void DrawTileMaps()
    {
        TileManager tileManager = TileManager.Instance;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                ICell cell = GetCell(x, y);

                string tileGraphic = "Unknown";
                if (PositionHasAnActor(x, y))
                {
                    tileGraphic = "Floor";
                }
                else
                {
                    tileGraphic = cell.IsWalkable ? "Floor" : "Wall";
                }

                Color color = Color.white;
                if (DebugCommandRegister.GetCategoryInstance<MapCategoryDebugCommands>(MapDebugCommands.MAP_CATEGORY_NAME).FieldOfView)
                {
                    if (IsInFov(cell.X, cell.Y))
                    {
                        if (!cell.IsExplored)
                        {
                            color = Color.black;
                        }
                    }
                    else
                    {
                        if (cell.IsExplored)
                        {
                            color.a = fogIntensity;
                        }
                        else
                        {
                            color = Color.black;
                        }
                    }
                }

                tileManager.SetTile(x, y, GameManager.Instance.TileSet, tileGraphic, color);
            }
        }
    }

    private void DrawStairs()
    {
        foreach (Stairs stairs in this.stairs)
        {
            Color color = Color.white;
            if (DebugCommandRegister.GetCategoryInstance<MapCategoryDebugCommands>(MapDebugCommands.MAP_CATEGORY_NAME).FieldOfView)
            {
                Vector2Int stairsMapPosition = new Vector2Int((int)stairs.transform.localPosition.x, (int)stairs.transform.localPosition.y);
                ICell mapCell = GetCell(stairsMapPosition.x, stairsMapPosition.y);

                color = Color.white;
                if (IsInFov(stairsMapPosition.x, stairsMapPosition.y))
                {
                    if (!mapCell.IsExplored)
                    {
                        color = Color.black;
                    }
                }
                else
                {
                    if (mapCell.IsExplored)
                    {
                        color.a = fogIntensity;
                    }
                    else
                    {
                        color = Color.black;
                    }
                }
            }

            stairs.Draw(color);
        }
    }

    private void DrawMonsters()
    {
        foreach (Monster monster in this.monsters)
        {
            Color color = Color.white;
            if (DebugCommandRegister.GetCategoryInstance<MapCategoryDebugCommands>(MapDebugCommands.MAP_CATEGORY_NAME).FieldOfView)
            {
                Vector2Int monsterMapPosition = new Vector2Int((int)monster.transform.localPosition.x, (int)monster.transform.localPosition.y);
                ICell mapCell = GetCell(monsterMapPosition.x, monsterMapPosition.y);

                color = Color.white;
                if (IsInFov(monsterMapPosition.x, monsterMapPosition.y))
                {
                    if (!mapCell.IsExplored)
                    {
                        color = Color.black;
                    }
                }
                else
                {
                    if (mapCell.IsExplored)
                    {
                        color.a = fogIntensity;
                    }
                    else
                    {
                        color = Color.black;
                    }
                }
            }

            monster.Draw(color);
        }
    }
    public override Stairs CanMoveNextLevel()
    {
        GameObject playerGO = GameManager.Instance.player;
        return stairs.Find(item => item.transform.localPosition.Equals(playerGO.transform.localPosition));
    }

}