using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CommanderMenuController : MonoBehaviour
{
    private Messaging_Service messaging_Service;
    private UIDocument uiDocument;
    private VisualElement root;
    private VisualElement rootInfo;
    private VisualElement infolabelMoveTeammatePosition;
    private VisualElement commanderMenu;
    private UIDocument infoMoveTeammatePosition;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public GameObject infoTeamMovePosition;

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
        uiDocument = GetComponent<UIDocument>();
        infoMoveTeammatePosition = infoTeamMovePosition.GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        root = uiDocument.rootVisualElement;
        rootInfo = infoMoveTeammatePosition.rootVisualElement;
        infolabelMoveTeammatePosition = rootInfo.Q<VisualElement>("label-chooselocation");
        infolabelMoveTeammatePosition.style.display = DisplayStyle.None;
        commanderMenu = root.Q<VisualElement>("command-menu");
        if (commanderMenu != null)
        {
            commanderMenu.style.display = DisplayStyle.None;
        }
        messaging_Service.openCommanderMenu += OpenCommanderMenu;
        messaging_Service.selectTeamMovePosition += SelectTeamMovePosition;
    }

    private void OnDisable()
    {
        messaging_Service.openCommanderMenu -= OpenCommanderMenu;
    }

    private void OpenCommanderMenu()
    {
        if (commanderMenu == null) return;

        if (commanderMenu.style.display == DisplayStyle.None)
        {
            commanderMenu.style.display = DisplayStyle.Flex;
        }
        else
        {
            commanderMenu.style.display = DisplayStyle.None;
        }
    }
    private void SelectTeamMovePosition(Boolean active)
    {
        if (active)
        {
            infolabelMoveTeammatePosition.style.display = DisplayStyle.Flex;
            UnityEngine.Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        }
        else
        {
            infolabelMoveTeammatePosition.style.display = DisplayStyle.None;
            UnityEngine.Cursor.SetCursor(null, hotSpot, cursorMode);
        }
    }
}
