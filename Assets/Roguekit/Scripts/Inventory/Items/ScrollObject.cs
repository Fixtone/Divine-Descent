using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scroll Object", menuName = "Inventory/Items/Scroll")]
public class ScrollObject : ItemObject
{
    public SpellObject SpellToTeach;

    private void Awake()
    {
        Type = ItemType.SCROLL;
    }

    /// <summary>
    /// Learn the scroll's spell
    /// </summary>
    /// <param name="user"></param>
    public override void Use(Being user)
    {
        base.Use(user);

        user.SpellBook.MemoriseSpell(new Spell(SpellToTeach));
        user.Bag.RemoveItem(this, 1);

        if (user == Player.Instance)
        {
            UIManager.Instance.UpdateSpellBook();
            UIManager.Instance.UpdateBag();
        }
    }
}
