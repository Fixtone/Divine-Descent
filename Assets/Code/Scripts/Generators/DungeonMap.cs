using System;
using System.Collections.Generic;
using RogueSharp;
using RogueSharp.MapCreation;
using Unity.VisualScripting;
using UnityEngine;

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

    public void UpdatePlayerFieldOfView(Player player)
    {
        // Compute the field-of-view based on the player's location and awareness
        ComputeFov((int)player.transform.localPosition.x, (int)player.transform.localPosition.y, 10, /*player.Awareness*/ true);
        // Mark all cells in field-of-view as having been explored
        foreach (Cell cell in GetAllCells())
        {
            if (IsInFov(cell.X, cell.Y))
            {
                SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
            }
        }
    }

    public void AddPlayer(GameObject player)
    {
        GameManager.Instance.player = player;
        Player playerComponent = player.GetComponent<Player>();

        SetActorPosition(playerComponent, (int)playerComponent.transform.localPosition.x, (int)playerComponent.transform.localPosition.y);

        SchedulingManager.instance.Add(player.GetComponent<Player>());
    }

    public void SetIsWalkable(int x, int y, bool isWalkable)
    {
        Cell cell = GetCell(x, y) as Cell;
        SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
    }

    public bool SetActorPosition(Actor actor, int x, int y)
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
}