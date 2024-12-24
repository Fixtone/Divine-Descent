using System;
using UnityEngine;
using RogueSharp;

[Serializable]
public struct StairsSave
{
    [SerializeField]
    public Entity.Type type;

    [SerializeField]
    public Stairs.Direction Direction;

    [SerializeField]
    public Vector3 mapPosition;

    [SerializeField]
    public string spriteFileName;

    [SerializeField]
    public int goToLevelId;
}