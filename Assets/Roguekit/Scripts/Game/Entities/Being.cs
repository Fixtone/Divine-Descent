using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimMove
{
    ATTACK,
}

public class Being : Entity
{
    public InventoryObject Bag;
    public Equipment Equipment = new Equipment();
    public SpellBookObject SpellBook;
    public AbilitiesObject Abilities;
    public SkillSet Skills;
    public int Gold = 0;
    public bool Attacking = false; //Is the being attacking an entity or is the player in combat mode?
    [Range(0, 0.99f)] public float AttackRate = .99f; //The rate in which it should attack in realtime mode

    protected Animator Anim;

    #region RATINGS

    /// <summary>
    /// The being's calculated attack rating
    /// </summary>
    public float AttackRating
    {
        get
        {
            float rating = 5; //Base rating

            if (Equipment.Primary != null)      rating += Equipment.Primary.AttackRating;
            if (Equipment.Secondary != null)    rating += Equipment.Secondary.AttackRating;
            if (Equipment.Head != null)         rating += Equipment.Head.AttackRating;
            if (Equipment.Torso != null)        rating += Equipment.Torso.AttackRating;
            if (Equipment.Legs != null)         rating += Equipment.Legs.AttackRating;
            if (Equipment.Gloves != null)       rating += Equipment.Gloves.AttackRating;
            if (Equipment.Feet != null)         rating += Equipment.Feet.AttackRating;
            if (Equipment.Shirt != null)        rating += Equipment.Shirt.AttackRating;

            rating += Stats.Strength;

            return rating;
        }
    }

    /// <summary>
    /// The being's calculated defence rating
    /// </summary>
    public float DefenceRating
    {
        get
        {
            float rating = 0; //Base rating

            if (Equipment.Primary != null)      rating += Equipment.Primary.DefenceRating;
            if (Equipment.Secondary != null)    rating += Equipment.Secondary.DefenceRating;
            if (Equipment.Head != null)         rating += Equipment.Head.DefenceRating;
            if (Equipment.Torso != null)        rating += Equipment.Torso.DefenceRating;
            if (Equipment.Legs != null)         rating += Equipment.Legs.DefenceRating;
            if (Equipment.Gloves != null)       rating += Equipment.Gloves.DefenceRating;
            if (Equipment.Feet != null)         rating += Equipment.Feet.DefenceRating;
            if (Equipment.Shirt != null)        rating += Equipment.Shirt.DefenceRating;
            
            rating += Stats.Constitution;

            return rating;
        }
    }

    /// <summary>
    /// The being's calculated magic rating
    /// </summary>
    public float MagicRating
    {
        get
        {
            float rating = 0; //Base rating

            if (Equipment.Primary != null)      rating += Equipment.Primary.MagicRating;
            if (Equipment.Secondary != null)    rating += Equipment.Secondary.MagicRating;
            if (Equipment.Head != null)         rating += Equipment.Head.MagicRating;
            if (Equipment.Torso != null)        rating += Equipment.Torso.MagicRating;
            if (Equipment.Legs != null)         rating += Equipment.Legs.MagicRating;
            if (Equipment.Gloves != null)       rating += Equipment.Gloves.MagicRating;
            if (Equipment.Feet != null)         rating += Equipment.Feet.MagicRating;
            if (Equipment.Shirt != null)        rating += Equipment.Shirt.MagicRating;

            rating += Stats.Intelligence;

            return rating;
        }
    }

    #endregion

    /// <summary>
    /// Should the being attack this tick
    /// </summary>
    protected bool AttackThisTick
    {
        get
        {
            if(!GameManager.Instance.Realtime) //For turn-based
                return true; 
            if (tickCount % ((1 - AttackRate) * 1000) == 0) return true; //For real-time
            return false;
        }
    }

    /// <summary>
    /// The entity's classification
    /// </summary>
    public override EntityClass Classification
    {
        get
        {
            return EntityClass.BEING;
        }
    }

    /// <summary>
    /// The distance the entity can attack
    /// </summary>
    protected int AttackRange
    {
        get
        {
            return 1;
        }
    }

    protected override void Start()
    {
        base.Start();

        if (Bag == null) SetupBag();
        if (SpellBook == null) SetupSpellBook();
        if (Abilities == null) SetupAbilities();
    }

    /// <summary>
    /// Respawn the entity
    /// </summary>
    public virtual void Resurrect()
    {
        if (Dead)
        {
            Stats.Health = Stats.MaxHealth;
        }
    }

    /// <summary>
    /// Creates a new scritable InventoryObject for the entity's bag
    /// </summary>
    public virtual void SetupBag()
    {
        Bag = ScriptableObject.CreateInstance(typeof(InventoryObject)) as InventoryObject;
    }

    /// <summary>
    /// Creates a new scritable SpellBookObject for the entity's spell book
    /// </summary>
    public virtual void SetupSpellBook()
    {
        SpellBook = ScriptableObject.CreateInstance(typeof(SpellBookObject)) as SpellBookObject;
    }

    /// <summary>
    /// Creates a new scritable AbilitiesObject for the entity's abilities
    /// </summary>
    public virtual void SetupAbilities()
    {
        Abilities = ScriptableObject.CreateInstance(typeof(AbilitiesObject)) as AbilitiesObject;
    }

    /// <summary>
    /// Equips an EnquipmentObject
    /// </summary>
    /// <param name="equipmentObject"></param>
    public virtual void Equip(EquipmentObject equipmentObject)
    {
        if (equipmentObject.Slot == EquipmentSlot.HAT)
        {
            if (Equipment.Head != null) Unequip(Equipment.Head);
            Equipment.Head = equipmentObject;
        }
        else if (equipmentObject.Slot == EquipmentSlot.TORSO)
        {
            if (Equipment.Torso != null) Unequip(Equipment.Torso);
            Equipment.Torso = equipmentObject;
        }
        else if (equipmentObject.Slot == EquipmentSlot.LEGS)
        {
            if (Equipment.Legs != null) Unequip(Equipment.Legs);
            Equipment.Legs = equipmentObject;
        }
        else if (equipmentObject.Slot == EquipmentSlot.GLOVES)
        {
            if (Equipment.Gloves != null) Unequip(Equipment.Gloves);
            Equipment.Gloves = equipmentObject;
        }
        else if (equipmentObject.Slot == EquipmentSlot.FEET)
        {
            if (Equipment.Feet != null) Unequip(Equipment.Feet);
            Equipment.Feet = equipmentObject;
        }
        else if (equipmentObject.Slot == EquipmentSlot.SHIRT)
        {
            if (Equipment.Shirt != null) Unequip(Equipment.Shirt);
            Equipment.Shirt = equipmentObject;
        }
        else if (equipmentObject.Slot == EquipmentSlot.PRIMARY)
        {
            if (Equipment.Primary != null) Unequip(Equipment.Primary);
            Equipment.Primary = equipmentObject;
        }
        else if (equipmentObject.Slot == EquipmentSlot.SECONDARY)
        {
            if (Equipment.Secondary != null) Unequip(Equipment.Secondary);
            Equipment.Secondary = equipmentObject;
        }
    }

    /// <summary>
    /// Takes off an EquipmentObject if it's being worn
    /// </summary>
    /// <param name="equipmentObject"></param>
    public virtual void Unequip(EquipmentObject equipmentObject)
    {
        if (equipmentObject.Slot == EquipmentSlot.HAT)
            Equipment.Head = null;
        else if (equipmentObject.Slot == EquipmentSlot.TORSO)
            Equipment.Torso = null;
        else if (equipmentObject.Slot == EquipmentSlot.LEGS)
            Equipment.Legs = null;
        else if (equipmentObject.Slot == EquipmentSlot.GLOVES)
            Equipment.Gloves = null;
        else if (equipmentObject.Slot == EquipmentSlot.FEET)
            Equipment.Feet = null;
        else if (equipmentObject.Slot == EquipmentSlot.SHIRT)
            Equipment.Shirt = null;
        else if (equipmentObject.Slot == EquipmentSlot.PRIMARY)
            Equipment.Primary = null;
        else if (equipmentObject.Slot == EquipmentSlot.SECONDARY)
            Equipment.Secondary = null; 
    }
    
    /// <summary>
    /// Toggles the being's attacking state
    /// </summary>
    public virtual void ToggleAttacking()
    {
        SetAttacking(!Attacking);
    }

    /// <summary>
    /// Targets an entity and attacks it
    /// </summary>
    /// <param name="target"></param>
    public virtual void AttackEntity(Entity target)
    {
        SetAttacking(true);
        SetTarget(target);
    }

    /// <summary>
    /// Sets whether the entity is attacking or not
    /// </summary>
    /// <param name="attacking"></param>
    public virtual void SetAttacking(bool attacking)
    {
        Attacking = attacking;
    }

    /// <summary>
    /// Sets the entity's target
    /// </summary>
    /// <param name="entity"></param>
    protected virtual void SetTarget(Entity entity)
    {
        Target = entity;
    }

    /// <summary>
    /// Attacks the entity's current target
    /// </summary>
    public override void AttackTarget()
    {
        AttackTarget(0);
    }

    /// <summary>
    /// Attacks the entity's current target
    /// </summary>
    /// <param name="bonus">Extra damage to do</param>
    public override void AttackTarget(float bonus)
    {
        if (Target == null) return;

        //Check if it's a ranged weapon that requires ammo 
        if(Equipment.Primary != null)
        {
            if(Equipment.Primary.GetType() == typeof(WeaponObject))
            {
                WeaponObject weaponObject = (WeaponObject)Equipment.Primary;

                if(weaponObject.Ammo != null)
                {
                    if(Bag.HasItem(weaponObject.Ammo))
                    {
                        Bag.RemoveItem(weaponObject.Ammo,1);
                        if (this.transform == Player.Instance.transform)
                            UIManager.Instance.UpdateBag();
                    }
                    else
                    {
                        //Not enough ammo
                        return;
                    }
                }
            }
        }

        float damage = UnityEngine.Random.Range(1, AttackRating + bonus);
        Target.TakeDamage(damage, this);
    }

    /// <summary>
    /// Takes physical damage
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="attacker"></param>
    public override void TakeDamage(float amount, Entity attacker)
    {
        float mitigation = UnityEngine.Random.Range(0, DefenceRating);
        amount -= mitigation;
        if (amount < 1) amount = 1;

        base.TakeDamage(amount, attacker);
    }

    /// <summary>
    /// Do an animation
    /// </summary>
    /// <param name="move">The animation move to perform</param>
    public virtual void DoAnim(AnimMove move)
    {
        if (Anim == null) Anim = Avatar.GetComponent<Animator>();

        switch(move)
        {
            case AnimMove.ATTACK:
                if (FacingDirection == Direction.NORTH) Anim.SetTrigger("Attack_North");
                else if (FacingDirection == Direction.EAST) Anim.SetTrigger("Attack_East");
                else if (FacingDirection == Direction.SOUTH) Anim.SetTrigger("Attack_South");
                else if (FacingDirection == Direction.WEST) Anim.SetTrigger("Attack_West");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Triggers death
    /// </summary>
    public override void Die()
    {
        base.Die();

        //Drop items from bag
        foreach(InventorySlot slot in Bag.Container.Slots)
        {
            if (slot.Id != -1)
            {
                ItemObject itemObject = Bag.Database.ItemLookup[slot.Id];
                GameManager.Instance.SpawnDropAt(itemObject, WorldX, WorldY);
                Bag.RemoveItem(slot, slot.Amount);
            }
        }

        if (Target != null)
        {
            System.Type t = Target.GetType();
            if (t == typeof(Being) || t == typeof(Player) || t == typeof(Mob))
            {
                ((Being)Target).Gold += Gold;
                if (Target.gameObject == Player.Instance.gameObject) UIManager.Instance.UpdateGold();
            }
        }
    }

}
