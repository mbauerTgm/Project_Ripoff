using MBT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/**
 * Die Klasse regelt alle Eingaben des Spielers 
 * und weist alle anderen Services mittels auslösen von Events
 * zu auf Eingaben folgende Aktionen an.
 * @author Maximilian Bauer
 * @version 1.0.5
 */
public class Input_Manager : MonoBehaviour
{
    private Messaging_Service messaging_Service;

    private Camera mainCamera;
    private VisualElement root;
    private VisualElement rootOpen;
    private bool wasdWasPressed = false;
    private bool isMiddleMouseHeld = false;
    private bool selectTeamMovePosition = false;
    private bool wasSelectTeamMovePosition = false;
    
    private LayerMask floorLayer;

    [Header("Combat Settings")]
    [SerializeField] private float playerFireRate = 0.45f; // Zeit in Sekunden zwischen Schüssen
    private float nextFireTime = 0f;

    [Header("Interaction Settings")]
    [SerializeField] private LayerMask objectiveInteractionLayer;
    private ObjectiveInteractable currentHoveredObjective;
    private Collider lastHitCollider;
    private bool interactionButtonPressed = false;

    [Header("UI")]
    [SerializeField] private bool sceneWithUI = true;
    public GameObject openCommandMenuUI;

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
        floorLayer = LayerMask.GetMask("Floor");
    }

    private void OnEnable()
    {
        if(sceneWithUI)
        {

            var uiDocument = GetComponent<UIDocument>();
            root = uiDocument.rootVisualElement;
            var uiOpenCommandMenuDocument = openCommandMenuUI.GetComponent<UIDocument>();
            rootOpen = uiOpenCommandMenuDocument.rootVisualElement;
            var openCommandMenuButton = rootOpen.Q<VisualElement>("button_opencommandmenu");
            var commandMenu = root.Q<VisualElement>("command-menu");
            commandMenu.style.position = Position.Absolute;
            //commandMenu.style.display = DisplayStyle.None;
            var moveToPanel = root.Q<VisualElement>("panel_moveto");
            var followLeaderPanel = root.Q<VisualElement>("panel_followleader");
            var suppressPositionPanel = root.Q<VisualElement>("panel_suppressposition");
            var stackUpPanel = root.Q<VisualElement>("panel_stackup");
            var enterAndClearPanel = root.Q<VisualElement>("panel_enterandclear");
            var holdPositionPanel = root.Q<VisualElement>("panel_holdposition");

            openCommandMenuButton.RegisterCallback<ClickEvent>(ev => messaging_Service.openCommanderMenu?.Invoke());
            moveToPanel.RegisterCallback<ClickEvent>(ev => HandleCommanderAction("teammateMoveEvent"));
            followLeaderPanel.RegisterCallback<ClickEvent>(ev => HandleCommanderAction("followPlayerEvent"));
            suppressPositionPanel.RegisterCallback<ClickEvent>(ev => HandleCommanderAction("TODO"));
            stackUpPanel.RegisterCallback<ClickEvent>(ev => HandleCommanderAction("TODO"));
            enterAndClearPanel.RegisterCallback<ClickEvent>(ev => HandleCommanderAction("TODO"));
            holdPositionPanel.RegisterCallback<ClickEvent>(ev => HandleCommanderAction("holdPositionEvent"));

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        //----------------------
        //player movement
        if(!selectTeamMovePosition)
        {   
            //queued move
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(1)) 
            { 
                messaging_Service.playerMoveShiftEvent?.Invoke(EvaluateMousePosition()); 
            }
            //normal move
            else if (Input.GetMouseButtonDown(1)) { messaging_Service.playerMoveEvent?.Invoke(EvaluateMousePosition()); }
            //visualize queue
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                messaging_Service.showPlayerQueue?.Invoke(true);
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                messaging_Service.showPlayerQueue?.Invoke(false);
            }
        }

        //----------------------
        //player interaction
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime) {
            nextFireTime = Time.time + playerFireRate;

            messaging_Service.fireLaserShotPlayer?.Invoke(); 
        }

        //----------------------
        // Objective Interaction
        if (Input.GetKeyDown(KeyCode.F)) { 
            interactionButtonPressed = true; 
            messaging_Service.interactObjectiveButtonDepress?.Invoke();
        }
        if (Input.GetKeyUp(KeyCode.F)) { 
            interactionButtonPressed = false; 
            messaging_Service.interactObjectiveButtonRelease?.Invoke();
        }
        HandleInteractionLogic();

        //----------------------
        //camera

        //Kamera bewegen mit W,A,S,D,Pfeiltasten
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(horizontal) > 0.01f || Math.Abs(vertical) > 0.01f) 
        { 
            Vector3 direction = new Vector3(horizontal, 0, vertical);
            messaging_Service.moveCameraTarget?.Invoke(direction); 
        } else 
        {
            
            //messaging_Service.moveCameraTarget?.Invoke(Vector3.zero);
        }

        // Freie Kamerabewegung mit gedrücktem Mausrad
        if (Input.GetMouseButtonDown(2)) { messaging_Service.rotateCameraWithMouse?.Invoke(true); }
        if (Input.GetMouseButtonUp(2)) { messaging_Service.rotateCameraWithMouse?.Invoke(false); }

        // Kamera Zoom mit Mausrad
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f) { messaging_Service.zoomCamera?.Invoke(scroll); }

        //----------------------
        //AI

        if (selectTeamMovePosition)
        {
            wasSelectTeamMovePosition = true;
            messaging_Service.selectTeamMovePosition?.Invoke(true);
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(1))
            {
                messaging_Service.teammateMoveShiftEvent?.Invoke(EvaluateMousePosition());
            }
            else if (Input.GetMouseButtonDown(1)) {
                messaging_Service.teammateMoveEvent?.Invoke(EvaluateMousePosition());
                selectTeamMovePosition = false;
            } else if (Input.GetMouseButtonDown(0)) { selectTeamMovePosition = false; }
        } else if (wasSelectTeamMovePosition)
        {
            wasSelectTeamMovePosition = false;
            messaging_Service.selectTeamMovePosition?.Invoke(false);
        }

        //----------------------
        //UI

        if (Input.GetKeyDown(KeyCode.F))
        {
            messaging_Service.openCommanderMenu?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            messaging_Service.togglePauseMenu?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            HandleCommanderAction("teammateMoveEvent");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            HandleCommanderAction("holdPositionEvent");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HandleCommanderAction("followPlayerEvent");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            HandleCommanderAction("TODO");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            HandleCommanderAction("TODO");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            HandleCommanderAction("TODO");
        }
    }

    /**
     * Ermittelt die Position auf der sich der Mauszeiger des Spielers befindet 
     * und gibt diese als Vector3 zurück.
     * @returns Vector3
     */
    private Vector3 EvaluateMousePosition()
    {
        // Ray von der Kamera zur Mausposition
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, floorLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 2f); // Debug Linie sichtbar im Scene View
            return hitInfo.point;
        } else
        {
            Debug.LogError("Failed to find Mouse Position");
            return gameObject.transform.position;
        }
        
    }

    private void HandleInteractionLogic()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, objectiveInteractionLayer))
        {
            if (hit.collider != lastHitCollider || lastHitCollider == null)
            {
                currentHoveredObjective = hit.collider.GetComponent<ObjectiveInteractable>();
                lastHitCollider = hit.collider;
            }

            if(currentHoveredObjective != null) { 
                // Distanz-Check
                Vector3 currentPlayerPosition = (Vector3) messaging_Service.getPlayerPosition?.Invoke();

                float distance = Vector3.Distance(currentHoveredObjective.transform.position, currentPlayerPosition);
                if (distance > currentHoveredObjective.requiredDistance)
                {
                    messaging_Service.resetProgressEvent?.Invoke(currentHoveredObjective); // Zu weit weg -> Abbruch
                    return;
                }
                messaging_Service.processInteractionEvent?.Invoke(currentHoveredObjective);
            }
        }
        else
        {
            //Nothing hit
            if (currentHoveredObjective != null)
            {
                messaging_Service.resetProgressEvent?.Invoke(currentHoveredObjective);
                currentHoveredObjective = null;
            }
            lastHitCollider = null;
        }
    }

    private void HandleCommanderAction(string action)
    {
        if (action == "teammateMoveEvent")
        {
            selectTeamMovePosition = true;
        }
        else if (action == "followPlayerEvent")
        {
            messaging_Service.followPlayerEvent?.Invoke();
        } else if (action == "holdPositionEvent")
        {
            messaging_Service.holdPositionEvent?.Invoke();
        }
    }
}
