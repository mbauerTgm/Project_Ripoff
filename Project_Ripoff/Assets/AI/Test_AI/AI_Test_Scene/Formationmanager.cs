using UnityEngine;
using MBT;

public class FormationManager : MonoBehaviour
{
    public Transform leaderTransform;
    public float spacing = 2f;

    public Vector3 GetFormationPosition(int memberIndex, TeammateFormationMode mode)
    {
        Vector3 leaderPos = leaderTransform.position;
        Vector3 leaderForward = leaderTransform.forward;
        Vector3 leaderRight = leaderTransform.right;

        int rank = (memberIndex / 2) + 1;
        float side = (memberIndex % 2 == 0) ? 1 : -1;

        switch (mode)
        {
            case TeammateFormationMode.Wedge:
                Debug.Log("FormationManager here ! | Wedge");
                return leaderPos - (leaderForward * rank * spacing) + (leaderRight * side * rank * spacing);

            case TeammateFormationMode.Line:
                Debug.Log("FormationManager here ! | Line");
                float backOffset = spacing;
                return leaderPos - (leaderForward * backOffset) + (leaderRight * side * rank * spacing);

            default:
                Debug.Log("FormationManager here ! | default");
                return leaderPos - (leaderForward * spacing);
        }
    }

    private void OnDrawGizmos()
    {
        if (leaderTransform == null) return;

        for (int i = 0; i < 4; i++)
        {
            Gizmos.color = Color.cyan;
            Vector3 posWedge = GetFormationPosition(i, TeammateFormationMode.Wedge);
            Gizmos.DrawSphere(posWedge, 0.3f);

            Gizmos.color = Color.yellow;
            Vector3 posLine = GetFormationPosition(i, TeammateFormationMode.Line);
            Gizmos.DrawWireSphere(posLine, 0.3f);
        }
    }
}