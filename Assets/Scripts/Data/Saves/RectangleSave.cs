using System;
using UnityEngine;
using RogueSharp;

[Serializable]
public struct RectangleSave 
{
    [SerializeField]
    public int X;
    [SerializeField]
    public int Y;

    [SerializeField]
    public int Width;

    [SerializeField]
    public int Height;
}
