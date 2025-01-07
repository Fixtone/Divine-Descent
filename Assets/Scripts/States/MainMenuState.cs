
using System;
using UnityEngine;

public class MainMenuState : BaseState
{
    public override void PrepareState()
    {
        base.PrepareState();

        owner.UiRoot.MainMenuView.ShowView();
    }

    public override void DestroyState()
    {
        base.DestroyState();

        owner.UiRoot.MainMenuView.HideView();
    }
}
