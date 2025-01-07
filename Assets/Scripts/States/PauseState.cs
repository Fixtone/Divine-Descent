
using System;
using UnityEngine;

public class PauseState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();

        owner.UiRoot.PauseView.OnContinueClicked += ContinueClicked;
        owner.UiRoot.PauseView.OnSaveClicked += SaveClicked;
        owner.UiRoot.PauseView.OnExitClicked += ExitClicked;

        owner.UiRoot.PauseView.ShowView();
    }

    public override void DestroyState()
    {
        owner.UiRoot.PauseView.HideView();
        
        owner.UiRoot.PauseView.OnContinueClicked -= ContinueClicked;
        owner.UiRoot.PauseView.OnSaveClicked -= SaveClicked;
        owner.UiRoot.PauseView.OnExitClicked -= ExitClicked;

        base.DestroyState();
    }

    private void ContinueClicked()
    {
        StateManager.Instance.PopState();
    }

    private void SaveClicked()
    {
        FileManager.Instance.SavePlayer();
        FileManager.Instance.SaveCurrentMap();

        StateManager.Instance.PopState();
    }

    public void ExitClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
