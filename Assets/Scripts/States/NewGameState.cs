
using System;
using RogueSharp.Random;
using UnityEngine;

public class NewGameState : BaseState
{
    private string _characterName;
    private int _worldSeed;

    public override void PrepareState()
    {
        base.PrepareState();

        NewGameView newGameViewRef = owner.UiRoot.NewGameView;

        newGameViewRef.OnGenerateNewGameClicked += OnGenerateNewGameClicked;

        _characterName = "Character";
        newGameViewRef.characterNameInputField.text = _characterName;

        _worldSeed = UnityEngine.Random.Range(1, int.MaxValue);
        newGameViewRef.seedInputField.text = _worldSeed.ToString();

        newGameViewRef.ShowView();
    }

    private void OnGenerateNewGameClicked()
    {
        FileManager.Instance.setPlayerDirectoryName(_characterName);
        FileManager.Instance.createPlayerDirectoryName();

        GameManager.Instance.WorldSeed = _worldSeed;
        GameManager.Instance.WorldRandom = new DotNetRandom(_worldSeed);

        StateManager.Instance.PushState(new GenerateNewWorldState());
    }

    public override void DestroyState()
    {
        owner.UiRoot.NewGameView.HideView();

        base.DestroyState();
    }
}
