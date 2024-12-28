
using UnityEngine;

/// <summary>
/// This is example of game state.
/// It shows game view and can load some content related to gameplay.
/// </summary>
public class GenerateNewWorldState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();

        //FileManager.Instance.CreateWorldSaveFile();

        StateManager.Instance.PushState(new GoToLevelState { mapIdGoingTo = 1, saveCurrentMap = false });
    }

    public override void DestroyState()
    {
        base.DestroyState();
    }
}
