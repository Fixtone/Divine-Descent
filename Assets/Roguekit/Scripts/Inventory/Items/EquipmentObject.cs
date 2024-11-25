using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public float AttackRating;
    public float DefenceRating;
    public float MagicRating;
    public EquipmentSlot Slot;

    private void Awake()
    {
        Type = ItemType.EQUIPMENT;
    }

    /// <summary>
    /// Equip or unequip the object
    /// </summary>
    /// <param name="user"></param>
    public override void Use(Being user)
    {
        base.Use(user);

        if (IsEquipped())
        {
            user.Unequip(this);
        }
        else
        {
            user.Equip(this);
            user.Bag.RemoveItem(this, 1);
        }

        if (user == Player.Instance)
        {
            UIManager.Instance.UpdateEquipment();
            UIManager.Instance.UpdateBag();
        }
    }

    /// <summary>
    /// Check if the object is currently equipped by the player
    /// </summary>
    /// <returns></returns>
    public bool IsEquipped()
    {
        bool isEquipped = false;

        switch(Slot)
        {
            case EquipmentSlot.HAT:
                isEquipped = Player.Instance.Equipment.Head == this;
                break;
            case EquipmentSlot.BACK:
                isEquipped = Player.Instance.Equipment.Back == this;
                break;
            case EquipmentSlot.FEET:
                isEquipped = Player.Instance.Equipment.Feet == this;
                break;
            case EquipmentSlot.GLOVES:
                isEquipped = Player.Instance.Equipment.Gloves == this;
                break;
            case EquipmentSlot.LEGS:
                isEquipped = Player.Instance.Equipment.Legs == this;
                break;
            case EquipmentSlot.PRIMARY:
                isEquipped = Player.Instance.Equipment.Primary == this;
                break;
            case EquipmentSlot.SECONDARY:
                isEquipped = Player.Instance.Equipment.Secondary == this;
                break;
            case EquipmentSlot.SHIRT:
                isEquipped = Player.Instance.Equipment.Shirt == this;
                break;
            case EquipmentSlot.TORSO:
                isEquipped = Player.Instance.Equipment.Torso == this;
                break;
        }

        return isEquipped;
    }
}
