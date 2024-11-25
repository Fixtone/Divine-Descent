using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Scenery", menuName = "Scenery/Scenery")]
public class SceneryObject : ScriptableObject
{
    public string SpriteName = "None";
    public Color Colour = Color.white;
    public SceneryClass SceneryClassification = SceneryClass.INANIMATE; //The kind of scenery
}
