using UnityEngine;
public class Player : Actor
{
    protected override void Start()
    {
        base.Start();
    }

    public PlayerSave Save()
    {
        PlayerSave playerSave = new PlayerSave();
        playerSave.characterName = Name;
        playerSave.mapPosition = transform.localPosition;

        return playerSave;
    }

    public void Load(PlayerSave playerSave)
    {
        Name = playerSave.characterName;
        transform.localPosition = playerSave.mapPosition;
    }
}