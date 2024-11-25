using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Database", menuName ="Inventory/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;
    public Dictionary<int, ItemObject> ItemLookup = new Dictionary<int, ItemObject>();

    /// <summary>
    /// Gets an ItemObject by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public ItemObject GetItemByName(string name)
    {
        ItemObject retVal = null;

        foreach(KeyValuePair<int,ItemObject> kvp in ItemLookup)
        {
            if(kvp.Value.name == name)
            {
                retVal = kvp.Value;
                break;
            }
        }

        return retVal;
    }

    /// <summary>
    /// Creates the item lookup table
    /// </summary>
    public void CreateLookup()
    {
        ItemLookup = new Dictionary<int, ItemObject>();
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].Id = i;
            ItemLookup.Add(i, Items[i]);
        }
    }

    public void OnAfterDeserialize()
    {
        CreateLookup();
    }

    public void OnBeforeSerialize()
    {
        ItemLookup = new Dictionary<int, ItemObject>();
    }
}
