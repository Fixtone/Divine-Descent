using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Direction FiringDirection = Direction.NONE;
    public float Speed = 1;
    public int Distance = 3;
    public SpriteRenderer Avatar;
    public bool Fired = false;
    private Vector3 startPoint = Vector3.zero;
    public Being Firer;

    void Start()
    {

    }

    void Update()
    {
        if(Fired)
        {
            DoMovement();

            if (Vector3.Distance(transform.position, startPoint) >= Distance)
                KillMe();
        }
    }

    /// <summary>
    /// Move the projectile
    /// </summary>
    private void DoMovement()
    {
        Vector3 direction = Vector3.zero;

        switch (FiringDirection)
        {
            case Direction.NORTH:
                direction = new Vector3(0, 1, 0);
                break;
            case Direction.SOUTH:
                direction = new Vector3(0, -1, 0);
                break;
            case Direction.WEST:
                direction = new Vector3(-1, 0, 0);
                break;
            case Direction.EAST:
                direction = new Vector3(1, 0, 0);
                break;
            default:
                direction = new Vector3(1, 0, 0);
                break;
        }

        transform.position += direction * Time.deltaTime * Speed;
    }

    /// <summary>
    /// Sets the projectile's sprite and colour
    /// </summary>
    /// <param name="spriteName"></param>
    /// <param name="colour"></param>
    public void SetSprite(string spriteName, Color colour)
    {
        if (Avatar == null) Avatar = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Avatar.sprite = GameManager.Instance.GetSprite(spriteName);
        Avatar.color = colour;
    }

    /// <summary>
    /// Fires the projectile
    /// </summary>
    public virtual void Fire()
    {
        Fired = true;
        startPoint = transform.position;
        Distance = TileManager.Instance.GetClearDistance(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Distance, FiringDirection);
    }

    /// <summary>
    /// Destroy this projectile
    /// </summary>
    protected virtual void KillMe()
    {
        Destroy(gameObject);
    }
   
}
