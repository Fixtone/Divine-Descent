using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Database", menuName = "Magic/Spells/Database")]
public class SpellDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public SpellObject[] Spells;
    public Dictionary<int, SpellObject> SpellLookup = new Dictionary<int, SpellObject>();

    /// <summary>
    /// Gets a spell by name
    /// </summary>
    /// <param name="name">Spell name string</param>
    /// <returns>SpellObject from lookup</returns>
    public SpellObject GetSpellByName(string name)
    {
        SpellObject retVal = null;

        foreach (KeyValuePair<int, SpellObject> kvp in SpellLookup)
        {
            if (kvp.Value.name == name)
            {
                retVal = kvp.Value;
                break;
            }
        }

        return retVal;
    }

    /// <summary>
    /// Creates the spell lookup table
    /// </summary>
    public void CreateLookup()
    {
        SpellLookup = new Dictionary<int, SpellObject>();

        for (int i = 0; i < Spells.Length; i++)
        {
            Spells[i].Id = i;
            SpellLookup.Add(i, Spells[i]);
        }
    }

    public void OnAfterDeserialize()
    {
        CreateLookup();
    }

    public void OnBeforeSerialize()
    {
        SpellLookup = new Dictionary<int, SpellObject>();
    }
}
