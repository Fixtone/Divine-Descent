using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public string Key;
    public float Value;

    public Skill(string name, float value)
    {
        Key = name;
        Value = value;
    }
}

[System.Serializable]
public class SkillSet 
{
    public List<Skill> SkillsKnown = new List<Skill>();
    private const float START_VALUE = 0.1f;
    private const float MIN_INCREMENT = 0.1f;

    /// <summary>
    /// Gets the value of a skill
    /// </summary>
    /// <param name="name">Skill name</param>
    /// <returns>Skill Value</returns>
    public float GetSkillValue(string name)
    {
        if (name == "") return 0;
        return GetSkill(name).Value;
    }

    /// <summary>
    /// Get a Skill object by its name. If the skill is not already known then create it
    /// </summary>
    /// <param name="name">Skill name</param>
    /// <returns>Skill object</returns>
    public Skill GetSkill(string name)
    {
        Skill retVal = null;

        foreach(Skill skill in SkillsKnown)
        {
            if(skill.Key == name)
            {
                retVal = skill;
                break;
            }
        }

        if (retVal == null)
        {
            Skill skill = new Skill(name, START_VALUE);
            SkillsKnown.Add(skill);
            retVal = skill;
        }

        return retVal;
    }

    /// <summary>
    /// Check if a skill is known
    /// </summary>
    /// <param name="name">Skill name</param>
    /// <returns>If skill is known</returns>
    public bool IsSkillKnown(string name)
    {
        bool retVal = false;

        foreach (Skill skill in SkillsKnown)
        {
            if (skill.Key == name)
            {
                retVal = true;
                break;
            }
        }

        return retVal;
    }

    /// <summary>
    /// Add a new skill
    /// </summary>
    /// <param name="name">Skill name</param>
    public void AddSkill(string name)
    {
        AddSkill(name, START_VALUE);
    }

    /// <summary>
    /// Add a new skill with a certain starting value
    /// </summary>
    /// <param name="name">Skill name</param>
    /// <param name="value">Starting value</param>
    public void AddSkill(string name, float value)
    {
        if (!IsSkillKnown(name))
        {
            Skill skill = new Skill(name, value);
            AddSkill(skill);
        }
    }

    /// <summary>
    /// Add a new skill
    /// </summary>
    /// <param name="skill">Skill object</param>
    public void AddSkill(Skill skill)
    {
        if(!SkillsKnown.Contains(skill)) SkillsKnown.Add(skill);
    }

    /// <summary>
    /// Increase a skill by the minimum increment
    /// </summary>
    /// <param name="name">Skill name</param>
    public void IncreaseSkill(string name)
    {
        if (name == "") return;
        IncreaseSkill(name, MIN_INCREMENT);
    }

    /// <summary>
    /// Increase a skill by a certain increment
    /// </summary>
    /// <param name="name">Skill name</param>
    /// <param name="increment">Amount to increase by</param>
    public void IncreaseSkill(string name, float increment)
    {
        if (name == "") return;
        Skill skill = GetSkill(name);
        skill.Value += increment;
    }
}
