using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public partial class SROptions
{
    // Default Value for property
    private bool _fieldOfView = true;

    // Options will be grouped by category
    [Category("Player Field Of View")]
    public bool FieldOfView
    {
        get { return _fieldOfView; }
        set
        {
            _fieldOfView = value;

            MapManager.Instance.currentMap.UpdatePlayerFieldOfView(GameManager.Instance.player.GetComponent<Player>());
            MapManager.Instance.currentMap.Draw();
        }
    }
}
