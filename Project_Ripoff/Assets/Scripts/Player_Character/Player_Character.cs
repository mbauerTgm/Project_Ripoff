using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * Diese Klasse stellt Funktionalität zur verwendung des Player Characters bereit.
 * @author Maximilian Bauer
 * @version 0.3
 */
public class Player_Character : MonoBehaviour
{
    private Messaging_Service messaging_Service;
    private NavMeshAgent agent;
    private Queue<Vector3> movePointsQueue;
    private bool agentMoving;
    private bool showQueue = false;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
    }

    private void OnEnable()
    {
        messaging_Service.playerMoveEvent += MovePlayer;
        messaging_Service.playerMoveShiftEvent += AddMovePointToQueue;
        messaging_Service.showPlayerQueue += SetShowQueue;
        messaging_Service.getPlayerPosition += GetPlayerPosition;
    }

    private void OnDisable()
    {
        messaging_Service.playerMoveEvent -= MovePlayer;
        messaging_Service.playerMoveShiftEvent -= AddMovePointToQueue;
        messaging_Service.showPlayerQueue -= SetShowQueue;
        messaging_Service.getPlayerPosition -= GetPlayerPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agentMoving = false;
        movePointsQueue = new Queue<Vector3>();

        // LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 0;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if(movePointsQueue.Count > 0)
                    {
                        MovePlayerToNextQueuePoint();
                    } 
                    else
                    {
                        agentMoving = false;
                    }
                }
            }
        }

        if(showQueue)
        {
            VisualizeQueue();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    /**
     * Wenn der Spieler nur Linksklick drückt, sich bewegen und die Queue überschreiben will
     */
    private void MovePlayer(Vector3 movePos)
    {
        movePointsQueue?.Clear();
        agent?.SetDestination(movePos);
        agentMoving = true;
    }

    private void MovePlayerToNextQueuePoint()
    {
        Vector3 movePos = movePointsQueue.Dequeue();
        agent?.SetDestination(movePos);
        agentMoving = true;
    }

    private void AddMovePointToQueue(Vector3 movePoint)
    {
        movePointsQueue?.Enqueue(movePoint);

        if(!agentMoving)
        {
            MovePlayerToNextQueuePoint();
        }
    } 

    private void SetShowQueue(bool showQueue)
    {
        this.showQueue = showQueue;
    }

    private void VisualizeQueue()
    {
        if (agent == null || movePointsQueue == null) return;

        // Sichtbarkeit je nach Einstellung
        if (!showQueue)
        {
            lineRenderer.enabled = false;
            return;
        }

        lineRenderer.enabled = true;

        List<Vector3> points = new List<Vector3>();

        if (agent.hasPath && agent.path.corners.Length > 0)
        {
            points.AddRange(agent.path.corners);
        }
        else
        {
            points.Add(transform.position);
        }

        foreach (Vector3 queuedPoint in movePointsQueue)
        {
            points.Add(queuedPoint);
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision == null) return;

        if (collision.gameObject.CompareTag("LaserShot"))
        {
            messaging_Service.playSFXEvent?.Invoke("CharacterImpact", transform.position);
            Debug.Log("Bitte hier VFX hinzufügen @Viktor! LG & Danke");
            PlayerDying();
        }
    }

    public void PlayerDying()
    {
        // Dying Animation
        messaging_Service.playSFXEvent("PlayerDying", transform.position);

    }

    public Vector3 GetPlayerPosition() {  return transform.position; }

}
