using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Stairs : Entity
{
    [SerializeField]
    public enum Direction
    {
        Up,
        Down
    }

    public Direction direction = Direction.Up;
    public int goToLevelId;

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
        stairsSave.direction = direction;
        stairsSave.goToLevelId = goToLevelId;
        stairsSave.textureFileName = spriteRenderer.sprite.texture.name;

        string[] subs = spriteRenderer.sprite.name.Split('_');
        stairsSave.spriteIndex = int.Parse(subs.Last());

        stairsSave.mapPosition = transform.localPosition;

        return stairsSave;
    }

    public static Stairs Load(StairsSave stairsSave)
    {
        string mapObjectPrefabPath = FileManager.Instance.GetMapObjectPrefabPath();
        GameObject stairsPrefab = Resources.Load<GameObject>(mapObjectPrefabPath);
        GameObject stairsInstance = GameObject.Instantiate(stairsPrefab, GameManager.Instance.StairsParent);
       
        Stairs stairsComponent = stairsInstance.GetComponent<Stairs>();

        stairsComponent.type = stairsSave.type;
        stairsComponent.direction = stairsSave.direction;
        stairsComponent.goToLevelId = stairsSave.goToLevelId;

        Sprite[] sprites = Resources.LoadAll<Sprite>(FileManager.TEXTURES_FOLDER_NAME + "_" + stairsSave.textureFileName);
        stairsComponent.spriteRenderer.sprite = sprites[stairsSave.spriteIndex];
        stairsComponent.transform.localPosition = stairsSave.mapPosition;

        return stairsComponent;
    }

    public static Stairs Create(StairsObject stairsObject, Vector3 position = new Vector3())
    {
        string mapObjectPrefabPath = FileManager.Instance.GetMapObjectPrefabPath();
        GameObject stairsPrefab = Resources.Load<GameObject>(mapObjectPrefabPath);

        GameObject stairsInstance = GameObject.Instantiate(stairsPrefab, GameManager.Instance.StairsParent);
        Stairs stairsComponent = stairsInstance.GetComponent<Stairs>();
        stairsComponent.type = stairsObject.type;
        stairsComponent.direction = stairsObject.direction;
        stairsComponent.spriteRenderer.sprite = stairsObject.sprite;
        stairsComponent.goToLevelId = stairsObject.goToLevelId;
        stairsComponent.transform.localPosition = position;

        return stairsComponent;
    }
}
