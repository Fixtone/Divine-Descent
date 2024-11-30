using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    #region Singleton
    public static StateManager Instance;
    #endregion

    [SerializeField]
    private UIRoot uiRoot;
    public UIRoot UiRoot => uiRoot;

    private Stack<BaseState> currentStates = new Stack<BaseState>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        BaseState currentState = currentStates.Peek();
        if (currentState == null)
        {
            return;
        }

        currentStates.Peek().UpdateState();
    }

    public void AddState(BaseState newState)
    {
        currentStates.Push(newState);
        BaseState currentState = currentStates.Peek();

        if (currentState != null)
        {
            currentState.owner = this;
            currentState.PrepareState();
            currentState.EnterState();
        }
    }

    public void ChangeState(BaseState newState)
    {
        foreach (BaseState state in currentStates)
        {
            // If we currently have state, we need to destroy it!
            if (state != null)
            {
                state.ExitState();
                state.DestroyState();
            }
        }

        currentStates.Clear();

        // Swap reference

        currentStates.Push(newState);

        BaseState currentState = currentStates.Peek();
        // If we passed reference to new state, we should assign owner of that state and initialize it!
        // If we decided to pass null as new state, nothing will happened.
        if (currentState != null)
        {
            currentState.owner = this;
            currentState.PrepareState();
            currentState.EnterState();
        }
    }

    public void PushState(BaseState newState)
    {
        BaseState currentState = currentStates.Peek();
        // If we currently have state, we need to destroy it!
        if (currentState != null)
        {
            currentState.ExitState();
        }

        // Swap reference
        currentStates.Push(newState);
        currentState = currentStates.Peek();
        // If we passed reference to new state, we should assign owner of that state and initialize it!
        // If we decided to pass null as new state, nothing will happened.
        if (currentState != null)
        {
            currentState.owner = this;
            currentState.PrepareState();
            currentState.EnterState();
        }
    }

    public void PopState()
    {
        BaseState currentState = currentStates.Peek();

        // If we currently have state, we need to destroy it!
        if (currentState == null)
        {
            return;

        }

        currentState.ExitState();
        currentState.DestroyState();
        currentStates.Pop();
    }


}
