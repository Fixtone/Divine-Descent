using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlot
{
    HAT,
    BACK,
    TORSO,
    LEGS,
    FEET,
    GLOVES,
    SHIRT,
    PRIMARY,
    SECONDARY,
}

/// <summary>
/// A being's equipment
/// </summary>
[System.Serializable]
public class Equipment
{
    public EquipmentObject Head;
    public EquipmentObject Back;
    public EquipmentObject Torso;
    public EquipmentObject Legs;
    public EquipmentObject Feet;
    public EquipmentObject Gloves;
    public EquipmentObject Primary;
    public EquipmentObject Secondary;
    public EquipmentObject Shirt;
}

/// <summary>
/// Encapsulates a save object to store equipment
/// </summary>
[System.Serializable]
public class EquipmentSave
{
    public Item Head;
    public Item Back;
    public Item Torso;
    public Item Legs;
    public Item Feet;
    public Item Gloves;
    public Item Primary;
    public Item Secondary;
    public Item Shirt;
}
