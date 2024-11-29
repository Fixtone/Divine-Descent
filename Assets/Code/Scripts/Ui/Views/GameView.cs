using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Game view class.
/// Passes button events.
/// </summary>
public class GameView : BaseView
{
    // Events to attach to.
    public UnityAction OnPauseClicked;
    public UnityAction OnFinishClicked;

    /// <summary>
    /// Method called by Pause Button.
    /// </summary>
    public void PauseClick()
    {
        OnPauseClicked?.Invoke();
    }

    /// <summary>
    /// Method called by Finish Button.
    /// </summary>
    public void FinishClick()
    {
        OnFinishClicked?.Invoke();
    }
}
