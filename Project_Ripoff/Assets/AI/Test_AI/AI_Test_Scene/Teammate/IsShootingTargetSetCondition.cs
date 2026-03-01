using MBT;
using UnityEngine;

[AddComponentMenu("")]
[MBTNode(name = "NPC/Check Is Shooting Target Set")]
public class IsShootingTargetSetCondition : Condition
{
    public Abort abort;

    public BoolReference isShootingTargetSetRef = new BoolReference(VarRefMode.DisableConstant);

    public override bool Check()
    {
        return isShootingTargetSetRef.Value == true;
    }

    public override void OnAllowInterrupt()
    {
        if (abort != Abort.None && !isShootingTargetSetRef.isConstant)
        {
            ObtainTreeSnapshot();
            isShootingTargetSetRef.GetVariable().AddListener(OnVariableChange);
        }
    }

    public override void OnDisallowInterrupt()
    {
        if (abort != Abort.None && !isShootingTargetSetRef.isConstant)
        {
            isShootingTargetSetRef.GetVariable().RemoveListener(OnVariableChange);
        }
    }

    private void OnVariableChange(bool oldValue, bool newValue)
    {
        EvaluateConditionAndTryAbort(abort);
    }
}