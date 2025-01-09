using UnityEngine;

/// <summary>
/// UI Root class, used for storing references to UI views.
/// </summary>
public class UIRoot : MonoBehaviour
{
    [SerializeField]
    private MainMenuView mainMenuView;
    public MainMenuView MainMenuView => mainMenuView;

    [SerializeField]
    private NewGameView newGameView;
    public NewGameView NewGameView => newGameView;

    [SerializeField]
    private LoadGameView loadGameView;
    public LoadGameView LoadGameView => loadGameView;

    [SerializeField]
    private GenerateMapView generateMapView;
    public GenerateMapView GenerateMapView => generateMapView;

    [SerializeField]
    private PauseView pauseView;
    public PauseView PauseView => pauseView;
}
