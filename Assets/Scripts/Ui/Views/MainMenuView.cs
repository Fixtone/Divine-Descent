using UnityEngine.Events;

public class MainMenuView : BaseView
{
    // Events to attach to.
    public UnityAction OnNewGameClicked;
    public UnityAction OnLoadClicked;
    public UnityAction OnExitClicked;

    public void OnClickNewGame()
    {
        OnNewGameClicked?.Invoke();
    }

    public void OnClickLoad()
    {
        OnLoadClicked?.Invoke();
    }

    public void OnClickExit()
    {
        OnExitClicked?.Invoke();
    }
}
