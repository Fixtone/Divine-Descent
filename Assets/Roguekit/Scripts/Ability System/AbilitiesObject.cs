using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Abilities Set", menuName = "Abilities/Abilities Set")]
[System.Serializable]
public class AbilitiesObject : ScriptableObject, ISerializationCallbackReceiver
{
    public AbilityDatabaseObject Database;
    public Abilities AbilitiesSet;

    void OnEnable()
    {

    }

    /// <summary>
    /// Populate the abilities database
    /// </summary>
    public void PopulateDatabase()
    {
        Database = GameManager.Instance.AbilityDatabase;
        AbilitiesSet = new Abilities();
    }

    /// <summary>
    /// Learn an ability
    /// </summary>
    /// <param name="abilityObject">An AbilityObject Object to learn</param>
    public void LearnAbility(AbilityObject abilityObject)
    {
        LearnAbility(new Ability(abilityObject));
    }

    /// <summary>
    /// Learn an ability
    /// </summary>
    /// <param name="ability">The Ability Object to learn</param>
    public void LearnAbility(Ability ability)
    {
        if (!AbilitiesSet.IsAbilityKnown(ability))
            AbilitiesSet.AbilitiesKnown.Add(ability);
    }

    /// <summary>
    /// Forget an ability
    /// </summary>
    /// <param name="abilityObject">An AbilityObject Object to forget</param>
    public void ForgetAbility(AbilityObject abilityObject)
    {
        ForgetAbility(new Ability(abilityObject));
    }

    /// <summary>
    /// Forget an ability
    /// </summary>
    /// <param name="ability">An Ability Object to forget</param>
    public void ForgetAbility(Ability ability)
    {
        if (AbilitiesSet.IsAbilityKnown(ability))
            AbilitiesSet.AbilitiesKnown.Remove(ability);
    }

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {

    }
}

[System.Serializable]
public class Abilities
{
    public List<Ability> AbilitiesKnown = new List<Ability>();

    /// <summary>
    /// Check if an Ability is already known
    /// </summary>
    /// <param name="ability"></param>
    /// <returns></returns>
    public bool IsAbilityKnown(Ability ability)
    {
        foreach (Ability abilityKnown in AbilitiesKnown)
            if (ability.Id == abilityKnown.Id) return true;

        return false;
    }
}
