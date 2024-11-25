using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Melee Attack", menuName = "Abilities/Ability/Melee Attack")]
public class MeleeAttackObject : AbilityObject
{
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

            if(!weaponObject.IsRanged)
            {
                if (performer.Target != null)
                {
                    performer.AttackTarget(Value + performer.Skills.GetSkillValue(Skill));
                    performer.DoAnim(AnimMove.ATTACK);
                    base.Perform(performer);
                    AudioManager.Instance.PlayAttack();
                }
            }
        }
    }
}
