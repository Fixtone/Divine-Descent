
using System;
using System.Collections.Generic;
using RogueSharp.Random;
using UnityEngine;

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
        FileManager.Instance.setPlayerDirectoryName(directoryName);

        WorldSave worldSave = FileManager.Instance.GetWorldSave();

        GameManager.Instance.WorldSeed = 1234;
        GameManager.Instance.WorldRandom = new DotNetRandom(_worldSeed);

        StateManager.Instance.PushState(new GoToLevelState { mapIdGoingTo = worldSave.currentMapId, saveCurrentMap = false });
    }
}
