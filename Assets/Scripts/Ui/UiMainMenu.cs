using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RogueSharp;
using RogueSharp.MapCreation;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UiMainMenu : MonoBehaviour
{
    [SerializeField] private InputField inputCharacterName;
    [SerializeField] private InputField inputSeed;
    [SerializeField] private Dropdown dropdownTileset;

    // Start is called before the first frame update
    void Start()
    {
        string charName = PlayerPrefs.GetString("CharName");

        if (charName == string.Empty)
        {
            charName = "Player";
        }

        inputCharacterName.text = charName;

        int seed = (int)System.DateTime.Now.Ticks;
        inputSeed.text = seed.ToString();
    }

    // Update is called once per frame
    public void NewGame()
    {
        string charName = inputCharacterName.text;

        PlayerPrefs.SetString("CharacterName", charName);

        int seed = int.Parse(inputSeed.text);
        PlayerPrefs.SetInt("Seed", seed);

        PlayerPrefs.SetString("TileSet", (dropdownTileset.options[dropdownTileset.value].text));
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
