using System;
using System.Collections;
using System.Collections.Generic;
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
        stairsSave.Direction = direction;
        stairsSave.goToLevelId = goToLevelId;
        stairsSave.spriteFileName = spriteRenderer.sprite.name;
        stairsSave.mapPosition = transform.localPosition;

        return stairsSave;
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
