using UnityEngine.Events;

public class InfoPopupView : BaseView
{
    public TMPro.TextMeshProUGUI infoTMP;
    // Events to attach to.
    public UnityAction OnOkClicked;

    public void SetInfoText(string infoText)
    {
        infoTMP.text = infoText;
    }

    public void OnClickOk()
    {
        OnOkClicked?.Invoke();
    }
}
