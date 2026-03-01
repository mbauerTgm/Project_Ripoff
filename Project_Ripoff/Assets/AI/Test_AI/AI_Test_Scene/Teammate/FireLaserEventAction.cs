using MBT;
using UnityEngine;

namespace MBTExample
{
    [AddComponentMenu("")]
    [MBTNode(name = "NPC/Fire Laser Event")]
    public class FireLaserEventAction : Leaf
    {
        private Messaging_Service messagingService;

        public override void OnEnter()
        {
            if (messagingService == null)
            {
                messagingService = Object.FindFirstObjectByType<Messaging_Service>();
            }
        }

        public override NodeResult Execute()
        {
            if (messagingService == null)
            {
                Debug.LogError("Messaging_Service nicht in der Szene gefunden!");
                return NodeResult.failure;
            }

            messagingService.fireLaserShotTeammate?.Invoke();
            return NodeResult.success;
        }
    }
}