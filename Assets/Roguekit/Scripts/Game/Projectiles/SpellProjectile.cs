using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : Projectile
{
    public SpellObject SpellObject;

    /// <summary>
    /// Check if a being has been hit
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Being>())
        {
            if (col.GetComponent<Being>() != Firer)
            {
                SpellObject.Hit(col.GetComponent<Being>());
                KillMe();
            }
        }
    }
}
