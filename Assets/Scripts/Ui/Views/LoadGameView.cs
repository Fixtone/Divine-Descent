using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LoadGameView : BaseView
{
    public GameObject saveGameButtonPrefab;
    public Transform content;
    public UnityAction<string> OnLoadGameClicked;

    private string _savedNameSelected;

    public void UpdateData(string[] gameSavePaths)
    {
        if (gameSavePaths.Length <= 0)
        {
            return;
        }

        foreach (string gameSavePath in gameSavePaths)
        {
            GameObject saveGameButtonInstance = GameObject.Instantiate(saveGameButtonPrefab, content);
            SavesGameButtonComponent savesGameButtonComponent = saveGameButtonInstance.GetComponent<SavesGameButtonComponent>();

            string gameSaveName = Path.GetFileName(gameSavePath);
            savesGameButtonComponent.button.onClick.RemoveAllListeners();
            savesGameButtonComponent.button.onClick.AddListener(() => OnSelectSaveToLoad(gameSaveName));

            savesGameButtonComponent.buttonText.text = gameSaveName;
        }
    }

    public void OnClickLoadGame()
    {
        OnLoadGameClicked?.Invoke(_savedNameSelected);
    }

    private void OnSelectSaveToLoad(string gameSaveName)
    {
        _savedNameSelected = gameSaveName;
    }
}
