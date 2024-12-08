using System;
using UnityEngine;
using RogueSharp;

[Serializable]
public struct MonsterSave
{
    [SerializeField]
    public string characterName;
    [SerializeField]
    public Vector3 mapPosition;
    [SerializeField]
    public string prefabPath;
}