
using UnityEngine;

/// <summary>
/// This is example of game state.
/// It shows game view and can load some content related to gameplay.
/// </summary>
public class GoToLevelState : BaseState
{
    public int mapIdGoingTo;
    public bool saveCurrentMap = true;
    public override void PrepareState()
    {
        base.PrepareState();

        owner.UiRoot.GenerateMapView.ShowView();

        GoToLevel();
    }

    public override void DestroyState()
    {
        base.DestroyState();

        owner.UiRoot.GenerateMapView.HideView();
    }

    private void GoToLevel()
    {
        if (saveCurrentMap)
        {
            FileManager.Instance.SaveCurrentMap();
        }

        foreach (Transform child in GameManager.Instance.StairsParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in GameManager.Instance.MonstersParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        SchedulingManager.Instance.Clear();

        WorldManager.Instance.LoadMap(mapIdGoingTo);

        WorldManager.Instance.currentMap.Draw();

        StateManager.Instance.ChangeState(new PlayerTurnState());
    }
}
