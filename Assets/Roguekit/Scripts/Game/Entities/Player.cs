using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction
{
    NONE = -1,
    NORTH = 0,
    EAST = 1,
    SOUTH = 2,
    WEST = 3,
    UP = 4,
    DOWN = 5,
    NORTHEAST = 6,
    SOUTHEAST = 7,
    SOUTHWEST = 8,
    NORTHWEST = 9,

}

public class Player : Being
{
    #region SINGLETON

    public static Player Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public string CharacterName;
    private DateTime lastMove = DateTime.Now;
    private Camera cam;
    public Vector2 StartPos;

    protected override void Start()
    {
        base.Start();

        CharacterName = PlayerPrefs.GetString("CharacterName");

        cam = Camera.main;

        Stats.SetUpStats();
        SetUpPlayer();

        Avatar.sprite = GameManager.Instance.GetSprite("Player");

        if (GameManager.Instance.Realtime) InvokeRepeating("Tick", GameManager.Instance.TickSpeed, GameManager.Instance.TickSpeed);

    }

    /// <summary>
    /// Sets up the player with some initial abilities and items
    /// </summary>
    private void SetUpPlayer()
    {
        Abilities.LearnAbility(Abilities.Database.GetAbilityByName("Melee Attack"));
        Abilities.LearnAbility(Abilities.Database.GetAbilityByName("Ranged Attack"));
        Abilities.LearnAbility(Abilities.Database.GetAbilityByName("Rest"));
        Abilities.LearnAbility(Abilities.Database.GetAbilityByName("Meditate"));
        Abilities.LearnAbility(Abilities.Database.GetAbilityByName("Bind Wound"));

        Bag.AddItem(Bag.Database.GetItemByName("Short Sword"), 1);
        Bag.AddItem(Bag.Database.GetItemByName("Shield Round"), 1);
    }

    /// <summary>
    /// Clear the bag, spells and abilities data
    /// </summary>
    public void ClearData()
    {
        Bag.Container.Clear();
        SpellBook.Book.SpellsMemorised = new List<Spell>();
        Abilities.AbilitiesSet.AbilitiesKnown = new List<Ability>();
    }

    protected override void Update()
    {
        if (Dead) return;

        base.Update();

        HandleControls();
        HandleMouseAndTouchControls();        
    }

    /// <summary>
    /// Sets the player's position in the scene and updates the WorldPosition
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
        WorldPosition = transform.position;
    }

    /// <summary>
    /// Handles axis controls
    /// </summary>
    private void HandleControls()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            MoveInDirection(Direction.NORTHEAST);
        }
        if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            MoveInDirection(Direction.SOUTHEAST);
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            MoveInDirection(Direction.SOUTHWEST);
        }
        if (Input.GetAxisRaw("Horizontal") < 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            MoveInDirection(Direction.NORTHWEST);
        }
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            MoveInDirection(Direction.NORTH);
        }
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            MoveInDirection(Direction.SOUTH);
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            MoveInDirection(Direction.EAST);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            MoveInDirection(Direction.WEST);
        }
    }

    /// <summary>
    /// Handles mouse and touch controls
    /// </summary>
    private void HandleMouseAndTouchControls()
    {
        if (EventSystem.current.IsPointerOverGameObject() || UIManager.Instance.Hovering) return;

        if(Input.GetMouseButton(0))
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            float epsilon = 2f;

            if (mousePos.x > transform.position.x + epsilon) MoveInDirection(Direction.EAST);
            if (mousePos.x < transform.position.x - epsilon) MoveInDirection(Direction.WEST);
            if (mousePos.y > transform.position.y + epsilon) MoveInDirection(Direction.NORTH);
            if (mousePos.y < transform.position.y - epsilon) MoveInDirection(Direction.SOUTH);
        }
    }

    /// <summary>
    /// Moves the entity
    /// </summary>
    /// <param name="direction">The direction to move</param>
    public override void MoveInDirection(Direction direction)
    {
        if (!GameManager.Instance.TickReady) return;

        TimeSpan ts = DateTime.Now - lastMove;
        if (ts.TotalMilliseconds < 250) return;
        lastMove = DateTime.Now;

        UIManager.Instance.HidePanelShop();
        GameManager.Instance.ShopKeeper = null;

        Vector3 checkPos = transform.position;

        switch (direction)
        {
            case Direction.NORTH:
                checkPos = checkPos + new Vector3(0, 1, 0);
                break;
            case Direction.SOUTH:
                checkPos = checkPos + new Vector3(0, -1, 0);
                break;
            case Direction.EAST:
                checkPos = checkPos + new Vector3(1, 0, 0);
                break;
            case Direction.WEST:
                checkPos = checkPos + new Vector3(-1, 0, 0);
                break;
            case Direction.NORTHEAST:
                checkPos = checkPos + new Vector3(1, 1, 0);
                break;
            case Direction.SOUTHEAST:
                checkPos = checkPos + new Vector3(1, -1, 0);
                break;
            case Direction.SOUTHWEST:
                checkPos = checkPos + new Vector3(-1, -1, 0);
                break;
            case Direction.NORTHWEST:
                checkPos = checkPos + new Vector3(-1, 1, 0);
                break;
        }

        FacingDirection = direction;

        Entity checkedEntity = CheckForEntities(direction, 0.25f, checkPos);
        Target = checkedEntity;

        bool moveInterrupted = false;

        if (checkedEntity != null)
            moveInterrupted = HandleEntity(checkedEntity);
        else
            SetAttacking(false);

        if (!TileManager.Instance.Map.IsBlocked((int)checkPos.x, (int)checkPos.y) && !moveInterrupted)
        {
            transform.position = checkPos;
            WorldPosition = transform.position;
            TileManager.Instance.Map.Reveal((int)checkPos.x, (int)checkPos.y, 1);
            TileManager.Instance.DrawMap();
        }

        //For turn-based
        if (!GameManager.Instance.Realtime)
        {
            Tick();
            GameManager.Instance.DoTick();
        }

        AudioManager.Instance.PlayMove();
    }

    /// <summary>
    /// Handles the entity that has been interacted with
    /// </summary>
    /// <param name="entity">The entity to interact with</param>
    /// <returns>Whether this entity interrupts movement</returns>
    private bool HandleEntity(Entity entity)
    {
        bool moveInterrupted = false;

        if (entity.Classification == EntityClass.DROP)
        {
            Drop drop = (Drop)entity;
            Item item = new Item(drop.ItemObject);
            Bag.AddItem(item, 1);
            drop.Die();
            UIManager.Instance.UpdateBag();
        }
        else if (entity.Classification == EntityClass.BEING)
        {
            moveInterrupted = true;

            if (entity.GetType() == typeof(Mob))
            {
                Mob mob = (Mob)entity;

                if (mob.Aggro || mob.Attacking)
                {
                    AttackEntity(entity);
                }
                else
                {
                    if(mob.Shop)
                    {
                        GameManager.Instance.ShopKeeper = mob;
                        UIManager.Instance.ShowPanelShop(mob);
                    }
                }
            }
        }
        else if (entity.Classification == EntityClass.SCENERY)
        {
            Scenery scenery = (Scenery)entity;

            if(scenery.SceneryObject.SceneryClassification == SceneryClass.STAIRSUP)
                GameManager.Instance.GoToUpperLevel();
            else if (scenery.SceneryObject.SceneryClassification == SceneryClass.STAIRSDOWN)
                GameManager.Instance.GoToLowerLevel();

            moveInterrupted = true;
        }
        else
        {
            moveInterrupted = true;
        }

        return moveInterrupted;
    }

    /// <summary>
    /// Handle the player's game tick
    /// </summary>
    public override void Tick()
    {
        base.Tick();
        if (Attacking && Vector3.Distance(WorldPosition, Target.WorldPosition) == AttackRange && AttackThisTick && !Target.Dead)
        {
            AttackTarget();
            DoAnim(AnimMove.ATTACK);
            AudioManager.Instance.PlayAttack();
        }
    }

    /// <summary>
    /// Equips an EnquipmentObject
    /// </summary>
    /// <param name="equipmentObject"></param>
    public override void Equip(EquipmentObject equipmentObject)
    {
        base.Equip(equipmentObject);

        UIManager.Instance.UpdateEquipment();
    }

    /// <summary>
    /// Takes off an EquipmentObject if it's being worn
    /// </summary>
    /// <param name="equipmentObject"></param>
    public override void Unequip(EquipmentObject equipmentObject)
    {
        base.Unequip(equipmentObject);

        UIManager.Instance.UpdateEquipment();

        Bag.AddItem(equipmentObject, 1);
        UIManager.Instance.UpdateBag();
    }

    /// <summary>
    /// Takes physical damage
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="attacker"></param>
    public override void TakeDamage(float amount, Entity attacker)
    {
        base.TakeDamage(amount, attacker);
        if (Dead) Die();
        AudioManager.Instance.PlayHit();
    }

    /// <summary>
    /// Targets an entity and attacks it
    /// </summary>
    /// <param name="target"></param>
    public override void AttackEntity(Entity target)
    {
        base.AttackEntity(target);   
    }

    /// <summary>
    /// Triggers death
    /// </summary>
    public override void Die()
    {
        UIManager.Instance.ShowPanelDead();
        base.Die();
    }

    /// <summary>
    /// Respawns the player
    /// </summary>
    public override void Resurrect()
    {
        if (Dead)
        {
            base.Resurrect();

            SetPosition(StartPos.x,StartPos.y);
            GameManager.Instance.GoToFirstLevel();
            TileManager.Instance.DrawMap();
            UpdateStats();
        }
    }

    #region FILE MANAGEMENT

    /// <summary>
    /// Saves the player character to disk
    /// </summary>
    public void Save()
    {
        PlayerSave saveObject = new PlayerSave
        {
            CharacterName = this.CharacterName,
            WorldPosition = this.WorldPosition,
            Stats = this.Stats,
            Bag = this.Bag,
            Gold = this.Gold,
            Equipment = new EquipmentSave
            {
                Head = new Item(Equipment.Head),
                Back = new Item(Equipment.Back),
                Torso = new Item(Equipment.Torso),
                Legs = new Item(Equipment.Legs),
                Feet = new Item(Equipment.Feet),
                Gloves = new Item(Equipment.Gloves),
                Primary = new Item(Equipment.Primary),
                Secondary = new Item(Equipment.Secondary),
                Shirt = new Item(Equipment.Shirt),
            },
            SpellBook = this.SpellBook,
            Abilities = this.Abilities,
            Skills = this.Skills,
        };

        FileManager.SavePlayer(saveObject);
    }

    /// <summary>
    /// Loads a previous saved character
    /// </summary>
    public void Load()
    {
        PlayerSave saveObject = FileManager.LoadPlayer(CharacterName);
        Stats = saveObject.Stats;
        Bag = saveObject.Bag;
        Gold = saveObject.Gold;
        SpellBook = saveObject.SpellBook;
        Abilities = saveObject.Abilities;
        Skills = saveObject.Skills;

        if (saveObject.Equipment.Head.Id != -1)
        {
            EquipmentObject equipmentObject = (EquipmentObject)Bag.Database.ItemLookup[saveObject.Equipment.Head.Id];
            Equipment.Head = equipmentObject;
        }
        if (saveObject.Equipment.Back.Id != -1)
        {
            EquipmentObject equipmentObject = (EquipmentObject)Bag.Database.ItemLookup[saveObject.Equipment.Back.Id];
            Equipment.Back = equipmentObject;
        }
        if (saveObject.Equipment.Torso.Id != -1)
        {
            EquipmentObject equipmentObject = (EquipmentObject)Bag.Database.ItemLookup[saveObject.Equipment.Torso.Id];
            Equipment.Torso = equipmentObject;
        }
        if (saveObject.Equipment.Legs.Id != -1)
        {
            EquipmentObject equipmentObject = (EquipmentObject)Bag.Database.ItemLookup[saveObject.Equipment.Legs.Id];
            Equipment.Legs = equipmentObject;
        }
        if (saveObject.Equipment.Feet.Id != -1)
        {
            EquipmentObject equipmentObject = (EquipmentObject)Bag.Database.ItemLookup[saveObject.Equipment.Feet.Id];
            Equipment.Feet = equipmentObject;
        }
        if (saveObject.Equipment.Gloves.Id != -1)
        {
            EquipmentObject equipmentObject = (EquipmentObject)Bag.Database.ItemLookup[saveObject.Equipment.Gloves.Id];
            Equipment.Gloves = equipmentObject;
        }
        if (saveObject.Equipment.Primary.Id != -1)
        {
            EquipmentObject equipmentObject = (EquipmentObject)Bag.Database.ItemLookup[saveObject.Equipment.Primary.Id];
            Equipment.Primary = equipmentObject;
        }
        if (saveObject.Equipment.Secondary.Id != -1)
        {
            EquipmentObject equipmentObject = (EquipmentObject)Bag.Database.ItemLookup[saveObject.Equipment.Secondary.Id];
            Equipment.Secondary = equipmentObject;
        }
        if (saveObject.Equipment.Shirt.Id != -1)
        {
            EquipmentObject equipmentObject = (EquipmentObject)Bag.Database.ItemLookup[saveObject.Equipment.Shirt.Id];
            Equipment.Shirt = equipmentObject;
        }

        UIManager.Instance.UpdateBag();
        UIManager.Instance.UpdateEquipment();
        UIManager.Instance.UpdateSpellBook();
        UIManager.Instance.UpdateAbilities();
        UIManager.Instance.UpdateSkills();
        UpdateStats();
    }

    void OnApplicationQuit()
    {
        ClearData();
    }

    #endregion

}
