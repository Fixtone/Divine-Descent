using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Misc Object", menuName = "Inventory/Items/Misc")]
public class MiscObject : ItemObject
{
    private void Awake()
    {
        Type = ItemType.MISC;
    }
}
