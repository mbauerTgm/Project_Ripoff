using UnityEngine;
using System;
using UnityEngine.UIElements;
using NUnit.Framework.Constraints;

/**
 * Die Klasse bietet eine Funktionalität für das Pause Menu.
 * @author Viktor Bublinskyy
 * @version 1.0.0
 */
public class PauseController : MonoBehaviour
{
    private VisualElement root;
    private Messaging_Service messaging_Service;
    private VisualElement mainmenuElement;
    public GameObject mainmenu;
    public MainMenuController mainMenuController;
    private VisualElement settingsPanel;

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
    }

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        //Debug.Log(mainmenu);
        //mainmenuElement = mainmenu.GetComponent<UIDocument>().rootVisualElement;
        //Debug.Log(mainmenuElement);
       // settingsPanel = mainmenuElement.Q<VisualElement>("Settings");
        root.Q<Button>("button-resume").clicked += resumeGameThroughUIButton;
        root.Q<Button>("button-restart").clicked += OnRestartClicked;
        root.Q<Button>("button-settings").clicked += mainMenuController.OnSettingsClicked;
        root.Q<Button>("button-exit").clicked += OnExitClicked;
        root.style.display = DisplayStyle.None;
    }
    /**
     * Initialisiert alle Elemente und verbindet die Verbindungsknöpfe und Schließknöpfe mit den Methoden die definiert wurden. 
     * Und fügt sich zum Messaging Service hinzu  
     */
    private void OnEnable()
    {

        if (messaging_Service != null)
        {
            messaging_Service.togglePauseMenu += togglePauseMenu;
        }
    }
    /** 
     * Deabonniert zum Event
     */
    private void OnDisable()
    {
        if (messaging_Service != null)
        {
            messaging_Service.togglePauseMenu -= togglePauseMenu;
        }

    }

    /**
     * Hier wird das Messaging Service implementiert, um das Pausenmenü öffnen zu können.
     */
    private void togglePauseMenu()
    {
        if (root.style.display == DisplayStyle.Flex)
        {
            root.style.display = DisplayStyle.None; 
        }
        else
        {
            root.style.display = DisplayStyle.Flex;
        }
    }

    private void resumeGameThroughUIButton()
    {
        messaging_Service.togglePauseMenu?.Invoke();
    }

    /**
     * Das Level wird neugestart wenn Restart gedrückt wird
     */
    private void OnRestartClicked()
    {
        messaging_Service.restartLevel?.Invoke();
    }

    /**
     * Das Spiel wird geschlossen wenn Exit gedrückt wird
     */
    private void OnExitClicked()
    {
        Application.Quit();
    }


}
