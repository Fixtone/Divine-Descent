using System;
using System.Collections.Generic;
using RogueSharp;
using RogueSharp.MapCreation;
using Unity.VisualScripting;
using UnityEngine;

public class GameMap : Map
{
    protected List<GameObject> stairs = new List<GameObject>();
    protected float fogIntensity = 0.25f;

    public GameMap()
    {
    }

    public virtual void UpdatePlayerFieldOfView(Player player)
    { }

    public virtual void AddStairs(GameObject stairs)
    { }

    public virtual void AddPlayer(GameObject player)
    { }

    public virtual void SetIsWalkable(int x, int y, bool isWalkable)
    { }

    public virtual bool SetActorPosition(Actor actor, int x, int y)
    {
        return false;
    }

    public virtual bool PositionHasAnActor(int x, int y)
    {
        return false;
    }

    public virtual MapSave Serialize()
    {
        return new MapSave();
    }

    public virtual void DeSerialize(MapSave mapSave)
    {
    }

    public virtual void Draw()
    {
    }

    public virtual void SetFogIntensity(float fogIntensity)
    {
        this.fogIntensity = fogIntensity;
    }
}