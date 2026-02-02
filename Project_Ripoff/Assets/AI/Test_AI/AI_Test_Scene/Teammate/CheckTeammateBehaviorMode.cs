using UnityEngine;
using MBT;

[AddComponentMenu("")]
[MBTNode(name = "NPC/Check NPC Behavior Mode")]

/**
 * @author Dominik Sandler
 * 
 * Selbst implementierte Node um im BehaviourTree zwischen den einzelnen Behaviours zu unterscheiden in der Logik
 * 
 */
public class CheckTeammateBehaviorMode : Condition
{
    public TeammateBehaviorModeReference modeRef = new TeammateBehaviorModeReference(VarRefMode.DisableConstant);
    public TeammateBehaviorMode expectedMode = TeammateBehaviorMode.Idle;
    public Abort abort;

    /**
     * Checkt ob im aktuellen Node das erwartete Behaviour uebergeben wurde.
     * Vererbt aus der Conditon Klasse gibt es beim uebereinstimmen SUCCESS zurueck wenn nicht dann gibt es FAILURE zurueck.
     */
    public override bool Check()
    {
        return modeRef.Value == expectedMode;
    }

    /**
     *  Methoden die von Condition verplichtet sind zu implementieren
     * 
     */
    public override void OnAllowInterrupt()
    {
        if (abort != Abort.None)
        {
            ObtainTreeSnapshot();
            var variable = modeRef.GetVariable();
            if (variable != null)
            {
                variable.AddListener(OnModeChange);
            }
            else
            {
                Debug.LogWarning($"{name}: modeRef.GetVariable() ist null – kein Listener registriert.");
            }
        }
    }

    /**
     *  Methoden die von Condition verplichtet sind zu implementieren
     * 
     */
    public override void OnDisallowInterrupt()
    {
        if (abort != Abort.None)
        {
            modeRef.GetVariable().RemoveListener(OnModeChange);
        }
    }

    /**
     *  Methoden die von Condition verplichtet sind zu implementieren
     * 
     */
    private void OnModeChange(TeammateBehaviorMode oldValue, TeammateBehaviorMode newValue)
    {
        EvaluateConditionAndTryAbort(abort);
    }
}
