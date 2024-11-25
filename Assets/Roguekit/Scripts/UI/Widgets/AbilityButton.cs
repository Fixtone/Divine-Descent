using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [SerializeField] private Text labelName;
    [SerializeField] public Image icon;
    public AbilityObject AbilityObject;

    /// <summary>
    /// Populate the UI fields
    /// </summary>
    /// <param name="abilityObject">The AbilityObject to populate with</param>
    public void Populate(AbilityObject abilityObject)
    {
        AbilityObject = abilityObject;
        labelName.text = abilityObject.name;
        icon.sprite = GameManager.Instance.GetSprite(abilityObject.SpriteName);

        icon.color = !GameManager.Instance.ColourTiles ? Color.white : abilityObject.Colour;
    }

    /// <summary>
    /// Click functionality
    /// </summary>
    public void Click()
    {
        AbilityObject.Perform(Player.Instance);
    }

    /// <summary>
    /// Destroy the button
    /// </summary>
    public void Kill()
    {
        Destroy(gameObject);
    }
}
