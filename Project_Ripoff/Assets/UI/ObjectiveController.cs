using UnityEngine;
using System;
using UnityEngine.UIElements;


/**
 * Die Klasse bietet eine Funktionalität für das Pause Menu.
 * @author Viktor Bublinskyy
 * @version 1.0.0
 */
public class ObjectiveController : MonoBehaviour
{
    private VisualElement root;
    private Messaging_Service messaging_Service;
    private ProgressBar progressBar;
    private bool isComplete = false;
    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
    }

    /**
     * Initialisiert alle Elemente und verbindet die Verbindungsknöpfe und Schließknöpfe mit den Methoden die definiert wurden. 
     * Und fügt sich zum Messaging Service hinzu  
     */
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        progressBar = root.Q<ProgressBar>("progressBar");
        if (messaging_Service != null)
        {
            messaging_Service.updateProgress += updateProgress;
            messaging_Service.objectiveComplete += OnObjectiveComplete;
            //    messaging_Service.hideInteractionHint += hideInteractionHint;
            //  messaging_Service.showInteractionHint += showInteractionHint;
        }
    }

    /** 
     * Deabonniert zum Event
     */
    private void OnDisable()
    {
        if (messaging_Service != null)
        {
            messaging_Service.updateProgress -= updateProgress;
        }

    }

    private void updateProgress(float progress)
    {
        if (isComplete) return;

        if (progressBar != null)
        {
            progressBar.value = progress;
        }
    }

    private void OnObjectiveComplete()
    {
        isComplete = true;

        if (progressBar != null)
        {
            progressBar.value = progressBar.highValue;

            var progressFill = progressBar.Q<VisualElement>(className: "unity-progress-bar__progress");
            if (progressFill != null)
            {
                progressFill.style.backgroundColor = Color.green;
            }
        }
    }

    private void hideInteractionHint()
    {
        root.style.display = DisplayStyle.None;
    }

    private void showInteractionHint()
    {
        root.style.display = DisplayStyle.Flex;
    }
}
