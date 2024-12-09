using System;
using UnityEngine;
using RogueSharp;

[Serializable]
public struct StairsSave
{
    [SerializeField]
    public Stairs.Type type;
    [SerializeField]
    public Vector3 mapPosition;
    [SerializeField]
    public string prefabPath;
}