using UnityEngine;
using UnityEngine.AI;
using MBT;

namespace MBTExample
{
    [AddComponentMenu("")]
    [MBTNode("NPC/Move Navmesh Agent Smart")]
    /**
     * @author Dominik Sandler
     * 
     * Selbst implementierte Node damit die Teammates dem Player folgen.
     * Im vergleich zur Standard Node beleibt dieser Node in RUNNING State auch wenn er in die Naehe des Spieler gekommen ist.
     * Der Unterschied ist das nur der BehaviourType der im Blackboard geaendert eird eintscheided was das Verhalten ist und was passiert.
     * Mann will das der Teammate weiterhin folgt auch wenn er in der Nähe ist, sodass wenn der Spieler sich wieder bewegt das die Teammates sofort folgen.
     */
    public class MoveNavmeshAgentSmart : Leaf
    {
        public TransformReference destination;
        public NavMeshAgent agent;
        public float stopDistance = 2f;
        [Tooltip("How often target position should be updated")]
        public float updateInterval = 0.25f;
        private float time = 0f;


        /**
         * Definition von allem was zu beginn vor der Execution angewendet wird
         */
        public override void OnEnter()
        {
            time = 0f;
            agent.isStopped = false;
            agent.updateRotation = false;
            agent.SetDestination(destination.Value.position);
        }

        /**
         * Wie der Node selbst funktioniert in der PlayerPos verfolgung ueber denn Navmesh_agent
         */
        public override NodeResult Execute()
        {
            if (agent == null || destination.Value == null)
                return NodeResult.failure;

            time += Time.deltaTime;
            if (time > updateInterval)
            {
                time = 0f;
                agent.SetDestination(destination.Value.position);
            }

            if (agent.pathPending)
                return NodeResult.running;

            float dist = Vector3.Distance(agent.transform.position, destination.Value.position);

            if (dist > stopDistance)
            {
                if (agent.isStopped)
                    agent.isStopped = false;
                RotateTowardsTarget();
                return NodeResult.running;
            }
            else
            {
                if (!agent.isStopped)
                    agent.isStopped = true;
                RotateTowardsTarget();
                return NodeResult.running; // bleibt aktiv anstatt auf SUCCESSFULL zu gehen
            }
        }

        public override void OnExit()
        {
            if (agent != null)
                agent.isStopped = true;
        }

        public override bool IsValid()
        {
            return !(destination.isInvalid || agent == null);
        }
        private void RotateTowardsTarget()
        {
            Vector3 direction = destination.Value.position - agent.transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.001f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            agent.transform.rotation = Quaternion.Slerp(
                agent.transform.rotation,
                targetRotation,
                Time.deltaTime * 5f
            );
        }

    }
}
