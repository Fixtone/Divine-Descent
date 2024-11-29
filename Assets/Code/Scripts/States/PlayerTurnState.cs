
using System;
using UnityEngine;

/// <summary>
/// This is example of game state.
/// It shows game view and can load some content related to gameplay.
/// </summary>
public class PlayerTurnState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();

        TileManager.Instance.Draw();
    }

    public override void DestroyState()
    {
        base.DestroyState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
