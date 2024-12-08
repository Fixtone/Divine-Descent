using System;
using System.Collections.Generic;
using RogueSharp;
using RogueSharp.MapCreation;
using Unity.VisualScripting;
using UnityEngine;

public class GameMap : Map
{
    protected int id;
    protected List<GameObject> monsters = new List<GameObject>();
    protected List<GameObject> stairs = new List<GameObject>();
    protected float fogIntensity;

    public GameMap()
    {
    }

    public virtual void SetId(int id)
    {
        this.id = id;
    }

    public virtual void UpdatePlayerFieldOfView(Player player)
    { }

    public virtual void AddStairs(GameObject stairs)
    { }

    public virtual void AddPlayer(GameObject player)
    { }

    public virtual void AddMonster(GameObject monster)
    {
    }

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

    public virtual bool DoesRoomHaveWalkableSpace(Rectangle room)
    {
        return false;
    }

    public virtual Vector3 GetRandomWalkableLocationInRoom(Rectangle room)
    {
        return Vector3.zero;
    }

    public virtual MapSave SaveMap()
    {
        return new MapSave();
    }

    public virtual void LoadMap(MapSave mapSave)
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