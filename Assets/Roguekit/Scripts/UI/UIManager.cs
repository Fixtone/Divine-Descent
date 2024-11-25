using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region SINGLETON

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject panelBag;
    [SerializeField] private GameObject panelEquipment;
    [SerializeField] private GameObject panelSpellBook;
    [SerializeField] private GameObject panelShop;
    [SerializeField] private GameObject panelMovement;
    [SerializeField] private GameObject panelAbilities;
    [SerializeField] private GameObject panelSkills;
    [SerializeField] private GameObject panelDead;
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private Transform bagContent;
    [SerializeField] private GameObject widgetItemButton;
    [SerializeField] private Transform equipmentContent;
    [SerializeField] private Transform spellBookContent;
    [SerializeField] private GameObject widgetSpellBookButton;
    [SerializeField] private Transform shopContent;
    [SerializeField] private GameObject widgetShopButton;
    [SerializeField] private Transform abilitiesContent;
    [SerializeField] private GameObject widgetAbilitiesButton;
    [SerializeField] private Transform skillsContent;
    [SerializeField] private GameObject widgetSkillsButton;
    [SerializeField] private Text labelGold;

    public bool Hovering = false;

    void Start()
    {
        HideAllPanels();
        ShowPanelMovement();
        ShowPanelBag();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) TogglePanelBag();
        if (Input.GetKeyDown(KeyCode.M)) TogglePanelSpellBook();
        if (Input.GetKeyDown(KeyCode.E)) TogglePanelEquipment();
        if (Input.GetKeyDown(KeyCode.P)) TogglePanelAbilities();
        if (Input.GetKeyDown(KeyCode.C)) TogglePanelSkills();
        if (Input.GetKeyDown(KeyCode.Escape)) TogglePanelMenu();
        if (Input.GetKeyDown(KeyCode.F12)) ToggleUI();
    }

    /// <summary>
    /// Hide all UI Window panels
    /// </summary>
    public void HideAllPanels()
    {
        panelBag.SetActive(false);
        panelEquipment.SetActive(false);
        panelSpellBook.SetActive(false);
        panelShop.SetActive(false);
        panelAbilities.SetActive(false);
        panelSkills.SetActive(false);
        panelDead.SetActive(false);
        panelMenu.SetActive(false);

        Hovering = false;
    }

    /// <summary>
    /// Show the mobile movement window if we're on mobile or WebGL
    /// </summary>
    private void ShowPanelMovement()
    {
#if UNITY_ANDROID
       if(!Application.isEditor) panelMovement.SetActive(true);
#endif
#if UNITY_IPHONE
       if(!Application.isEditor) panelMovement.SetActive(true);
#endif
#if UNITY_WEBGL
       if(!Application.isEditor) panelMovement.SetActive(true);
#endif
    }

    #region BAG

    /// <summary>
    /// Show the window panel for bag
    /// </summary>
    public void ShowPanelBag()
    {
        panelBag.SetActive(true);
        UpdateBag();
        panelBag.transform.SetAsLastSibling();
    }

    /// <summary>
    /// Hide the window panel for bag
    /// </summary>
    public void HidePanelBag()
    {
        panelBag.SetActive(false);
    }

    /// <summary>
    /// Toggle the window panel for bag
    /// </summary>
    public void TogglePanelBag()
    {
        if (Player.Instance.Dead) return;

        if (panelBag.activeSelf)
            HidePanelBag();
        else
            ShowPanelBag();
    }

    /// <summary>
    /// Update the contents of the player's bag
    /// </summary>
    public void UpdateBag()
    {
        for (int i = 0; i < bagContent.childCount; i++)
        {
            bagContent.GetChild(i).GetComponent<InventoryButton>().Kill();
        }

        for (int i = 0; i < Player.Instance.Bag.Container.Slots.Length; i++)
        {  
            InventorySlot slot = Player.Instance.Bag.Container.Slots[i];

            if (slot.Id != -1)
            {
                GameObject widgetInstance = GameObject.Instantiate(widgetItemButton, bagContent);
                widgetInstance.GetComponent<InventoryButton>().Populate(slot);
            }
        }

        UpdateGold();
    }

    /// <summary>
    /// Update the player's gold display
    /// </summary>
    public void UpdateGold()
    {
        labelGold.text = string.Format("Gold: {0}", Player.Instance.Gold);
    }

    #endregion

    #region EQUIPMENT

    /// <summary>
    /// Show the window panel for equipment
    /// </summary>
    public void ShowPanelEquipment()
    {
        panelEquipment.SetActive(true);
        UpdateEquipment();
        panelEquipment.transform.SetAsLastSibling();
    }

    /// <summary>
    /// Hide the window panel for equipment
    /// </summary>
    public void HidePanelEquipment()
    {
        panelEquipment.SetActive(false);
    }

    /// <summary>
    /// Toggle the window panel for equipment
    /// </summary>
    public void TogglePanelEquipment()
    {
        if (Player.Instance.Dead) return;

        if (panelEquipment.activeSelf)
            HidePanelEquipment();
        else
            ShowPanelEquipment();
    }

    /// <summary>
    /// Update the player's equipment
    /// </summary>
    public void UpdateEquipment()
    {
        for (int i = 0; i < equipmentContent.childCount; i++)
            equipmentContent.GetChild(i).GetComponent<InventoryButton>().Kill();

        if (Player.Instance.Equipment.Head != null)         AddEquipmentWidget(Player.Instance.Equipment.Head);
        if (Player.Instance.Equipment.Torso != null)        AddEquipmentWidget(Player.Instance.Equipment.Torso);
        if (Player.Instance.Equipment.Legs != null)         AddEquipmentWidget(Player.Instance.Equipment.Legs);
        if (Player.Instance.Equipment.Gloves != null)       AddEquipmentWidget(Player.Instance.Equipment.Gloves);
        if (Player.Instance.Equipment.Feet != null)         AddEquipmentWidget(Player.Instance.Equipment.Feet);
        if (Player.Instance.Equipment.Shirt != null)        AddEquipmentWidget(Player.Instance.Equipment.Shirt);
        if (Player.Instance.Equipment.Primary != null)      AddEquipmentWidget(Player.Instance.Equipment.Primary);
        if (Player.Instance.Equipment.Secondary != null)    AddEquipmentWidget(Player.Instance.Equipment.Secondary);
    }

    /// <summary>
    /// Adds a new equipment widget to the equipment window
    /// </summary>
    /// <param name="equipmentObject">The EquipmentObject to populate from</param>
    private void AddEquipmentWidget(EquipmentObject equipmentObject)
    {
        GameObject widgetInstance = GameObject.Instantiate(widgetItemButton, equipmentContent);
        widgetInstance.GetComponent<InventoryButton>().Populate(equipmentObject);
    }

    #endregion

    #region SPELL BOOK

    /// <summary>
    /// Show the window panel for spell book
    /// </summary>
    public void ShowPanelSpellBook()
    {
        panelSpellBook.SetActive(true);
        UpdateSpellBook();
        panelSpellBook.transform.SetAsLastSibling();
    }

    /// <summary>
    /// Hide the window panel for spell book
    /// </summary>
    public void HidePanelSpellBook()
    {
        panelSpellBook.SetActive(false);
    }

    /// <summary>
    /// Toggle the window panel for spell book
    /// </summary>
    public void TogglePanelSpellBook()
    {
        if (Player.Instance.Dead) return;

        if (panelSpellBook.activeSelf)
            HidePanelSpellBook();
        else
            ShowPanelSpellBook();
    }

    /// <summary>
    /// Update the player's spell book with memorised spells
    /// </summary>
    public void UpdateSpellBook()
    {
        for (int i = 0; i < spellBookContent.childCount; i++)
            spellBookContent.GetChild(i).GetComponent<SpellBookButton>().Kill();

        foreach(Spell spell in Player.Instance.SpellBook.Book.SpellsMemorised)
        {
            SpellObject spellObject = Player.Instance.SpellBook.Database.SpellLookup[spell.Id];
            AddSpellBookWidget(spellObject);
        }
    }

    /// <summary>
    /// Adds a new spell widget to the equipment window
    /// </summary>
    /// <param name="spellObject">The SpellObject to populate from</param>
    private void AddSpellBookWidget(SpellObject spellObject)
    {
        GameObject widgetInstance = GameObject.Instantiate(widgetSpellBookButton, spellBookContent);
        widgetInstance.GetComponent<SpellBookButton>().Populate(spellObject);
    }

    #endregion

    #region ABILITIES

    /// <summary>
    /// Show the window panel for abilities
    /// </summary>
    public void ShowPanelAbilities()
    {
        panelAbilities.SetActive(true);
        UpdateAbilities();
        panelAbilities.transform.SetAsLastSibling();
    }

    /// <summary>
    /// Hide the window panel for abilities
    /// </summary>
    public void HidePanelAbilities()
    {
        panelAbilities.SetActive(false);
    }

    /// <summary>
    /// Toggle the window panel for abilities
    /// </summary>
    public void TogglePanelAbilities()
    {
        if (Player.Instance.Dead) return;

        if (panelAbilities.activeSelf)
            HidePanelAbilities();
        else
            ShowPanelAbilities();
    }

    /// <summary>
    /// Update the player's abilities from abilities known
    /// </summary>
    public void UpdateAbilities()
    {
        for (int i = 0; i < abilitiesContent.childCount; i++)
            abilitiesContent.GetChild(i).GetComponent<AbilityButton>().Kill();

        foreach (Ability ability in Player.Instance.Abilities.AbilitiesSet.AbilitiesKnown)
        {
            AbilityObject abilityObject = Player.Instance.Abilities.Database.AbilityLookup[ability.Id];
            AddAbilitiesWidget(abilityObject);
        }
    }

    /// <summary>
    /// Adds a new ability widget to the equipment window
    /// </summary>
    /// <param name="abilityObject">AbilityObject to populate from</param>
    private void AddAbilitiesWidget(AbilityObject abilityObject)
    {
        GameObject widgetInstance = GameObject.Instantiate(widgetAbilitiesButton, abilitiesContent);
        widgetInstance.GetComponent<AbilityButton>().Populate(abilityObject);
    }


    #endregion

    #region SKILLS

    /// <summary>
    /// Show the window panel for skills
    /// </summary>
    public void ShowPanelSkills()
    {
        panelSkills.SetActive(true);
        UpdateSkills();
        panelSkills.transform.SetAsLastSibling();
    }

    /// <summary>
    /// Hide the window panel for skills
    /// </summary>
    public void HidePanelSkills()
    {
        panelSkills.SetActive(false);
    }

    /// <summary>
    /// Toggle the window panel for skills
    /// </summary>
    public void TogglePanelSkills()
    {
        if (Player.Instance.Dead) return;

        if (panelSkills.activeSelf)
            HidePanelSkills();
        else
            ShowPanelSkills();
    }

    /// <summary>
    /// Update the player's skill list
    /// </summary>
    public void UpdateSkills()
    {
        for (int i = 0; i < skillsContent.childCount; i++)
            skillsContent.GetChild(i).GetComponent<SkillButton>().Kill();

        foreach (Skill skill in Player.Instance.Skills.SkillsKnown)
        {
            AddSkillsWidget(skill);
        }
    }

    /// <summary>
    /// Adds a skill widget to the skill list
    /// </summary>
    /// <param name="skill">The Skill to populate from</param>
    private void AddSkillsWidget(Skill skill)
    {
        GameObject widgetInstance = GameObject.Instantiate(widgetSkillsButton, skillsContent);
        widgetInstance.GetComponent<SkillButton>().Populate(skill);
    }

    #endregion

    #region SHOP

    /// <summary>
    /// Show the window panel for shop
    /// </summary>
    public void ShowPanelShop(Mob shopKeeper)
    {
        panelShop.SetActive(true);
        UpdateShop(shopKeeper);
        panelShop.transform.SetAsLastSibling();
    }

    /// <summary>
    /// Hide the window panel for skills
    /// </summary>
    public void HidePanelShop()
    {
        panelShop.SetActive(false);
        GameManager.Instance.ShopKeeper = null;
    }

    /// <summary>
    /// Updates the shop window with items from a shopkeeper
    /// </summary>
    /// <param name="shopKeeper">Shopkeeper Mob to populate from</param>
    public void UpdateShop(Mob shopKeeper)
    {
        for (int i = 0; i < shopContent.childCount; i++)
        {
            shopContent.GetChild(i).GetComponent<ShopItemButton>().Kill();
        }

        for (int i = 0; i < shopKeeper.Bag.Container.Slots.Length; i++)
        {
            InventorySlot slot = shopKeeper.Bag.Container.Slots[i];

            if (slot.Id != -1)
            {
                GameObject widgetInstance = GameObject.Instantiate(widgetShopButton, shopContent);
                widgetInstance.GetComponent<ShopItemButton>().Populate(slot, shopKeeper);
            }
        }
    }
    #endregion

    #region MENUS

    /// <summary>
    /// Shows the death window
    /// </summary>
    public void ShowPanelDead()
    {
        HideAllPanels();
        panelDead.SetActive(true);
        panelMovement.SetActive(false);
    }

    /// <summary>
    /// Shows the menu window
    /// </summary>
    public void ShowPanelMenu()
    {
        HideAllPanels();
        panelMenu.SetActive(true);
    }

    /// <summary>
    /// Toggles the menu window
    /// </summary>
    public void TogglePanelMenu()
    {
        if (Player.Instance.Dead) return;

        if (panelMenu.activeSelf)
            HideAllPanels();
        else
            ShowPanelMenu();
    }


    #endregion

    #region ACTIONS

    /// <summary>
    /// Toggles the UI visibility
    /// </summary>
    public void ToggleUI()
    {
        canvas.enabled = !canvas.enabled;
    }

    /// <summary>
    /// Respawns the player
    /// </summary>
    public void Respawn()
    {
        Player.Instance.ClearData();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game Scene");
    }

    /// <summary>
    /// Saves the player character
    /// </summary>
    public void Save()
    {
        Player.Instance.Save();
        HideAllPanels();
    }

    /// <summary>
    /// Loads a previous saved character
    /// </summary>
    public void Load()
    {
        Player.Instance.Load();
        HideAllPanels();
    }

    /// <summary>
    /// Quits the game to main menu
    /// </summary>
    public void QuitGame()
    {
        Player.Instance.ClearData();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    #endregion

}
