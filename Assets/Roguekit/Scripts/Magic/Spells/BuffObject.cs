using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Buff Spell", menuName = "Magic/Spells/Buff")]
public class BuffObject : SpellObject
{
    public Attribute Attribute;
    public int Length= 10;

    private void Awake()
    {
        SpellType = SpellType.BUFF;
    }

    /// <summary>
    /// Cast the spell
    /// </summary>
    /// <param name="caster">The being who casts the spell</param>
    public override void Cast(Being caster)
    {
        if (caster.Stats.Mana >= ManaCost)
        {
            if (HasReagents(caster))
            {
                base.Cast(caster);
                if (Reagents.Length > 0) TakeRegeants(caster);

                Buff buff = new Buff(Attribute, Value, Length);

                if (Focus == Focus.SELF)
                {
                    caster.Stats.AddBuff(name, buff);
                    caster.UpdateStats();
                }

                if (caster == Player.Instance)
                {
                    if (Reagents.Length > 0) UIManager.Instance.UpdateBag();
                }
            }
        }
    }
}
