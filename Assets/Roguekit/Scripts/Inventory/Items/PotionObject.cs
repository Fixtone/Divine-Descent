using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory/Items/Potion")]
public class PotionObject : ItemObject
{
    public float HP = 0;
    public float MP = 0;

    private void Awake()
    {
        Type = ItemType.POTION;
    }

    /// <summary>
    /// Consume the potion
    /// </summary>
    /// <param name="user"></param>
    public override void Use(Being user)
    {
        base.Use(user);

        user.Stats.Health += HP;
        user.Stats.Mana += MP;
        user.UpdateStats();
        user.Bag.RemoveItem(this, 1);

        if (user == Player.Instance)
            UIManager.Instance.UpdateBag();
    }
}
