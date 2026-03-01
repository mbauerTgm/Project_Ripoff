using UnityEngine;
using MBT;
using System;

/**
 * @author Dominik Sandler 
 * 
 * Der TeameventListener sorgt,dass die einzelnen Teammate Events ausgeführt werden.
 */
public class TeammateEventListener : MonoBehaviour
{
    private Messaging_Service messaging;
    public Blackboard blackboard;
    private UnityEngine.AI.NavMeshAgent agent;
    public int teamIndex;
    public FormationManager formationManager;
    private Transform formationTargetHelper;
    private GameObject movetargetObject;
    private Laser_Shooter laserShooter;



    public void setIndex(int index)
    {
        this.teamIndex = index;
    }
    private void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GameObject go = new GameObject($"Target_{gameObject.name}");
        formationTargetHelper = go.transform;
        blackboard.GetVariable<TransformVariable>("TargetPosition").Value = formationTargetHelper;

    }

    private void Update()
    {
        var mode = blackboard.GetVariable<TeammateBehaviorModeVariable>("BehaviorMode").Value;
        var form = blackboard.GetVariable<TeammateFormationModeVariable>("FormationMode").Value;

        if (mode == TeammateBehaviorMode.FollowPlayer )
        {
            Destroy(movetargetObject);
            Vector3 targetPos;
            if (form == TeammateFormationMode.None)
            {
                blackboard.GetVariable<TransformVariable>("TargetPosition").Value = transform;
                targetPos = transform.position;
            }
            else
            {
                Debug.Log(mode + " | " + form);
                targetPos = formationManager.GetFormationPosition(teamIndex, form);
            }
            formationTargetHelper.position = targetPos;

            var targetVar = blackboard.GetVariable<TransformVariable>("TargetPosition");
            if (targetVar.Value != formationTargetHelper)
            {
                targetVar.Value = formationTargetHelper;
            }
        }
    }


    private void Awake()
    {
        messaging = FindFirstObjectByType<Messaging_Service>();
        laserShooter = FindFirstObjectByType<Laser_Shooter>();
    }


    private void OnEnable()
    {
        if (messaging == null)
        {
            messaging = FindFirstObjectByType<Messaging_Service>();
        }

        if (messaging != null)
        {
            messaging.followPlayerEvent += OnFollowPlayer;
            messaging.holdPositionEvent += OnHoldPosition;
            messaging.teammateMoveEvent += OnMoveTeammatesToPosition;

            messaging.wedgeFormationEvent += OnWedgeFormation;
            messaging.lineFormationEvent += OnLineFormation;
            messaging.veeFormationEvent += OnVeeFormation;
            messaging.fileFormationEvent += OnFileFormation;
            messaging.echelonFormationEvent += OnEchelonFormation;
            messaging.noneFormationEvent += OnNoneFormation;

            messaging.fireLaserShotTeammate += OnShootReceived;
        }
    }

    private void OnDisable()
    {
        if (messaging != null)
        {


            messaging.followPlayerEvent -= OnFollowPlayer;
            messaging.holdPositionEvent -= OnHoldPosition;
            messaging.teammateMoveEvent -= OnMoveTeammatesToPosition;

            messaging.wedgeFormationEvent -= OnWedgeFormation;
            messaging.lineFormationEvent -= OnLineFormation;
            messaging.veeFormationEvent -= OnVeeFormation;
            messaging.fileFormationEvent -= OnFileFormation;
            messaging.echelonFormationEvent -= OnEchelonFormation;
            messaging.noneFormationEvent -= OnNoneFormation;

            messaging.fireLaserShotTeammate -= OnShootReceived;
        }

        if (movetargetObject != null)
        {
            Destroy(movetargetObject);
        }
    }

    private void OnFollowPlayer()
    {
        var varRef = blackboard.GetVariable<TeammateBehaviorModeVariable>("BehaviorMode");
        if (varRef == null)
        {
            Debug.LogError(" Blackboard variable 'BehaviorMode' not found!");
            return;
        }

        var targetVar = blackboard.GetVariable<TransformVariable>("TargetPosition");
        targetVar.Value = formationTargetHelper;

        if (movetargetObject != null){
            Destroy(movetargetObject);
            movetargetObject = null;
        }

        varRef.Value = TeammateBehaviorMode.FollowPlayer;
    }

    private void OnHoldPosition()
    {
        var varRef = blackboard.GetVariable<TeammateBehaviorModeVariable>("BehaviorMode");
        varRef.Value = TeammateBehaviorMode.Idle;
    }

    private void OnMoveTeammatesToPosition(Vector3 movePosition)
    { 
        var varRef = blackboard.GetVariable<TeammateBehaviorModeVariable>("BehaviorMode");
        var targetVar = blackboard.GetVariable<TransformVariable>("TargetPosition");

        if (movetargetObject != null)
        {
            Destroy(movetargetObject);
        }

        movetargetObject = new GameObject("TeammateMoveTarget");
        movetargetObject.transform.position = movePosition;

        targetVar.Value = movetargetObject.transform;
        varRef.Value = TeammateBehaviorMode.MoveToPosition;
    }

    private void OnWedgeFormation()
    {
        blackboard.GetVariable<TeammateFormationModeVariable>("FormationMode").Value = TeammateFormationMode.Wedge;
    }

    private void OnLineFormation()
    {
        blackboard.GetVariable<TeammateFormationModeVariable>("FormationMode").Value = TeammateFormationMode.Line;
    }
    private void OnVeeFormation()
    {
        blackboard.GetVariable<TeammateFormationModeVariable>("FormationMode").Value = TeammateFormationMode.Vee;
    }
    private void OnFileFormation()
    {
        blackboard.GetVariable<TeammateFormationModeVariable>("FormationMode").Value = TeammateFormationMode.File;
    }
    private void OnEchelonFormation()
    {
        blackboard.GetVariable<TeammateFormationModeVariable>("FormationMode").Value = TeammateFormationMode.Echelon;
    }

    private void OnNoneFormation()
    {
        blackboard.GetVariable<TeammateFormationModeVariable>("FormationMode").Value = TeammateFormationMode.None;
    }

    private void OnShootReceived()
    {
        var targetVar = blackboard.GetVariable<TransformVariable>("LaserTarget");

        if (targetVar != null && targetVar.Value != null)
        {
            Vector3 direction = targetVar.Value.position - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        if (laserShooter != null)
        {
            laserShooter.SendMessage("Shoot");
        }
    }
}
