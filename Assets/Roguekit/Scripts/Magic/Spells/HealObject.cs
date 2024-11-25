using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Heal Spell", menuName = "Magic/Spells/Heal")]
public class HealObject : SpellObject
{
    private void Awake()
    {
        SpellType = SpellType.HEAL;
    }

    /// <summary>
    /// Cast the spell
    /// </summary>
    /// <param name="caster">The being who casts the spell</param>
    public override void Cast(Being caster)
    {
        if(caster.Stats.Mana >= ManaCost)
        {
            if (HasReagents(caster))
            {
                base.Cast(caster);
                if (Reagents.Length > 0) TakeRegeants(caster);

                caster.Stats.Health += (Value + caster.MagicRating + myCaster.Skills.GetSkillValue(Skill));
                caster.UpdateHealth();

                if (caster == Player.Instance)
                {
                    if (Reagents.Length > 0) UIManager.Instance.UpdateBag();
                }
            }
        }

    }
}
