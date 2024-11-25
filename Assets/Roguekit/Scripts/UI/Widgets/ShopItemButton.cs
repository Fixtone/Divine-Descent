using UnityEngine;
using UnityEngine.UI;

public class ShopItemButton : MonoBehaviour
{
    [SerializeField] private Text labelName;
    [SerializeField] private Text labelPrice;
    [SerializeField] public Image icon;
    public ItemObject ItemObject;
    public InventorySlot Slot;
    private Mob shopKeeper;

    /// <summary>
    /// Populate the UI fields
    /// </summary>
    /// <param name="slot">InventorySlot to populate from</param>
    /// <param name="shopKeeper">The shopkeeper Mob</param>
    public void Populate(InventorySlot slot, Mob shopKeeper)
    {
        this.Slot = slot;
        this.shopKeeper = shopKeeper;

        if (slot.Id != -1)
        {
            ItemObject = shopKeeper.Bag.Database.ItemLookup[slot.Id];
            labelPrice.text = slot.Item.Price.ToString();
            labelName.text = ItemObject.name;
            icon.sprite = GameManager.Instance.GetSprite(ItemObject.SpriteName);
            icon.color = !GameManager.Instance.ColourTiles ? Color.white : ItemObject.Colour;
        }
    }

    /// <summary>
    /// Populate the UI fields
    /// </summary>
    /// <param name="itemObject">ItemObject to populate from</param>
    public void Populate(ItemObject itemObject)
    {
        ItemObject = itemObject;
        labelName.text = ItemObject.name;
        icon.sprite = GameManager.Instance.GetSprite(ItemObject.SpriteName);
    }

    /// <summary>
    /// Click functionality
    /// </summary>
    public void Click()
    {
        if (Player.Instance.Gold >= ItemObject.Price)
        {
            Player.Instance.Bag.AddItem(ItemObject, 1);
            shopKeeper.Bag.RemoveItem(ItemObject, 1);
            Player.Instance.Gold -= ItemObject.Price;

            UIManager.Instance.UpdateBag();
            UIManager.Instance.UpdateShop(shopKeeper);
        }
    }

    /// <summary>
    /// Destroy the button
    /// </summary>
    public void Kill()
    {
        Destroy(gameObject);
    }
}
