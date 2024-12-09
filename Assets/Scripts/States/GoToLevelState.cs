
using UnityEngine;

/// <summary>
/// This is example of game state.
/// It shows game view and can load some content related to gameplay.
/// </summary>
public class GoToLevelState : BaseState
{
    public Stairs.Type stairsType;
    public override void EnterState()
    {
        base.EnterState();

        switch (stairsType)
        {
            case Stairs.Type.Up:
                {
                    break;
                }
            case Stairs.Type.Down:
                {
                    GoToDownLevel();
                    break;
                }
        };
    }

    private void GoToDownLevel()
    {
        FileManager.Instance.SaveGame();

        foreach (Transform child in GameManager.Instance.StairsParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (Transform child in GameManager.Instance.MonstersParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        SchedulingManager.Instance.Clear();

        StateManager.Instance.PushState(new GenerateMapState());

        WorldManager.Instance.currentMap.Draw();

        StateManager.Instance.ChangeState(new PlayerTurnState());
    }

    private void GoToUpLevel()
    {
    }
}
