using System;
using UnityEngine;
using RogueSharp;
using System.Collections.Generic;

[Serializable]
public class WorldSave
{
    [SerializeField]
    public int worldSeed;
    [SerializeField]
    public int currentMapId;
    [SerializeField]
    public List<MapSave> maps;
}