using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public class FileManager
{
    #region PLAYER

    /// <summary>
    /// Saves the player character
    /// </summary>
    /// <param name="saveObject"></param>
    public static void SavePlayer(PlayerSave saveObject)
    {
        string docPath = GetDocumentsPath() + "/" + saveObject.CharacterName + "_player.txt";

        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(docPath, json);

        SaveBag(saveObject.CharacterName, saveObject.Bag);
        SaveEquipment(saveObject.CharacterName, saveObject.Equipment);
        SaveSpellBook(saveObject.CharacterName, saveObject.SpellBook);
        SaveAbilities(saveObject.CharacterName, saveObject.Abilities);
    }

    /// <summary>
    /// Loads the player character
    /// </summary>
    /// <param name="characterName"></param>
    /// <returns></returns>
    public static PlayerSave LoadPlayer(string characterName)
    {
        PlayerSave returnPlayer = new PlayerSave();
        string docPath = GetDocumentsPath() + "/" + characterName + "_player.txt";

        if (File.Exists(docPath))
        {
            string saveString = File.ReadAllText(docPath);
            returnPlayer = JsonUtility.FromJson<PlayerSave>(saveString);
        }

        LoadBag(returnPlayer);
        LoadEquipment(returnPlayer);
        LoadSpellBook(returnPlayer);
        LoadAbilities(returnPlayer);

        return returnPlayer;
    }

    #endregion

    #region BAG

    /// <summary>
    /// Saves the bag
    /// </summary>
    /// <param name="characterName"></param>
    /// <param name="bag"></param>
    public static void SaveBag(string characterName, InventoryObject bag)
    {
        string saveData = JsonUtility.ToJson(bag.Container, true);
        BinaryFormatter bf = new BinaryFormatter();
        string path = string.Concat(GetDocumentsPath(), "/", characterName, "_bag.txt");
        FileStream file = File.Create(path);
        bf.Serialize(file, saveData);
        file.Close();
    }

    /// <summary>
    /// Loads the bag
    /// </summary>
    /// <param name="playerSave"></param>
    public static void LoadBag(PlayerSave playerSave)
    {
        string path = string.Concat(GetDocumentsPath(), "/", playerSave.CharacterName, "_bag.txt");

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            if (playerSave.Bag == null) playerSave.Bag = Player.Instance.Bag;
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), playerSave.Bag.Container);
            file.Close();
        }
    }

    #endregion

    #region EQUIPMENT

    /// <summary>
    /// Saves the player's equipment
    /// </summary>
    /// <param name="characterName"></param>
    /// <param name="equipment"></param>
    public static void SaveEquipment(string characterName, EquipmentSave equipment)
    {
        string saveData = JsonUtility.ToJson(equipment, true);
        BinaryFormatter bf = new BinaryFormatter();
        string path = string.Concat(GetDocumentsPath(), "/", characterName, "_equipment.txt");
        FileStream file = File.Create(path);
        bf.Serialize(file, saveData);
        file.Close();
    }

    /// <summary>
    /// Loads the player's equipment
    /// </summary>
    /// <param name="playerSave"></param>
    public static void LoadEquipment(PlayerSave playerSave)
    {
        string path = string.Concat(GetDocumentsPath(), "/", playerSave.CharacterName, "_equipment.txt");

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), playerSave.Equipment);
            file.Close();
        }
    }

    #endregion

    #region SPELL BOOK

    /// <summary>
    /// Saves the player's spellbook
    /// </summary>
    /// <param name="characterName"></param>
    /// <param name="book"></param>
    public static void SaveSpellBook(string characterName, SpellBookObject book)
    {
        string saveData = JsonUtility.ToJson(book.Book, true);
        BinaryFormatter bf = new BinaryFormatter();
        string path = string.Concat(GetDocumentsPath(), "/", characterName, "_spellbook.txt");
        FileStream file = File.Create(path);
        bf.Serialize(file, saveData);
        file.Close();
    }

    /// <summary>
    /// Loads the player's spellbook
    /// </summary>
    /// <param name="playerSave"></param>
    public static void LoadSpellBook(PlayerSave playerSave)
    {
        string path = string.Concat(GetDocumentsPath(), "/", playerSave.CharacterName, "_spellbook.txt");

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            if (playerSave.SpellBook == null) playerSave.SpellBook = Player.Instance.SpellBook;
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), playerSave.SpellBook.Book);
            file.Close();
        }
    }

    #endregion

    #region ABILITIES

    /// <summary>
    /// Saves the player's abilities
    /// </summary>
    /// <param name="characterName"></param>
    /// <param name="abilities"></param>
    public static void SaveAbilities(string characterName, AbilitiesObject abilities)
    {
        string saveData = JsonUtility.ToJson(abilities.AbilitiesSet, true);
        BinaryFormatter bf = new BinaryFormatter();
        string path = string.Concat(GetDocumentsPath(), "/", characterName, "_abilities.txt");
        FileStream file = File.Create(path);
        bf.Serialize(file, saveData);
        file.Close();
    }

    /// <summary>
    /// Loads the player's abilities
    /// </summary>
    /// <param name="playerSave"></param>
    public static void LoadAbilities(PlayerSave playerSave)
    {
        string path = string.Concat(GetDocumentsPath(), "/", playerSave.CharacterName, "_abilities.txt");

        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            if (playerSave.Abilities == null) playerSave.Abilities = Player.Instance.Abilities;
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), playerSave.Abilities.AbilitiesSet);
            file.Close();
        }
    }

    #endregion

    #region MISC

    /// <summary>
    /// Get the path to save/load data files
    /// </summary>
    /// <returns></returns>
    private static string GetDocumentsPath()
    {
        string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
        path = path.Substring(0, path.LastIndexOf('/'));
        return path;
    }

    #endregion
}
