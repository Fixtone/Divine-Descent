using System;
using UnityEngine;
using RogueSharp;
using System.Collections.Generic;

[Serializable]
public struct WorldSave
{
    [SerializeField]
    public List<MapSave> maps;
}