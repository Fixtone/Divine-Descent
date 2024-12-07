using UnityEngine;

/// <summary>
/// UI Root class, used for storing references to UI views.
/// </summary>
public class UIRoot : MonoBehaviour
{
    [SerializeField]
    private GenerateMapView generateMapView;
    public GenerateMapView GenerateMapView => generateMapView;
}
