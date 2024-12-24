using System;
using UnityEngine;
using RogueSharp;

[Serializable]
public struct MonsterSave
{
    [SerializeField]
    public Entity.Type type;
    public Monster.SubType subtype;
    [SerializeField]
    public string spriteFileName;
    [SerializeField]
    public Vector3 mapPosition;
}