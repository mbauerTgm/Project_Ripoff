using UnityEngine;
using MBT;

/**
 * @author Dominik Sandler
 * 
 * Definiert ein Enum mit dem man das aktuelle Verhalten von den Teammates festlegen kann. 
 */

public enum TeammateBehaviorMode
{
    Idle,
    FollowPlayer,
    MoveToPosition,
    StackUp,
    EnterAndClear,
    SupressPosition
}

/**
 * @author Dominik Sandler
 * 
 * Erstellt einen neuen Type an Variable die man dann im BehaviourTree nutzten kann um den Teammates zwischen den einzelnen States zu navigieren
 * 
 */
[AddComponentMenu("")]
public class TeammateBehaviorModeVariable : Variable<TeammateBehaviorMode>
{
    protected override bool ValueEquals(TeammateBehaviorMode a, TeammateBehaviorMode b)
    {
        return a == b;
    }
}

[System.Serializable]
public class TeammateBehaviorModeReference: VariableReference<TeammateBehaviorModeVariable, TeammateBehaviorMode>
{
    public TeammateBehaviorModeReference(VarRefMode mode = VarRefMode.DisableConstant)
    {
    }

    public TeammateBehaviorModeReference(TeammateBehaviorMode behaviorMode)
    {
        useConstant = true;
        constantValue = behaviorMode;
    }

        public TeammateBehaviorMode Value
        {
            get
            {
                if (useConstant)
                    return constantValue;
                if (this.GetVariable() == null)
                    return default;
                return GetVariable().Value;
            }
            set
            {
                if (useConstant)
                    constantValue = value;
                else if (this.GetVariable() != null)
                    GetVariable().Value = value;
            }
        }
}

