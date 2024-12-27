using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        monsterSave.type = type;
        monsterSave.subtype = subType;
        monsterSave.textureFileName = spriteRenderer.sprite.texture.name;

        string[] subs = spriteRenderer.sprite.name.Split('_');
        monsterSave.spriteIndex = int.Parse(subs.Last());

        monsterSave.mapPosition = transform.localPosition;
        return monsterSave;
    }

    public static Monster Load(MonsterSave monsterSave)
    {
        string monsterObjectPrefabPath = FileManager.Instance.GetMonsterObjectPrefabPath();
        GameObject monsterPrefab = Resources.Load<GameObject>(monsterObjectPrefabPath);
        GameObject monsterInstance = GameObject.Instantiate(monsterPrefab, GameManager.Instance.MonstersParent);
       
        Monster monsterComponent = monsterInstance.GetComponent<Monster>();

        monsterComponent.type = monsterSave.type;
        monsterComponent.subType = monsterSave.subtype;

        Sprite[] sprites = Resources.LoadAll<Sprite>(FileManager.TEXTURES_FOLDER_NAME + "_" + monsterSave.textureFileName);
        monsterComponent.spriteRenderer.sprite = sprites[monsterSave.spriteIndex];
        monsterComponent.transform.localPosition = monsterSave.mapPosition;

        return monsterComponent;
    }

    public static Monster Create(MonsterObject monsterObject, Vector3 position = new Vector3())
    {
        string monsterObjectPrefabPath = FileManager.Instance.GetMonsterObjectPrefabPath();
        GameObject monsterPrefab = Resources.Load<GameObject>(monsterObjectPrefabPath);

        GameObject monsterInstance = GameObject.Instantiate(monsterPrefab, GameManager.Instance.MonstersParent);
        Monster monsterComponent = monsterInstance.GetComponent<Monster>();
        monsterComponent.type = monsterObject.type;
        monsterComponent.spriteRenderer.sprite = monsterObject.sprite;
        monsterComponent.transform.localPosition = position;

        return monsterComponent;
    }
}
