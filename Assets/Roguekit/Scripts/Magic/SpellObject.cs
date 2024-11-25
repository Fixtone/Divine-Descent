using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
    HEAL,
    NUKE,
    BUFF,
}

public enum Focus
{
    SELF,
    PROJECTILE,
    AOE,
    TARGET,
}

public abstract class SpellObject : ScriptableObject
{
    [HideInInspector] public int Id;
    public SpellType SpellType;
    public Focus Focus;
    public string SpriteName = "None";
    public Color Colour = Color.white;
    [TextArea(2, 5)] public string Description;
    public float Value = 1;
    public float ManaCost = 1;
    public int Range = 3;
    public string Skill;
    protected Being myCaster;
    public ItemObject[] Reagents;

    /// <summary>
    /// Cast the spell
    /// </summary>
    /// <param name="caster">The being who casts the spell</param>
    public virtual void Cast(Being caster)
    {
        myCaster = caster;
        caster.Stats.BaseMana -= ManaCost;
        caster.UpdateMana();
        caster.Skills.IncreaseSkill(Skill);
        if (caster == Player.Instance) UIManager.Instance.UpdateSkills();

        GameManager.Instance.DoTick();
        AudioManager.Instance.PlaySpell();
    }


    /// <summary>
    /// When the spell hits another Being
    /// </summary>
    /// <param name="beingHit">The Being hit</param>
    public virtual void Hit(Being beingHit)
    {
  
    }

    /// <summary>
    /// Checks if the caster has the required reagents for the spell
    /// </summary>
    /// <param name="caster"></param>
    /// <returns></returns>
    protected virtual bool HasReagents(Being caster)
    {
        if (Reagents.Length == 0) return true;

        bool retVal = true;

        for (int i = 0; i < Reagents.Length; i++)
        {
            if (!caster.Bag.HasItem(Reagents[i]))
                retVal = false;
        }

        return retVal;
    }

    /// <summary>
    /// Removes the required reagents from the caster's bag
    /// </summary>
    /// <param name="caster"></param>
    protected virtual void TakeRegeants(Being caster)
    {
        for (int i = 0; i < Reagents.Length; i++)
        {
            caster.Bag.RemoveItem(Reagents[i], 1);
        }
    }
}

[System.Serializable]
public class Spell
{
    public int Id = -1;
    public string Name;
    public float Value = 0;
    public float ManaCost = 0;
    public int Range = 0;
    public SpellType Type;
    public Focus Focus;

    public Spell(SpellObject spellObject)
    {
        if (spellObject != null)
        {
            Name = spellObject.name;
            Id = spellObject.Id;
            Value = spellObject.Value;
            ManaCost = spellObject.ManaCost;
            Type = spellObject.SpellType;
            Focus = spellObject.Focus;
            Range = spellObject.Range;
        }
    }
}
