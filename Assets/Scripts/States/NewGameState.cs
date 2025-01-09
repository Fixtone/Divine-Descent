
using System;
using RogueSharp.Random;
using UnityEngine;

public class NewGameState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();

        NewGameView newGameViewRef = owner.UiRoot.NewGameView;

        newGameViewRef.OnGenerateNewGameClicked += OnGenerateNewGameClicked;

        newGameViewRef.InitData(UnityEngine.Random.Range(1, int.MaxValue), "Character");

        newGameViewRef.ShowView();
    }

    private void OnGenerateNewGameClicked(string directoryName, int seed)
    {
        FileManager.Instance.setPlayerDirectoryName(directoryName);
        FileManager.Instance.createPlayerDirectoryName();

        GameManager.Instance.WorldSeed = seed;
        GameManager.Instance.WorldRandom = new DotNetRandom(seed);

        StateManager.Instance.ChangeState(new GenerateNewWorldState());
    }

    public override void DestroyState()
    {
        owner.UiRoot.NewGameView.HideView();

        base.DestroyState();
    }
}
