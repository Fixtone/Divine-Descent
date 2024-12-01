using System;
using UnityEngine;
using RogueSharp;

[Serializable]
public struct PlayerSave 
{
    [SerializeField]
    public string characterName;
    [SerializeField]
    public Vector3 mapPosition;
}