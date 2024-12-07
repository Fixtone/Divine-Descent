using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : Entity
{
    protected override void Start()
    {
        base.Start();
    }

    public override void Draw(Color color)
    {
        spriteRenderer.color = color;
    }
}
