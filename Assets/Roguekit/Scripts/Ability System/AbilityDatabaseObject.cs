using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Database", menuName = "Abilities/Database")]
public class AbilityDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public AbilityObject[] Abilities;
    public Dictionary<int, AbilityObject> AbilityLookup = new Dictionary<int, AbilityObject>();

    /// <summary>
    /// Get an ability
    /// </summary>
    /// <param name="name">Ability name string</param>
    /// <returns>AbilityObject from lookup</returns>
    public AbilityObject GetAbilityByName(string name)
    {
        AbilityObject retVal = null;

        foreach (KeyValuePair<int, AbilityObject> kvp in AbilityLookup)
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
    /// Creates the ability lookup table
    /// </summary>
    public void CreateLookup()
    {
        AbilityLookup = new Dictionary<int, AbilityObject>();

        for (int i = 0; i < Abilities.Length; i++)
        {
            Abilities[i].Id = i;
            AbilityLookup.Add(i, Abilities[i]);
        }
    }

    public void OnAfterDeserialize()
    {
        CreateLookup();
    }

    public void OnBeforeSerialize()
    {
        AbilityLookup = new Dictionary<int, AbilityObject>();
    }
}
