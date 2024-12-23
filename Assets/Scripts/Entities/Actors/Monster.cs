using System;
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

    public MonsterSave Save()
    {
        MonsterSave monsterSave = new MonsterSave();
        monsterSave.characterName = Name;
        monsterSave.mapPosition = transform.localPosition;
        monsterSave.type = type;
        monsterSave.subtype = subType;
        return monsterSave;
    }
}
