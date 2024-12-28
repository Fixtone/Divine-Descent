using System;
using UnityEngine;
using RogueSharp;

[Serializable]
public class PlayerSave 
{
    [SerializeField]
    public string characterName;
    [SerializeField]
    public Vector3 mapPosition;
}