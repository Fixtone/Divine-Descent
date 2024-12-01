using System;
using System.Collections.Generic;
using RogueSharp;
using RogueSharp.MapCreation;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonMap : GameMap
{
    public override void UpdatePlayerFieldOfView(Player player)
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

    public override void AddPlayer(GameObject player)
    {
        GameManager.Instance.player = player;
        Player playerComponent = player.GetComponent<Player>();

        SetActorPosition(playerComponent, (int)playerComponent.transform.localPosition.x, (int)playerComponent.transform.localPosition.y);

        SchedulingManager.Instance.Add(player.GetComponent<Player>());
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
        return GameManager.Instance.player.transform.localPosition == new Vector3(x,y,0);
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
}