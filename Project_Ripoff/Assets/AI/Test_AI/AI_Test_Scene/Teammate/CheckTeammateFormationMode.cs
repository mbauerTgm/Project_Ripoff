using UnityEngine;
using MBT;
using System.Collections.Generic;

[AddComponentMenu("")]
[MBTNode(name = "NPC/Check NPC Formation Mode")]

/**
 * @author Dominik Sandler
 * 
 * Selbst implementierte Node um im BehaviourTree zwischen den einzelnen Formations zu unterscheiden in der Logik
 * 
 */
public class CheckTeammateFormationMode : Condition
{ 
    public TeammateFormationModeReference formRef = new TeammateFormationModeReference(VarRefMode.DisableConstant);
    public List<TeammateFormationMode> expectedFormations = new List<TeammateFormationMode>() { TeammateFormationMode.None };
    public Abort abort;


    public override bool Check()
    {
        TeammateFormationMode currentFormation = formRef.Value;

        bool isInList = expectedFormations.Contains(currentFormation);

        return isInList;
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
            var variable = formRef.GetVariable();
            if (variable != null)
            {
                variable.AddListener(OnModeChange);
            }
            else
            {
                Debug.LogWarning($"{name}: formRef.GetVariable() ist null – kein Listener registriert.");
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
            formRef.GetVariable().RemoveListener(OnModeChange);
        }
    }

    /**
     *  Methoden die von Condition verplichtet sind zu implementieren
     * 
     */
    private void OnModeChange(TeammateFormationMode oldValue, TeammateFormationMode newValue)
    {
        EvaluateConditionAndTryAbort(abort);
    }
}
