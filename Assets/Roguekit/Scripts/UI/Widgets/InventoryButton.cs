using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] private Text labelName;
    [SerializeField] private Text labelAmount;
    [SerializeField] public Image icon;
    public ItemObject ItemObject;
    public InventorySlot Slot;

    /// <summary>
    /// Populate the UI fields
    /// </summary>
    /// <param name="slot">InventorySlot to populate from</param>
    public void Populate(InventorySlot slot)
    {
        this.Slot = slot;

        if (slot.Id != -1)
        {
            ItemObject = Player.Instance.Bag.Database.ItemLookup[slot.Id];
            if (slot.Amount > 1) labelAmount.text = slot.Amount.ToString();
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
        icon.color = !GameManager.Instance.ColourTiles ? Color.white : ItemObject.Colour;
    }

    /// <summary>
    /// Click functionality
    /// </summary>
    public void Click()
    {
        if (GameManager.Instance.ShopKeeper != null)
        {
            //Sell
            GameManager.Instance.ShopKeeper.Bag.AddItem(ItemObject, 1);
            Player.Instance.Bag.RemoveItem(ItemObject, 1);
            Player.Instance.Gold += ItemObject.Price;
            UIManager.Instance.UpdateBag();
            UIManager.Instance.UpdateShop(GameManager.Instance.ShopKeeper);
        }
        else
        {
            ItemObject.Use(Player.Instance);
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
