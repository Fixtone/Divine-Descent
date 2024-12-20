﻿using UnityEngine;

/// <summary>
/// This is base state script implementation.
/// StateMachine uses these virtual methods to call state when it needs to prepare itself for operating, updating or even being destroyed.
/// </summary>
public abstract class BaseState
{
    // Reference to our state machine.
    public StateManager owner;

    /// <summary>
    /// Method called to prepare state to operate - same as Unity's Start()
    /// </summary>
    public virtual void PrepareState() { }

    public virtual void EnterState() { }
    /// <summary>
    /// Method called to update state on every frame - same as Unity's Update()
    /// </summary>
    public virtual void UpdateState() { }

    public virtual void ExitState() { }
    /// <summary>
    /// Method called to destroy state - same as Unity's OnDestroy() but here it might be important!
    /// </summary>
    public virtual void DestroyState() { }
}
