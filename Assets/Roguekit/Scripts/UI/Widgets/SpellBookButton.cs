using UnityEngine;
using UnityEngine.UI;

public class SpellBookButton : MonoBehaviour
{
    [SerializeField] private Text labelName;
    [SerializeField] private Text labelManaCost;
    [SerializeField] public Image icon;
    public SpellObject SpellObject;

    /// <summary>
    /// Populate the UI fields
    /// </summary>
    /// <param name="spellObject">SpellObject to populate from</param>
    public void Populate(SpellObject spellObject)
    {
        SpellObject = spellObject;
        labelName.text = SpellObject.name;
        labelManaCost.text = SpellObject.ManaCost.ToString();
        icon.sprite = GameManager.Instance.GetSprite(spellObject.SpriteName);
        icon.color = !GameManager.Instance.ColourTiles ? Color.white : SpellObject.Colour;
    }

    /// <summary>
    /// Click functionality
    /// </summary>
    public void Click()
    {
        SpellObject.Cast(Player.Instance);
    }

    /// <summary>
    /// Destroy the button
    /// </summary>
    public void Kill()
    {
        Destroy(gameObject);
    }
}
