using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewGameView : BaseView
{
    public TMPro.TMP_InputField characterNameInputField;
    public TMPro.TMP_InputField seedInputField;

    public UnityAction OnGenerateNewGameClicked;

    public void OnClickGenerateNewGame()
    {
        OnGenerateNewGameClicked?.Invoke();
    }
}
