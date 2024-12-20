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

    public override void AddStairs(GameObject stairs)
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

    public override void AddMonster(GameObject monster)
    {
        monsters.Add(monster);

        Monster monsterComponent = monster.GetComponent<Monster>();

        SetActorPosition(monsterComponent, (int)monsterComponent.transform.localPosition.x, (int)monsterComponent.transform.localPosition.y);

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

    public override MapSave Serialize()
    {
        MapSave mapSave = new MapSave();
        mapSave.MapState = Save();

        return mapSave;
    }

    public override void DeSerialize(MapSave mapSave)
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
        foreach (GameObject stairsGO in this.stairs)
        {
            Stairs stairs = stairsGO.GetComponent<Stairs>();
            if (stairs == null)
            {
                continue;
            }

            Color color = Color.white;
            if (DebugCommandRegister.GetCategoryInstance<MapCategoryDebugCommands>(MapDebugCommands.MAP_CATEGORY_NAME).FieldOfView)
            {
                Vector2Int stairsMapPosition = new Vector2Int((int)stairsGO.transform.localPosition.x, (int)stairsGO.transform.localPosition.y);
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
        foreach (GameObject monsterGO in this.monsters)
        {
            Monster monster = monsterGO.GetComponent<Monster>();
            if (stairs == null)
            {
                continue;
            }

            Color color = Color.white;
            if (DebugCommandRegister.GetCategoryInstance<MapCategoryDebugCommands>(MapDebugCommands.MAP_CATEGORY_NAME).FieldOfView)
            {
                Vector2Int monsterMapPosition = new Vector2Int((int)monsterGO.transform.localPosition.x, (int)monsterGO.transform.localPosition.y);
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
}