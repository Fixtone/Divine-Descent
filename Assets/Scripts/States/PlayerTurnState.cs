
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

        WorldManager.Instance.currentMap.Draw();
    }

    public override void DestroyState()
    {
        base.DestroyState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        bool didAction = false;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            didAction = CommandManager.Instance.MoveActor(GameManager.Instance.player.GetComponent<Player>(), Direction.Right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            didAction = CommandManager.Instance.MoveActor(GameManager.Instance.player.GetComponent<Player>(), Direction.Left);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            didAction = CommandManager.Instance.MoveActor(GameManager.Instance.player.GetComponent<Player>(), Direction.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            didAction = CommandManager.Instance.MoveActor(GameManager.Instance.player.GetComponent<Player>(), Direction.Down);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            FileManager.Instance.SaveGame();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            FileManager.Instance.LoadGame();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            StateManager.Instance.AddState(new GoToLevelState { stairsType = Stairs.Type.Down });
        }

        if (didAction)
        {
            WorldManager.Instance.currentMap.Draw();
            CameraManager.Instance.UpdateCamera();

            StateManager.Instance.AddState(new EnemyTurnState());
        }
    }
}
