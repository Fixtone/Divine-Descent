using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    ABILITY,
    PASSIVE,
    COMBAT,
}

public abstract class AbilityObject : ScriptableObject
{
    [HideInInspector] public int Id;
    public AbilityType AbilityType;
    public string SpriteName = "None";
    public Color Colour = Color.white;
    [TextArea(2, 5)] public string Description;
    protected Being myPerformer;
    public string Skill;
    public float Value = 0;
    public ItemObject[] Reagents;

    /// <summary>
    /// Perform the ability
    /// </summary>
    /// <param name="performer"></param>
    public virtual void Perform(Being performer)
    {
        myPerformer = performer;
        GameManager.Instance.DoTick();
        myPerformer.Skills.IncreaseSkill(Skill);

        if (performer == Player.Instance)
            UIManager.Instance.UpdateSkills();
    }

    /// <summary>
    /// Called when the ability used hits another Being
    /// </summary>
    /// <param name="beingHit">The Being hit</param>
    public virtual void Hit(Being beingHit)
    {
        
    }

    /// <summary>
    /// Checks if the performer has the required reagents for the ability
    /// </summary>
    /// <param name="performer"></param>
    /// <returns></returns>
    protected virtual bool HasReagents(Being performer)
    {
        if (Reagents.Length == 0) return true;

        bool retVal = true;

        for(int i = 0; i < Reagents.Length; i++)
        {
            if (!performer.Bag.HasItem(Reagents[i]))
                retVal = false;
        }

        return retVal;
    }

    /// <summary>
    /// Removes the required reagents from the performer's bag
    /// </summary>
    /// <param name="performer"></param>
    protected virtual void TakeRegeants(Being performer)
    {
        for (int i = 0; i < Reagents.Length; i++)
        {
            performer.Bag.RemoveItem(Reagents[i], 1);
        }
    }
}


[System.Serializable]
public class Ability
{
    public int Id = -1;
    public string Name;
    public AbilityType AbilityType;

    public Ability(AbilityObject skillObject)
    {
        if (skillObject != null)
        {
            Name = skillObject.name;
            Id = skillObject.Id;
            AbilityType = skillObject.AbilityType;
        }
    }
}

