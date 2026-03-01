using UnityEngine;
using MBT;
using System;

/**
 *  @author Dominik Sandler
 *  
 *  Erlaubt es die eizenlnen Events über den Inspector auszufuehren
 * 
 */

public class CommandService : MonoBehaviour
{
    private Messaging_Service messaging;
    public Blackboard blackboard;

    private void Awake()
    {
        messaging = FindFirstObjectByType<Messaging_Service>();
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

    [ContextMenu("Trigger Vee Fromation")]
    public void CommandFormationVee()
    {
        messaging.veeFormationEvent?.Invoke();
    }

    [ContextMenu("Trigger File Fromation")]
    public void CommandFormationFile()
    {
        messaging.fileFormationEvent?.Invoke();
    }

    [ContextMenu("Trigger Echelon Fromation")]
    public void CommandFormationEchelon()
    {
        messaging.echelonFormationEvent?.Invoke();
    }

    [ContextMenu("Trigger None Fromation")]
    public void CommandFormationNone()
    {
        messaging.noneFormationEvent?.Invoke();
    }

    [ContextMenu("Trigger Teammate Shooting")]
    public void CommandTeammateShooting()
    {
        blackboard.GetVariable<BoolVariable>("IsShootingTargetSet").Value = true;
        blackboard.GetVariable<Vector3Variable>("LaserTarget").Value = new Vector3(5, 5, 5);
        messaging.fireLaserShotTeammate?.Invoke();
        //blackboard.GetVariable<BoolVariable>("IsShootingTargetSet").Value = false;
    }
}
