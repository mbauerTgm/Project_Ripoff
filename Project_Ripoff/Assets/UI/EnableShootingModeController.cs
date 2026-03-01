using UnityEngine;
using UnityEngine.UIElements;

public class EnableShootingModeController : MonoBehaviour
{
    private VisualElement root;
    private Button enableButton; 
    private Messaging_Service messaging_Service;
    private bool isShootingModeActive = false;
    private readonly Color defaultColor = new Color(61f / 255f, 61f / 255f, 61f / 255f, 0.67f);
    private readonly Color activeColor = new Color(1f, 0f, 0f, 0.67f);

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
    }

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        if (uiDocument == null) return;
        root = uiDocument.rootVisualElement;
        enableButton = root.Q<Button>("button_enableshootingmode");
        root.Q<Button>("button_enableshootingmode").clicked += OnButtonClicked;

        if (messaging_Service != null)
        {
            messaging_Service.toggleShootingMode += ToggleShootingModeHandler;
        }
    }

    private void OnDisable()
    {
        if (messaging_Service != null)
        {
            messaging_Service.toggleShootingMode -= ToggleShootingModeHandler;
        }
    }

    private void OnButtonClicked()
    {
        messaging_Service.toggleShootingMode?.Invoke();
    }

    private void ToggleShootingModeHandler()
    {
        if (enableButton == null) return;

        isShootingModeActive = !isShootingModeActive;

        if (isShootingModeActive)
        {
            enableButton.style.backgroundColor = activeColor;
            enableButton.text = "DISABLE SHOOTING MODE"; 
        }
        else
        {
            enableButton.style.backgroundColor = defaultColor;
            enableButton.text = "ENABLE SHOOTING MODE"; 
        }
    }
}