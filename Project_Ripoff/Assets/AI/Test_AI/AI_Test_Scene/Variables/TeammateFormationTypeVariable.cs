using UnityEngine;
using MBT;

/**
 * @author Dominik Sandler
 * 
 * Definiert ein Enum mit dem man die aktuelle Formation von den Teammates festlegen kann. 
 */
public enum TeammateFormationMode
    {
        None,
        Line,
        Wedge,
        Vee,
        File,
        Echelon
    }

/**
 * @author Dominik Sandler
 * 
 * Erstellt einen neuen Type an Variable die man dann im BehaviourTree nutzten kann um den Teammates zwischen den einzelnen States zu navigieren
 * 
 */
[AddComponentMenu("")]
public class TeammateFormationModeVariable : Variable<TeammateFormationMode>
{
    protected override bool ValueEquals(TeammateFormationMode a, TeammateFormationMode b)
    {
        return a == b;
    }
}


[System.Serializable]
public class TeammateFormationModeReference : VariableReference<TeammateFormationModeVariable, TeammateFormationMode>
{
    public TeammateFormationModeReference(VarRefMode mode = VarRefMode.DisableConstant)
    {
    }

    public TeammateFormationModeReference(TeammateFormationMode behaviorMode)
    {
        useConstant = true;
        constantValue = behaviorMode;
    }

    public TeammateFormationMode Value
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