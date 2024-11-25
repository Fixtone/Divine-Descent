using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private InputField inputCharacterName;
    [SerializeField] private InputField inputSeed;
    [SerializeField] private Dropdown dropdownTileset;

    void Start()
    {
        string charName = PlayerPrefs.GetString("CharName");
        if (charName == "") charName = "Player";
        inputCharacterName.text = charName;
        inputSeed.text = UnityEngine.Random.Range(0, 9999).ToString();
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        string charName = inputCharacterName.text;
        if (charName == "") charName = "Player";
        PlayerPrefs.SetString("CharacterName", charName);
        int seed = int.Parse(inputSeed.text);
        PlayerPrefs.SetInt("Seed",  seed);
        PlayerPrefs.SetString("TileSet",(dropdownTileset.options[dropdownTileset.value].text));
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game Scene");
    }
}
