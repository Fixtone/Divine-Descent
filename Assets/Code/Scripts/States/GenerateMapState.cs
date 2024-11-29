
using System;
using UnityEngine;

public class GenerateMapState : BaseState
{

    public override void PrepareState()
    {
        base.PrepareState();

        owner.UiRoot.GenerateMapView.ShowView();

        MapManager.Instance.GenerateNewMap();

        StateManager.Instance.ChangeState(new PlayerTurnState());
    }

    public override void DestroyState()
    {
        base.DestroyState();

        owner.UiRoot.GenerateMapView.HideView();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
