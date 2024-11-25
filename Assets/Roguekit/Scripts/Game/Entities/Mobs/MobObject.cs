using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Mob", menuName = "Mobs/Mob")]
public class MobObject : ScriptableObject
{
    public string SpriteName = "None";
    public Color Colour = Color.white;
    public bool Aggro = true;
    public float AggroRadius = 4;
    [Range(0, 0.99f)] public float AttackRate = .99f; //The rate in which it should attack in realtime mode
    public ItemObject[] Bag;
    public int Gold; //How much gold to drop on death
    public Stats Stats; 
    public bool Shop = false; //Is the mob a shop?
}
