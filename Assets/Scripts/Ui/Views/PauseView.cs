using UnityEngine;
using UnityEngine.Events;

public class PauseView : BaseView
{

    // Events to attach to.
    public UnityAction OnContinueClicked;
    public UnityAction OnSaveClicked;
    public UnityAction OnExitClicked;

    public void OnClickContinue()
    {
        OnContinueClicked?.Invoke();
    }

    public void OnClickSave()
    {
        OnSaveClicked?.Invoke();
    }

    public void OnClickExit()
    {
        OnExitClicked?.Invoke();
    }
}
