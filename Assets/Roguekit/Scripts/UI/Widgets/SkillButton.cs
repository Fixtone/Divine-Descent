using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private Text labelName;
    [SerializeField] private Text labelValue;
    public Skill Skill;

    /// <summary>
    /// Populate the UI fields
    /// </summary>
    /// <param name="skill">Skill to populate from</param>
    public void Populate(Skill skill)
    {
        Skill = skill;
        labelName.text = skill.Key;
        labelValue.text = skill.Value.ToString("0.0");
    }

    /// <summary>
    /// Click functionality
    /// </summary>
    public void Click()
    {

    }

    /// <summary>
    /// Destroy the button
    /// </summary>
    public void Kill()
    {
        Destroy(gameObject);
    }
}
