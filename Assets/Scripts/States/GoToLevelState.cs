
using UnityEngine;

/// <summary>
/// This is example of game state.
/// It shows game view and can load some content related to gameplay.
/// </summary>
public class GoToLevelState : BaseState
{
    public int mapIdGoingTo;
    public override void EnterState()
    {
        base.EnterState();

        GoToLevel();
    }

    private void GoToLevel()
    {
        FileManager.Instance.SaveCurrentMap();

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

    // private void GoToDownLevel()
    // {
    //     FileManager.Instance.SaveCurrentMap();

    //     foreach (Transform child in GameManager.Instance.StairsParent)
    //     {
    //         GameObject.Destroy(child.gameObject);
    //     }

    //     foreach (Transform child in GameManager.Instance.MonstersParent)
    //     {
    //         GameObject.Destroy(child.gameObject);
    //     }

    //     SchedulingManager.Instance.Clear();

    //     StateManager.Instance.PushState(new GenerateMapState());

    //     WorldManager.Instance.currentMap.Draw();

    //     StateManager.Instance.ChangeState(new PlayerTurnState());
    // }

    // private void GoToUpLevel()
    // {
    //     FileManager.Instance.SaveCurrentMap();

    //     foreach (Transform child in GameManager.Instance.StairsParent)
    //     {
    //         GameObject.Destroy(child.gameObject);
    //     }

    //     foreach (Transform child in GameManager.Instance.MonstersParent)
    //     {
    //         GameObject.Destroy(child.gameObject);
    //     }

    //     SchedulingManager.Instance.Clear();

    //     StateManager.Instance.PushState(new LoadMapState());

    //     WorldManager.Instance.currentMap.Draw();

    //     StateManager.Instance.ChangeState(new PlayerTurnState());
    // }
}
