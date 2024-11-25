using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Inventory", menuName ="Inventory/Inventory")]
[System.Serializable]
public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemDatabaseObject Database;
    public Inventory Container;

    void OnEnable()
    {
        
    }

    /// <summary>
    /// Populates the inventory database with slots
    /// </summary>
    public void PopulateDatabase()
    {
        Database = GameManager.Instance.ItemDatabase;
        Container = new Inventory();

        for (int i = 0; i < Container.Slots.Length; i++)
        {
            Container.Slots[i] = new InventorySlot();
        }
    }

    /// <summary>
    /// Adds ItemObjects to inventory
    /// </summary>
    /// <param name="itemObject">ItemObject to add</param>
    /// <param name="amount">How many</param>
    public void AddItem(ItemObject itemObject, int amount)
    {
        AddItem(new Item(itemObject), amount);
    }

    /// <summary>
    /// Adds Items to inventory
    /// </summary>
    /// <param name="itemObject">Item to add</param>
    /// <param name="amount">How many</param>
    public void AddItem(Item item, int amount)
    {
        //Find first empty inventory slot and add item to it
        if(!item.Stacks)
        {
            SetFirstEmptySlot(item, amount);
            return;
        }

        for(int i = 0; i < Container.Slots.Length; i++)
        {
            if(Container.Slots[i].Id == item.Id)
            {
                Container.Slots[i].AddAmount(amount);
                return;
            }
        }

        SetFirstEmptySlot(item, amount);
    }

    /// <summary>
    /// Adds items to the first empty slot
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public InventorySlot SetFirstEmptySlot(Item item, int amount)
    {
        for (int i = 0; i < Container.Slots.Length; i++)
        {
            if(Container.Slots[i].Id <= -1)
            {
                Container.Slots[i].UpdateSlot(item.Id, item, amount);
                return Container.Slots[i];
            }
        }

        return null;
    }

    /// <summary>
    /// Removes items
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="amount"></param>
    public void RemoveItem(InventorySlot slot, int amount)
    {
        for (int i = 0; i < Container.Slots.Length; i++)
        {
            if (Container.Slots[i] == slot)
            {
                Container.Slots[i].RemoveAmount(amount);

                if (Container.Slots[i].Amount <= 0)
                    Container.Slots[i].UpdateSlot(-1, null, 0);

                return;
            }
        }
    }

    /// <summary>
    /// Removes items
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public void RemoveItem(ItemObject item, int amount)
    {
        for (int i = 0; i < Container.Slots.Length; i++)
        {
            if (Container.Slots[i].Id == item.Id)
            {
                Container.Slots[i].RemoveAmount(amount);

                if (Container.Slots[i].Amount <= 0)
                    Container.Slots[i].UpdateSlot(-1, null, 0);

                return;
            }
        }
    }

    /// <summary>
    /// Check if the inventory has an item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool HasItem(ItemObject item)
    {
        return GetNumItems(item) > 0;
    }

    /// <summary>
    /// Get the number of a certain item in the inventory
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int GetNumItems(ItemObject item)
    {
        int retVal = 0;

        for (int i = 0; i < Container.Slots.Length; i++)
        {
            if (Container.Slots[i].Id == item.Id)
            {
                retVal = Container.Slots[i].Amount;
                break;
            }
        }

        return retVal;
    }


    /// <summary>
    /// Drops an item from a given slot
    /// </summary>
    /// <param name="slot">Slot to drop from</param>
    /// <param name="pos">World position to drop to</param>
    public void Drop(InventorySlot slot, Vector3 pos)
    {
        GameObject dropPrefab = Resources.Load("Entities/Drop") as GameObject;
        GameObject dropInstance = GameObject.Instantiate(dropPrefab);
        ItemObject obj = Database.ItemLookup[slot.Id];
        dropInstance.name = string.Format("Drop {0}", obj.name);
    }

    /// <summary>
    /// Switches slots
    /// </summary>
    /// <param name="slot1"></param>
    /// <param name="slot2"></param>
    public void MoveSlot(InventorySlot slot1, InventorySlot slot2)
    {
        InventorySlot temp = new InventorySlot(slot2.Id, slot2.Item, slot2.Amount);
        slot2.UpdateSlot(slot1.Id, slot1.Item, slot1.Amount);
        slot1.UpdateSlot(temp.Id, temp.Item, temp.Amount);
    }    

    public void OnAfterDeserialize()
    {

    }

    public void OnBeforeSerialize()
    {

    }   
}

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[24]; 

    /// <summary>
    /// If inventory is full
    /// </summary>
    public bool IsFull
    {
        get
        {
            bool retVal = true;

            for(int i = 0; i < Slots.Length; i++)
            {
                if(Slots[i].Id == -1)
                {
                    retVal = false;
                    break;
                }
            }

            return retVal;
        }
    }

    /// <summary>
    /// Clear the inventory
    /// </summary>
    public void Clear()
    {
        foreach(InventorySlot slot in Slots)
        {
            slot.Id = -1;
            slot.Amount = 0;
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public int Id = -1;
    public Item Item;
    public int Amount;

    public InventorySlot()
    {
        Id = -1;
        Item = null;
        Amount = 0;
    }

    public InventorySlot(int id, Item item, int amount)
    {
        Id = id;
        Item = item;
        Amount = amount;
    }

    /// <summary>
    /// Updates the item and amount in a slot
    /// </summary>
    /// <param name="id"></param>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public void UpdateSlot(int id, Item item, int amount)
    {
        Id = id;
        Item = item;
        Amount = amount;
    }

    /// <summary>
    /// Increases amount
    /// </summary>
    /// <param name="value"></param>
    public void AddAmount(int value)
    {
        Amount += value;
    }

    /// <summary>
    /// Decreases amount
    /// </summary>
    /// <param name="value"></param>
    public void RemoveAmount(int value)
    {
        Amount -= value;
    }
}