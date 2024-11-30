
using System;
using UnityEngine;

public class EnemyTurnState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();

        bool didAction = true;

        if (didAction)
        {
            StateManager.Instance.AddState(new PlayerTurnState());
        }
    }
}
