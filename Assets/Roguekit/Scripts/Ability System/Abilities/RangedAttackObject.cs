using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Ranged Attack", menuName = "Abilities/Ability/Ranged Attack")]
public class RangedAttackObject : AbilityObject
{
    public int Range = 5;

    private void Awake()
    {
        AbilityType = AbilityType.COMBAT;
    }

    /// <summary>
    /// Perform the ability
    /// </summary>
    /// <param name="performer"></param>
    public override void Perform(Being performer)
    {
        if (performer.Equipment.Primary != null)
        {
            WeaponObject weaponObject = (WeaponObject)performer.Equipment.Primary;
            Debug.Log(weaponObject.IsRanged);
            if (weaponObject.IsRanged)
            {
                if (weaponObject.Ammo != null)
                {
                    if (performer.Bag.HasItem(weaponObject.Ammo))
                    {
                        base.Perform(performer);
                        Fire(performer);
                        performer.Bag.RemoveItem(weaponObject.Ammo, 1);
                        if (performer.transform == Player.Instance.transform)
                            UIManager.Instance.UpdateBag();
                    }
                }
                else
                {
                    base.Perform(performer);
                    Fire(performer);
                }
            }
        }
    }

    /// <summary>
    /// Fire the projectile
    /// </summary>
    /// <param name="performer"></param>
    private void Fire(Being performer)
    {
        GameObject projectilePrefab = Resources.Load("Projectiles/Ability Projectile") as GameObject;
        GameObject projectileInstance = Instantiate(projectilePrefab, performer.transform.position, Quaternion.identity);
        AbilityProjectile projectile = projectileInstance.GetComponent<AbilityProjectile>();
        projectile.Firer = performer;
        projectile.SetSprite(SpriteName, Colour);
        projectile.Speed = 6;
        projectile.Distance = Range;
        projectile.FiringDirection = performer.FacingDirection;
        projectile.AbilityObject = this;
        projectile.Fire();
        performer.DoAnim(AnimMove.ATTACK);
        AudioManager.Instance.PlayRanged();
    }


    public override void Hit(Being beingHit)
    {
        beingHit.TakeDamage(myPerformer.AttackRating + myPerformer.Skills.GetSkillValue(Skill), myPerformer);
    }
}
