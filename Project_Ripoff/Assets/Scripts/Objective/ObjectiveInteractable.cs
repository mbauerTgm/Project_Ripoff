using MBT;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static Laser_Shooter;

public class ObjectiveInteractable : MonoBehaviour
{
    private Messaging_Service messaging_Service;

    [Header("Einstellungen")]
    public float interactionTime = 2.0f; // (Sekunden)
    public float requiredDistance = 3.5f; // Wie nah der Player sein muss

    private float currentTimer = 0f;
    private bool isCompleted = false;
    private bool interactionActive = false;
    private bool interactionButtonPressed = false;

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
    }

    private void OnEnable()
    {
        messaging_Service.interactObjectiveButtonDepress += InteractObjectiveButtonDepress;
        messaging_Service.interactObjectiveButtonRelease += InteractObjectiveButtonRelease;
        messaging_Service.processInteractionEvent += ProcessInteraction;
        messaging_Service.resetProgressEvent += ResetProgress;
    }

    private void OnDisable()
    {
        messaging_Service.interactObjectiveButtonDepress -= InteractObjectiveButtonDepress;
        messaging_Service.interactObjectiveButtonRelease -= InteractObjectiveButtonRelease;
        messaging_Service.processInteractionEvent -= ProcessInteraction;
        messaging_Service.resetProgressEvent -= ResetProgress;
    }

    void Update()
    {
        if (interactionActive && interactionButtonPressed)
        {
            //Timer
            currentTimer += Time.deltaTime;

            //Progress
            float progress = Mathf.Clamp01(currentTimer / interactionTime);
            messaging_Service.updateInteractionBar?.Invoke(transform.position, progress);
            messaging_Service.updateProgress?.Invoke(progress);
            Debug.Log(progress);

            // Check ob fertig
            if (currentTimer >= interactionTime)
            {
                CompleteObjective();
            }
        }
    }

    public void ProcessInteraction(ObjectiveInteractable meantObjective)
    {
        if (this != meantObjective) return;

        if (isCompleted) return;

        interactionActive = true;
        messaging_Service.showInteractionHint?.Invoke(transform.position);
    }

    //Wird aufgerufen, wenn der Spieler loslässt oder wegschaut
    public void ResetProgress(ObjectiveInteractable meantObjective)
    {
        if (!this == meantObjective) return;

        interactionActive = false;

        currentTimer = 0f;
        messaging_Service.hideInteractionHint?.Invoke(transform.position);
        messaging_Service.updateInteractionBar?.Invoke(transform.position, 0f);
        messaging_Service.updateProgress?.Invoke(0f);
    }

    private void CompleteObjective()
    {
        isCompleted = true;
        Debug.Log("Objective Completed!");
        messaging_Service.objectiveComplete?.Invoke();
    }

    private void InteractObjectiveButtonDepress() { interactionButtonPressed = true; }

    private void InteractObjectiveButtonRelease() { interactionButtonPressed = false; }
}
