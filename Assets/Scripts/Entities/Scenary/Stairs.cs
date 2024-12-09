using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : Entity
{
    [SerializeField]
    public enum Type
    {
        Up,
        Down
    }

    public Type type = Type.Up;

    protected override void Start()
    {
        base.Start();
    }

    public override void Draw(Color color)
    {
        spriteRenderer.color = color;
    }

    public StairsSave Save()
    {
        StairsSave stairsSave = new StairsSave();
        stairsSave.type = type;
        stairsSave.mapPosition = transform.localPosition;
        stairsSave.prefabPath = PrefabPath;

        return stairsSave;
    }
}
