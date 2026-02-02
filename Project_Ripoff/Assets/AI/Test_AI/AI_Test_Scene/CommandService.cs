using UnityEngine;

/**
 *  @author Dominik Sandler
 *  
 *  Erlaubt es die eizenlnen Events über den Inspector auszufuehren
 * 
 */

public class CommandService : MonoBehaviour
{
    private Messaging_Service messaging;

    private void Awake()
    {
        messaging = FindObjectOfType<Messaging_Service>();
    }

    [ContextMenu("Trigger Follow Player Event")]
    public void CommandFollowPlayer()
    {
        messaging.followPlayerEvent?.Invoke();
    }

    [ContextMenu("Trigger Hold Position Event")]
    public void CommandHoldPosition()
    {
        messaging.holdPositionEvent?.Invoke();
    }

    [ContextMenu("Trigger Move to Position Event")]
    public void CommandMoveToPosition() { 

        messaging.teammateMoveEvent?.Invoke(new Vector3(5,5,5));
    }

    [ContextMenu("Trigger Wedge Fromation")]
    public void CommandFormationWedge()
    {
        messaging.wedgeFormationEvent?.Invoke();
    }

    [ContextMenu("Trigger Line Fromation")]
    public void CommandFormationLine()
    {
        messaging.lineFormationEvent?.Invoke();
    }

    [ContextMenu("Trigger None Fromation")]
    public void CommandFormationNone()
    {
        messaging.noneFormationEvent?.Invoke();
    }
}
