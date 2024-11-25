using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Nuke Spell", menuName = "Magic/Spells/Nuke")]
public class NukeObject : SpellObject
{
    private void Awake()
    {
        SpellType = SpellType.NUKE;
    }

    /// <summary>
    /// Cast the spell
    /// </summary>
    /// <param name="caster">The being who casts the spell</param>
    public override void Cast(Being caster)
    {
        if (caster.Stats.Mana >= ManaCost)
        {
            if (HasReagents(caster))
            {
                base.Cast(caster);
                if (Reagents.Length > 0) TakeRegeants(caster);

                if (Focus == Focus.PROJECTILE)
                {
                    GameObject projectilePrefab = Resources.Load("Projectiles/Spell Projectile") as GameObject;
                    GameObject projectileInstance = Instantiate(projectilePrefab, caster.transform.position, Quaternion.identity);
                    SpellProjectile projectile = projectileInstance.GetComponent<SpellProjectile>();
                    projectile.Firer = caster;
                    projectile.SetSprite(SpriteName, Colour);
                    projectile.Speed = 6;
                    projectile.Distance = Range;
                    projectile.FiringDirection = caster.FacingDirection;
                    projectile.SpellObject = this;
                    projectile.Fire();
                    caster.DoAnim(AnimMove.ATTACK);
                }

                if (caster == Player.Instance)
                {
                    if (Reagents.Length > 0) UIManager.Instance.UpdateBag();
                }
            }
        }
    }

    /// <summary>
    /// When the spell hits another Being
    /// </summary>
    /// <param name="beingHit">The Being hit</param>
    public override void Hit(Being beingHit)
    {
        beingHit.TakeDamage(Value + myCaster.MagicRating + myCaster.Skills.GetSkillValue(Skill), myCaster);
    }

}
