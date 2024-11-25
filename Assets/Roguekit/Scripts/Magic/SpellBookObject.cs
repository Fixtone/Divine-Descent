using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Book", menuName = "Magic/Spell Book")]
[System.Serializable]
public class SpellBookObject : ScriptableObject, ISerializationCallbackReceiver
{
    public SpellDatabaseObject Database;
    public SpellBook Book;

    void OnEnable()
    {

    }

    /// <summary>
    /// Populate the spellbook database
    /// </summary>
    public void PopulateDatabase()
    {
        Database = GameManager.Instance.SpellDatabase;
        Book = new SpellBook();
    }

    /// <summary>
    /// Memorise a spell
    /// </summary>
    /// <param name="spell">Spell to memorise</param>
    public void MemoriseSpell(Spell spell)
    {
        if(!Book.IsSpellKnown(spell))
            Book.SpellsMemorised.Add(spell);
    }

    /// <summary>
    /// Memorise a spell
    /// </summary>
    /// <param name="spellObject">SpellObject to memorise</param>
    public void MemoriseSpell(SpellObject spellObject)
    {
        MemoriseSpell(new Spell(spellObject));
    }

    /// <summary>
    /// Forget a spell
    /// </summary>
    /// <param name="spell">Spell to forget</param>
    public void ForgetSpell(Spell spell)
    {
        if (Book.IsSpellKnown(spell))
            Book.SpellsMemorised.Remove(spell);
    }

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {

    }
}

[System.Serializable]
public class SpellBook
{
    public List<Spell> SpellsMemorised = new List<Spell>();

    /// <summary>
    /// Check if a spell is already known
    /// </summary>
    /// <param name="spell"></param>
    /// <returns>If spell is known</returns>
    public bool IsSpellKnown(Spell spell)
    {
        foreach(Spell memmedSpell in SpellsMemorised)
            if (spell.Id == memmedSpell.Id) return true;

        return false;
    }
}

