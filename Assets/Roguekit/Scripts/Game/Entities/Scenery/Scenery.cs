using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneryClass
{
    UNSET = -1,
    INANIMATE = 0,
    STAIRSUP = 1,
    STAIRSDOWN = 2,
}

public class Scenery : Entity
{
    public SceneryObject SceneryObject;

    public override EntityClass Classification
    {
        get
        {
            return EntityClass.SCENERY;
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Populates the scenery from a scriptable SceneryObject
    /// </summary>
    /// <param name="obj"></param>
    public void Populate(SceneryObject obj)
    {
        SceneryObject = obj;
        Avatar = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Avatar.sprite = GameManager.Instance.GetSprite(SceneryObject.SpriteName);

        transform.name = string.Format("Scenery ({0})", SceneryObject.name);
    }

    /// <summary>
    /// Handle the scenery's game tick
    /// </summary>
    public override void Tick()
    {
        base.Tick();

        SetShade();
    }

    /// <summary>
    /// Takes physical damage
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="attacker"></param>
    public override void TakeDamage(float amount, Entity attacker)
    {
        base.TakeDamage(amount, attacker);
        if (Stats.Health == 0) Die();
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
