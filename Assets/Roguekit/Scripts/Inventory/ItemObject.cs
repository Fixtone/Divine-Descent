using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    WEAPON,
    EQUIPMENT,
    POTION,
    FOOD,
    MISC,
    SCROLL,
}


public abstract class ItemObject : ScriptableObject
{
    [HideInInspector] 
    public int Id;
    public ItemType Type;
    public string SpriteName = "None";
    public Color Colour = Color.white;
    public int Price = 1;
    public bool Stacks = false;

    /// <summary>
    /// Creates item
    /// </summary>
    /// <returns></returns>
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

    /// <summary>
    /// Uses the item
    /// </summary>
    /// <param name="user">The being who is using it</param>
    public virtual void Use(Being user)
    {
        GameManager.Instance.DoTick();
    }
}

[System.Serializable]
public class Item
{
    public int Id = -1;
    public string Name;
    public bool Stacks = false;
    public int Price = 0;

    public Item(ItemObject itemObject)
    {
        if (itemObject != null)
        {
            Name = itemObject.name;
            Id = itemObject.Id;
            Stacks = itemObject.Stacks;
            Price = itemObject.Price;
        }
    }

}
