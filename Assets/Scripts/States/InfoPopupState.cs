
using System;
using UnityEngine;

public class InfoPopupState : BaseState
{
    public string infoText;

    public override void PrepareState()
    {
        base.PrepareState();

        owner.UiRoot.InfoPopupView.OnOkClicked += OnOkClicked;
        owner.UiRoot.InfoPopupView.SetInfoText(infoText);
        owner.UiRoot.InfoPopupView.ShowView();
    }

    public override void DestroyState()
    {
        owner.UiRoot.InfoPopupView.OnOkClicked -= OnOkClicked;

        owner.UiRoot.InfoPopupView.HideView();

        base.DestroyState();
    }

    private void OnOkClicked()
    {
        StateManager.Instance.PopState();
    }
}
