
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

        bool didAction = false;
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            CommandManager.instance.MoveActor(GameManager.Instance.player.GetComponent<Player>(), Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            CommandManager.instance.MoveActor(GameManager.Instance.player.GetComponent<Player>(), Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            CommandManager.instance.MoveActor(GameManager.Instance.player.GetComponent<Player>(), Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            CommandManager.instance.MoveActor(GameManager.Instance.player.GetComponent<Player>(), Direction.Down);
        }

        if (didAction)
        {
            TileManager.Instance.Draw();
            StateManager.Instance.AddState(new EnemyTurnState());
        }
    }
}
