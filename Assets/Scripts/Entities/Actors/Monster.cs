using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Actor
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
