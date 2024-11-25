using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Recover Ability", menuName = "Abilities/Ability/Recover Ability")]
public class RecoverObject : AbilityObject
{
    public float HealthRecovery = 0;
    public float ManaRecovery = 0;

    private void Awake()
    {
        AbilityType = AbilityType.ABILITY;
    }

    /// <summary>
    /// Perform the ability
    /// </summary>
    /// <param name="performer"></param>
    public override void Perform(Being performer)
    {
        if (HasReagents(performer))
        {
            base.Perform(performer);
            if (Reagents.Length > 0) TakeRegeants(performer);

            performer.Stats.Health += HealthRecovery;
            performer.Stats.Mana += ManaRecovery;

            if (performer == Player.Instance)
            {
                Player.Instance.UpdateStats();
                if(Reagents.Length > 0) UIManager.Instance.UpdateBag();
            }
        }
    }
}
