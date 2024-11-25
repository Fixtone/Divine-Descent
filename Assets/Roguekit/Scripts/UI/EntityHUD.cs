using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHUD : MonoBehaviour
{
    [SerializeField] private GameObject damageIndicator;
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Slider sliderMana;

    void Start()
    {
        sliderHealth.gameObject.SetActive(false);
        sliderMana.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Show the damage indicator
    /// </summary>
    /// <param name="amount">Amount of damage</param>
    public void ShowDamage(float amount)
    {
        GameObject indicator = Instantiate(damageIndicator, transform);
        indicator.transform.localPosition = new Vector3(UnityEngine.Random.Range(-.2f, .2f), UnityEngine.Random.Range(-.2f, .2f));
        indicator.transform.GetChild(0).GetComponent<Text>().text = Mathf.FloorToInt(amount).ToString();
        indicator.transform.SetAsLastSibling();
        Destroy(indicator, 0.45f);
    }

    /// <summary>
    /// Sets the value of the health bar
    /// </summary>
    /// <param name="percentage">Percentage</param>
    public void SetHealth(float percentage)
    {
        sliderHealth.value = percentage;
        sliderHealth.gameObject.SetActive(percentage < 100);
    }

    /// <summary>
    /// Sets the value of the mana bar
    /// </summary>
    /// <param name="percentage">Mana</param>
    public void SetMana(float percentage)
    {
        sliderMana.value = percentage;
        sliderMana.gameObject.SetActive(percentage < 100);
    }
}
