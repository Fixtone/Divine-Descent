
using System;
using UnityEngine;

public class MainMenuState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();

        owner.UiRoot.MainMenuView.OnNewGameClicked += OnNewGameClicked;
        owner.UiRoot.MainMenuView.OnLoadClicked += OnLoadClicked;
        owner.UiRoot.MainMenuView.OnExitClicked += OnExitClicked;

        owner.UiRoot.MainMenuView.ShowView();
    }

    public override void DestroyState()
    {
        owner.UiRoot.MainMenuView.OnNewGameClicked -= OnNewGameClicked;
        owner.UiRoot.MainMenuView.OnLoadClicked -= OnLoadClicked;
        owner.UiRoot.MainMenuView.OnExitClicked -= OnExitClicked;

        owner.UiRoot.MainMenuView.HideView();

        base.DestroyState();
    }

    private void OnNewGameClicked()
    {
        StateManager.Instance.ChangeState(new NewGameState());
    }

    private void OnLoadClicked()
    {
        StateManager.Instance.ChangeState(new LoadGameState());
    }

    private void OnExitClicked()
    {
        throw new NotImplementedException();
    }
}
