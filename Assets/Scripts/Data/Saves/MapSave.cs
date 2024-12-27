using System;
using UnityEngine;
using RogueSharp;
using System.Collections.Generic;
using System.ComponentModel;

[Serializable]
public struct MapSave
{
    [SerializeField]
    public int Id;
    [SerializeField]
    public float FogIntensity;
    [SerializeField]
    public MapState MapState;
    [SerializeField]
    public List<MonsterSave> Monsters;
    [SerializeField]
    public List<StairsSave> Stairs;
    [SerializeField]
    public List<RectangleSave> Rooms;
}