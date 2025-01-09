using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewGameView : BaseView
{
    public TMPro.TMP_InputField characterNameInputField;
    public TMPro.TMP_InputField seedInputField;

    private int _seed;
    private string _directoryName;

    public UnityAction<string, int> OnGenerateNewGameClicked;

    public void InitData(int seed, string directoryName)
    {
        _seed = seed;
        _directoryName = directoryName;

        characterNameInputField.text = directoryName;
        seedInputField.text = seed.ToString();
    }

    public void OnClickGenerateNewGame()
    {
        OnGenerateNewGameClicked?.Invoke(_directoryName, _seed);
    }

    public void OnCharacterNameEndEdit()
    {
        _directoryName = characterNameInputField.text;
    }

    public void OnSeedEndEdit()
    {
        _seed = int.Parse(seedInputField.text);
    }
}
