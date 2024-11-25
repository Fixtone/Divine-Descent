using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory/Items/Food")]
public class FoodObject : ItemObject
{
    public float RestoreHealthValue;

    private void Awake()
    {
        Type = ItemType.FOOD;
    }

    /// <summary>
    /// Eat the food
    /// </summary>
    /// <param name="user"></param>
    public override void Use(Being user)
    {
        base.Use(user);

        user.Stats.Health += RestoreHealthValue;
        user.HUD.SetHealth(user.Stats.HealthPercentage);
        user.Bag.RemoveItem(this, 1);

        if(user == Player.Instance)
            UIManager.Instance.UpdateBag();
    }
}
