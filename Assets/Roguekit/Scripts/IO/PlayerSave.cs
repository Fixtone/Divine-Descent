using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Encapsulates player character details to save
/// </summary>
public class PlayerSave 
{
    public string CharacterName;
    public Vector3 WorldPosition;
    public Stats Stats;
    public InventoryObject Bag;
    public EquipmentSave Equipment;
    public SpellBookObject SpellBook;
    public AbilitiesObject Abilities;
    public SkillSet Skills;
    public int Gold;
}
