using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory/Items/Weapon")]
public class WeaponObject : EquipmentObject
{
    public ItemObject Ammo;
    public int Range = 0;
    public string Skill;

    public bool IsRanged
    {
        get
        {
            return Range > 0;
        }
    }

    private void Awake()
    {
        Type = ItemType.WEAPON;
        Slot = EquipmentSlot.PRIMARY;
    }
}
