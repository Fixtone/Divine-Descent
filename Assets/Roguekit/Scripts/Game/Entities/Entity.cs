using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityClass
{
    UNSET = -1,
    BEING = 0,
    SCENERY = 1,
    DROP = 2,
}

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))] //Make it kinematic
public abstract class Entity : MonoBehaviour
{
    public int WorldX = 0;
    public int WorldY = 0;
    public Stats Stats;
    public SpriteRenderer Avatar;
    public Entity Target;
    [TextArea(2, 5)] public string Description;
    public EntityHUD HUD;
    public Direction FacingDirection = Direction.NONE;

    protected float tickCount = 0;
    protected Color startColour = Color.white;
    public bool PositionSet = false;

    /// <summary>
    /// The entity's classification
    /// </summary>
    public virtual EntityClass Classification
    {
        get
        {
            return EntityClass.UNSET;
        }
    }

    /// <summary>
    /// The entity's position in the world
    /// </summary>
    public Vector3 WorldPosition
    {
        get
        {
            return new Vector3(WorldX, WorldY, 0);
        }
        set
        {
            WorldX = (int)value.x;
            WorldY = (int)value.y;
        }
    }

    /// <summary>
    /// Can the entity be seen by the player
    /// </summary>
    public bool Visible
    {
        get
        {
            return TileManager.Instance.GetShade(transform.position) > 0;
        }
    }

    /// <summary>
    /// If the entity is dead
    /// </summary>
    public bool Dead
    {
        get
        {
            return Stats.Health == 0;
        }
    }

    protected virtual void Start()
    {
        if (Avatar == null) Avatar = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {

    }

    /// <summary>
    /// Moves the entity
    /// </summary>
    /// <param name="direction">The direction to move</param>
    public virtual void MoveInDirection(Direction direction)
    {
        int newWorldX, newWorldY = 0;

        switch (direction)
        {
            case Direction.NORTH:
                newWorldY = WorldY + 1;
                if (TileManager.Instance.CanMoveTo(WorldX, newWorldY))
                {
                    WorldY = newWorldY;
                }
                break;
            case Direction.SOUTH:
                newWorldY = WorldY - 1;
                if (TileManager.Instance.CanMoveTo(WorldX, newWorldY))
                {
                    WorldY = newWorldY;
                }
                break;
            case Direction.WEST:
                newWorldX = WorldX - 1;
                if (TileManager.Instance.CanMoveTo(newWorldX, WorldY))
                {
                    WorldX = newWorldX;
                }
                break;
            case Direction.EAST:
                newWorldX = WorldX + 1;
                if (TileManager.Instance.CanMoveTo(newWorldX, WorldY))
                {
                    WorldX = newWorldX;
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Checks for entities
    /// </summary>
    /// <param name="direction">The direction to check</param>
    /// <param name="distance">How far to check</param>
    /// <returns>An entity</returns>
    public virtual Entity CheckForEntities(Direction direction, float distance)
    {
        return CheckForEntities(direction, distance, transform.position);
    }

    /// <summary>
    /// Checks for entities
    /// </summary>
    /// <param name="direction">The direction to check</param>
    /// <param name="distance">How far to check</param>
    /// <param name="startPos">The start position</param>
    /// <returns>An entity</returns>
    public virtual Entity CheckForEntities(Direction direction, float distance, Vector3 startPos)
    {
        return CheckForEntities(DirectionToVector(direction), distance, startPos);
    }

    /// <summary>
    /// Checks for entities
    /// </summary>
    /// <param name="direction">The direction to check</param>
    /// <param name="distance">How far to check</param>
    /// <param name="startPos">The start position</param>
    /// <returns>An entity</returns>
    public virtual Entity CheckForEntities(Vector3 direction, float distance, Vector3 startPos)
    {
        Entity retEntity = null;

        RaycastHit2D[] hits = Physics2D.RaycastAll(startPos, direction, distance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject != gameObject)
            {
                if (hit.collider.gameObject.GetComponent<Entity>())
                {
                    retEntity = hit.collider.gameObject.GetComponent<Entity>();
                    if (retEntity.WorldPosition == WorldPosition) retEntity = null; //In case we're stood on top of it
                }
            }
        }

        return retEntity;
    }

    /// <summary>
    /// Converts a direction enum to a vector
    /// </summary>
    /// <param name="direction">Direction enum</param>
    /// <returns>Direction vector</returns>
    private Vector2 DirectionToVector(Direction direction)
    {
        Vector2 dir = Vector2.zero;

        if (direction == Direction.NORTH) dir = Vector2.up;
        if (direction == Direction.SOUTH) dir = -Vector2.up;
        if (direction == Direction.WEST) dir = Vector2.left;
        if (direction == Direction.EAST) dir = -Vector2.left;

        return dir;
    }

    /// <summary>
    /// Sets the entity's position on the world map
    /// </summary>
    public virtual void SetPositionOnMap()
    {
        transform.position = new Vector3(WorldX, WorldY, 0);
        SetShade();
        PositionSet = true;
    }

    /// <summary>
    /// Sets the avatar's shading
    /// </summary>
    public virtual void SetShade()
    {
        Color backgroundColour = new Color(startColour.r, startColour.g, startColour.b, 0);
        float shade = TileManager.Instance.GetShade(transform.position);
        Avatar.color = Color.Lerp(backgroundColour, startColour, shade); 
        if (HUD != null) HUD.GetComponent<CanvasGroup>().alpha = shade;
    }

    /// <summary>
    /// Handles the entity's game tick 
    /// </summary>
    public virtual void Tick()
    {
        tickCount++;
        if (tickCount == 1000) tickCount = 0;
        Stats.ProcessBuffs();
    }

    /// <summary>
    /// Attacks the entity's current target
    /// </summary>
    public virtual void AttackTarget()
    {
        
    }

    /// <summary>
    /// Attacks the entity's current target
    /// </summary>
    /// <param name="bonus">Extra damage to do</param>
    public virtual void AttackTarget(float bonus)
    {

    }

    /// <summary>
    /// Takes physical damage
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="attacker"></param>
    public virtual void TakeDamage(float amount, Entity attacker)
    {
        Stats.Health -= amount;

        if (HUD != null)
        {
            HUD.ShowDamage(amount);
            UpdateHealth();
        }
    }

    /// <summary>
    /// Updates the avatar's HP and MP display
    /// </summary>
    public void UpdateStats()
    {
        UpdateHealth();
        UpdateMana();
    }

    /// <summary>
    /// Updates the avatar's HP display
    /// </summary>
    public void UpdateHealth()
    {
        HUD.SetHealth(Stats.HealthPercentage);
    }

    /// <summary>
    /// Updates the avatar's MP display
    /// </summary>
    public void UpdateMana()
    {
        HUD.SetMana(Stats.ManaPercentage);
    }

    /// <summary>
    /// Triggers death
    /// </summary>
    public virtual void Die()
    {
        GameManager.Instance.Entities.Remove(this);

    }
}
