using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : Entity
{
    public ItemObject ItemObject;

    protected override void Start()
    {
        base.Start();

        if (ItemObject != null)
            Populate(ItemObject);
    }

    /// <summary>
    /// Populates the item from a scriptable ItemObject
    /// </summary>
    /// <param name="obj"></param>
    public void Populate(ItemObject itemObject)
    {
        ItemObject = itemObject;
        Avatar = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Avatar.sprite = GameManager.Instance.GetSprite(itemObject.SpriteName);
        startColour = !GameManager.Instance.ColourTiles ? Color.white : itemObject.Colour;
        transform.name = string.Format("Drop ({0})", ItemObject.name);
    }

    public override EntityClass Classification
    {
        get
        {
            return EntityClass.DROP;
        }
    }

    /// <summary>
    /// Handle the drop's game tick
    /// </summary>
    public override void Tick()
    {
        base.Tick();

        SetShade();
    }

    /// <summary>
    /// Triggers death
    /// </summary>
    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }
}
