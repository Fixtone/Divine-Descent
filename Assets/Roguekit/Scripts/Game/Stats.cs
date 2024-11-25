using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Stat
{
    public float Value;
    public float Max;

    /// <summary>
    /// Calculated percentage
    /// </summary>
    public float Percentage
    {
        get
        {
            return Value / Max * 100f;
        }
    }

    public Stat()
    {
        Value = 100;
        Max = 100;
    }

    public Stat(float value)
    {
        Value = value;
        Max = value;
    }

    public Stat(float value, float max)
    {
        Value = value;
        Max = max;
    }

    /// <summary>
    /// Overloaded - operator
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static Stat operator -(Stat stat, float amount)
    {
        float newValue = stat.Value - amount;
        if (newValue < 0) newValue = 0;
        return new Stat(newValue, stat.Max);
    }

    /// <summary>
    /// Overloaded + operator
    /// </summary>
    /// <param name="stat"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static Stat operator +(Stat stat, float amount)
    {
        float newValue = stat.Value + amount;
        if (newValue > stat.Max) newValue = stat.Max;
        return new Stat(newValue, stat.Max);
    }

    /// <summary>
    /// Sets the value directly
    /// </summary>
    /// <param name="amount"></param>
    public void SetTo(float amount)
    {
        Value += amount;
        if (Value > Max) Value = Max;
        if (Value < 0) Value = 0;
    }

    /// <summary>
    /// Sets the max value
    /// </summary>
    /// <param name="amount"></param>
    public void SetMax(float amount)
    {
        Max = amount;
        Value = Max;
    }

    /// <summary>
    /// Increases the value
    /// </summary>
    /// <param name="amount">Amount to increase by</param>
    public void IncreaseBy(float amount)
    {
        Max += amount;
        Value = Max;
    }

    /// <summary>
    /// Decreases the value
    /// </summary>
    /// <param name="amount">Amount to decrease by</param>
    public void DecreaseBy(float amount)
    {
        Max -= amount;
        if (Value > Max) Value = Max;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

[Serializable]
public enum Attribute
{
    STRENGTH = 0,
    CONSTITUTION = 1,
    INTELLIGENCE = 2,
}


[Serializable]
public class AttributeStat
{
    public Attribute Key;
    public Stat Value;

    public AttributeStat(Attribute att, Stat stat)
    {
        Key = att;
        Value = stat;
    }
}

public class Buff
{
    public Attribute Attribute;
    public float Value;
    public int TicksRemaining;

    public Buff(Attribute att, float value, int length)
    {
        Attribute = att;
        Value = value;
        TicksRemaining = length;
    }
}

[Serializable]
public class Stats
{
    public float BaseHealth;
    public float BaseMaxHealth;
    public float BaseMana;
    public float BaseMaxMana;

    public List<AttributeStat> Attributes = new List<AttributeStat>();
    public Dictionary<string, Buff> Buffs = new Dictionary<string, Buff>();

    /// <summary>
    /// Health value
    /// </summary>
    public float Health
    {
        get
        {
            return BaseHealth;
        }
        set
        {
            BaseHealth = value;
            if (BaseHealth > MaxHealth) BaseHealth = MaxHealth;
            if (BaseHealth < 0) BaseHealth = 0;
        }
    }

    /// <summary>
    /// Max health value including constitution buffs
    /// </summary>
    public float MaxHealth
    {
        get
        {
            return BaseMaxHealth + GetBuffs(Attribute.CONSTITUTION);
        }
        set
        {
            BaseMaxHealth = value;
        }
    }

    /// <summary>
    /// Calculated health percentage
    /// </summary>
    public float HealthPercentage
    {
        get
        {
            return Health / MaxHealth * 100f;
        }
    }

    /// <summary>
    /// Mana value
    /// </summary>
    public float Mana
    {
        get
        {
            return BaseMana;
        }
        set
        {
            BaseMana = value;
            if (BaseMana > MaxMana) BaseMana = MaxMana;
            if (BaseMana < 0) BaseMana = 0;
        }
    }

    /// <summary>
    /// Max mana value including intelligence buffs
    /// </summary>
    public float MaxMana
    {
        get
        {
            return BaseMaxMana + GetBuffs(Attribute.INTELLIGENCE);
        }
        set
        {
            BaseMaxMana = value;
        }
    }

    /// <summary>
    /// Calculated mana percentage
    /// </summary>
    public float ManaPercentage
    {
        get
        {
            return Mana / MaxMana * 100f;
        }
    }

    /// <summary>
    /// Strength value
    /// </summary>
    public float Strength
    {
        get
        {
            return GetAttribute(Attribute.STRENGTH).Value + GetBuffs(Attribute.STRENGTH);
        }
    }

    /// <summary>
    /// Constitution value
    /// </summary>
    public float Constitution
    {
        get
        {
            return GetAttribute(Attribute.CONSTITUTION).Value + GetBuffs(Attribute.CONSTITUTION);
        }
    }

    /// <summary>
    /// Intelligence value
    /// </summary>
    public float Intelligence
    {
        get
        {
            return GetAttribute(Attribute.INTELLIGENCE).Value + GetBuffs(Attribute.INTELLIGENCE);
        }
    }

    /// <summary>
    /// Gets an attribute
    /// </summary>
    /// <param name="att"></param>
    /// <returns></returns>
    public Stat GetAttribute(Attribute att)
    {
        Stat retVal = null;

        foreach(AttributeStat attStat in Attributes)
        {
            if(attStat.Key == att)
            {
                retVal = attStat.Value;
                break;
            }
        }

        return retVal;
    }

    /// <summary>
    /// Get the buffs for an attribute
    /// </summary>
    /// <param name="att"></param>
    /// <returns></returns>
    public float GetBuffs(Attribute att)
    {
        float retVal = 0;

        foreach(KeyValuePair<string,Buff> kvp in Buffs)
        {
            Buff b = kvp.Value;

            if (b.Attribute == att)
                retVal += b.Value;
        }

        return retVal;
    }

    /// <summary>
    /// Adds a buff
    /// </summary>
    /// <param name="name"></param>
    /// <param name="buff"></param>
    public void AddBuff(string name, Buff buff)
    {
        if(Buffs.ContainsKey(name))
        {
            Buffs[name].TicksRemaining = buff.TicksRemaining;
        }
        else
        {
            Buffs.Add(name, buff);
            
            if(buff.Attribute == Attribute.CONSTITUTION)
            {
                Health += buff.Value;
            }
        }
    }

    /// <summary>
    /// Iterates through the buffs, decrements their tick counts and removes them if their count reaches 0
    /// </summary>
    public void ProcessBuffs()
    {
        List<string> buffsToRemove = new List<string>();

        foreach (KeyValuePair<string, Buff> kvp in Buffs)
        {
            Buff b = kvp.Value;
            b.TicksRemaining--;

            if (b.TicksRemaining <= 0)
                buffsToRemove.Add(kvp.Key);
        }

        foreach (string s in buffsToRemove)
            RemoveBuff(s);
    }

    /// <summary>
    /// Removes a buff
    /// </summary>
    /// <param name="name"></param>
    public void RemoveBuff(string name)
    {
        Buffs.Remove(name);
    }

    public Stats()
    {

    }

    /// <summary>
    /// Sets up initial stats
    /// </summary>
    public void SetUpStats()
    {
        BaseMaxHealth = 100;
        BaseHealth = BaseMaxHealth;
        BaseMaxMana = 100;
        BaseMana = BaseMaxMana;

        Attributes = new List<AttributeStat>();

        Attributes.Add(new AttributeStat(Attribute.STRENGTH, new Stat(1)));
        Attributes.Add(new AttributeStat(Attribute.CONSTITUTION, new Stat(1)));
        Attributes.Add(new AttributeStat(Attribute.INTELLIGENCE, new Stat(1)));
    }


}
