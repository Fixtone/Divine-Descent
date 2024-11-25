using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Being
{
    [Range(0,1)] public float MoveRate = .5f;
    public bool Aggro = false; //Is it an aggressive mob 
    public float AggroRadius = 2;
    public int Size = 1;
    private Coroutine couroutineUpdatePath;
    public Vector3 TargetPos;
    
    public MobObject MobObject;
    private Vector3 previousPos = Vector3.zero;
    private bool waitForPath = false;
    public bool Shop = false;

    /// <summary>
    /// Can the mob move this tick
    /// </summary>
    private bool MoveThisTick
    {
        get
        {
            if (!GameManager.Instance.Realtime) 
                return true; //For turn-based
            if (tickCount % ((1 - MoveRate) * 1000) == 0) return true; //For real-time
            return false;
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Sets the mobs position on the game map
    /// </summary>
    public override void SetPositionOnMap()
    {
        base.SetPositionOnMap();
    }

    /// <summary>
    /// Populates the mob from a scriptable MobObject
    /// </summary>
    /// <param name="obj"></param>
    public void Populate(MobObject obj)
    {
        MobObject = obj;
        Avatar = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Avatar.sprite = GameManager.Instance.GetSprite(MobObject.SpriteName);

        startColour = !GameManager.Instance.ColourTiles ? Color.white : MobObject.Colour;
        transform.name = string.Format("Mob ({0})", MobObject.name);

        Aggro = MobObject.Aggro;
        AggroRadius = MobObject.AggroRadius;
        AttackRate = MobObject.AttackRate;

        SetupBag();
        Bag.PopulateDatabase();
        foreach (ItemObject itemObject in obj.Bag)
            Bag.AddItem(itemObject, 1);
        Gold = MobObject.Gold;

        SetupSpellBook();
        SpellBook.PopulateDatabase();

        Stats.BaseHealth = MobObject.Stats.BaseMaxHealth;
        Stats.BaseMaxHealth = MobObject.Stats.BaseMaxHealth;
        Stats.BaseMana = MobObject.Stats.BaseMaxHealth;
        Stats.BaseMaxMana = MobObject.Stats.BaseMaxHealth;
        Stats.Attributes = MobObject.Stats.Attributes;

        Shop = MobObject.Shop;
    }

    #region ASTAR

    private Vector3[] path;
    const float pathUpdateMoveThreshold = 0.25f;
    const float minPathUpdateTime = 0.2f;
    int targetIndex = 0;

    /// <summary>
    /// Coroutine to update the pathfinding path when the destination changes
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < 0.3f) yield return new WaitForSeconds(0.3f);

        if (Target != null)
        {
            Vector3 targetDesintation = TargetPos;
            PathRequestManager.RequestPath(new PathRequest(WorldPosition, targetDesintation, OnPathFound));

            float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
            Vector3 targetPosOld = TargetPos;

            while (true)
            {
                yield return new WaitForSeconds(minPathUpdateTime);
    
                if ((TargetPos - targetPosOld).sqrMagnitude > sqrMoveThreshold)
                {
                    PathRequestManager.RequestPath(new PathRequest(WorldPosition, TargetPos, OnPathFound));
                    targetPosOld = TargetPos;
                }
            }
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
        }
    }


    /// <summary>
    /// Called when a path is found
    /// </summary>
    /// <param name="newPath"></param>
    /// <param name="pathSuccessful"></param>
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            targetIndex = 0;
            path = newPath;
        }

        if (waitForPath)
        {
            Tick();
            waitForPath = false;
        }
    }

    #endregion

    /// <summary>
    /// Moves the mob to a given world position
    /// </summary>
    /// <param name="worldPositionToMoveTo"></param>
    private void MoveTo(Vector3 worldPositionToMoveTo)
    {
        previousPos = transform.position;
        WorldX = (int)worldPositionToMoveTo.x;
        WorldY = (int)worldPositionToMoveTo.y;
        SetPositionOnMap();
    }

    /// <summary>
    /// Handle the mob's game tick
    /// </summary>
    public override void Tick()
    {
        base.Tick();
        if (!PositionSet) return;
        SetShade();

        if(Aggro && Target == null) CheckAggro();

        if(Target != null)
        {
            if (TargetPos != Target.WorldPosition) //The target has moved so we need to wait for A* to update before ticking
            {
                waitForPath = true;
                TargetPos = Target.WorldPosition;
                return;
            }
        }

        //If we have a path then move to it
        if (path != null)
        {
            if (MoveThisTick)
            {
                bool moveInterrupted = false;

                Vector3 currentWaypoint = WorldPosition;

                if (targetIndex < path.Length)
                {
                    currentWaypoint = path[targetIndex];
                    targetIndex++;
                }

                Entity checkedEntity = CheckForEntities((currentWaypoint - WorldPosition).normalized, 1.25f, WorldPosition);

                if(checkedEntity != null)
                {
                    if (checkedEntity.Classification == EntityClass.BEING || checkedEntity.Classification == EntityClass.SCENERY)
                        moveInterrupted = true;
                }

                if (!moveInterrupted && !(currentWaypoint.x == WorldX && currentWaypoint.y == WorldY))
                {
                    MoveTo(currentWaypoint);
                }
            }

            //If the mob is attacking and in range of its target
            if (Attacking && Vector3.Distance(WorldPosition,TargetPos) <= AttackRange && AttackThisTick && !Target.Dead)
            {
                AttackTarget();

                Vector3 dir = (transform.position - TargetPos).normalized;
                if      (dir == new Vector3(0, -1, 0))  FacingDirection = Direction.NORTH;
                else if (dir == new Vector3(0, 1, 0))   FacingDirection = Direction.SOUTH;
                else if (dir == new Vector3(-1, 0, 0))  FacingDirection = Direction.EAST;
                else if (dir == new Vector3(1, 0, 0))   FacingDirection = Direction.WEST;

                DoAnim(AnimMove.ATTACK);

                //Check if on top of target
                if(transform.position == Target.transform.position)
                {
                    transform.position = previousPos;
                }
            }
        }
        
    }

    /// <summary>
    /// Checks if the mob has any entities within range that should trigger aggro
    /// </summary>
    protected void CheckAggro()
    {
        Collider2D[] objectsHit = Physics2D.OverlapCircleAll(transform.position, AggroRadius);

        foreach(Collider2D hit in objectsHit)
        {
            if (hit.gameObject != gameObject)
            {
                if (ShouldAttackEntity(hit.gameObject))
                {
                    AttackEntity(hit.gameObject.GetComponent<Entity>());
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Check whether the entity should be attacked by the mob
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private bool ShouldAttackEntity(GameObject entity)
    {
        if (entity.GetComponent<Player>()) return true;
        return false;
    }

    /// <summary>
    /// Sets a given entity to be the mob's target
    /// </summary>
    /// <param name="entity"></param>
    protected override void SetTarget(Entity entity)
    {
        base.SetTarget(entity);

        TargetPos = entity.transform.position;
        if (couroutineUpdatePath != null) StopCoroutine(couroutineUpdatePath);
        couroutineUpdatePath = StartCoroutine(UpdatePath());
    }

    /// <summary>
    /// Takes physical damage
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="attacker"></param>
    public override void TakeDamage(float amount, Entity attacker)
    {
        base.TakeDamage(amount, attacker);
        SetTarget(attacker);
        Attacking = true;
        if (Dead) Die();
    }

    /// <summary>
    /// Triggers death
    /// </summary>
    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
    }
}
