
using RogueSharp.Random;

public class LoadGameState : BaseState
{
    private int _worldSeed;

    public override void PrepareState()
    {
        base.PrepareState();

        string[] gameSavesNamesList = FileManager.Instance.GetGameSavesNames();

        owner.UiRoot.LoadGameView.OnLoadGameClicked += OnGameClicked;
        owner.UiRoot.LoadGameView.UpdateData(gameSavesNamesList);

        owner.UiRoot.LoadGameView.ShowView();
    }

    public override void DestroyState()
    {
        owner.UiRoot.LoadGameView.HideView();

        base.DestroyState();
    }

    private void OnGameClicked(string directoryName)
    {
        if (string.IsNullOrEmpty(directoryName))
        {
            StateManager.Instance.PushState(new InfoPopupState { infoText = "You need to select one saved game." });

            return;
        }

        FileManager.Instance.setPlayerDirectoryName(directoryName);

        WorldSave worldSave = FileManager.Instance.GetWorldSave();

        GameManager.Instance.WorldSeed = 1234;
        GameManager.Instance.WorldRandom = new DotNetRandom(_worldSeed);

        StateManager.Instance.ChangeState(new GoToLevelState { mapIdGoingTo = worldSave.currentMapId, saveCurrentMap = false });
    }
}
