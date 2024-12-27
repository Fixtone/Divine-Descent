using System;
using UnityEngine;
using RogueSharp;

[Serializable]
public struct StairsSave
{
    [SerializeField]
    public Entity.Type type;

    [SerializeField]
    public Stairs.Direction direction;

    [SerializeField]
    public Vector3 mapPosition;

    [SerializeField]
    public string textureFileName;

    [SerializeField]
    public int spriteIndex;

    [SerializeField]
    public int goToLevelId;
}