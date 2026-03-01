using UnityEngine;

/// <summary>
/// Diese Klasse kann einem Enemy gegeben werden um ihn in das FOV/FOW System zu integrieren 
/// und auszublenden, wenn der Spieler ihn nicht sieht.
/// <br></br>
/// <br></br>
/// <b>Author: Maximilian Bauer</b>
/// <br></br>
/// <b>Version 1.0.0</b>
/// </summary>
public class EnemyVisibility : MonoBehaviour
{
    [Header("Einstellungen")]
    public Transform player;
    public Light playerLight;
    public LayerMask obstacleMask;

    private Renderer enemyRenderer;
    private bool isVisible = false;

    void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        UpdateVisibility(false);
    }

    void Update()
    {
        if (player == null || playerLight == null) return;

        //Distanz prüfen
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > playerLight.range)
        {
            UpdateVisibility(false); //Zu weit weg
            return;
        }

        // Winkel prüfen (Ist der Gegner im Lichtkegel?)
        Vector3 directionToEnemy = (transform.position - player.position).normalized;
        float angle = Vector3.Angle(player.forward, directionToEnemy);

        //Der SpotAngle ist der gesamte Kegel, -> benötigen nur die Hälfte für die Prüfung nach links/rechts
        if (angle > playerLight.spotAngle / 2f)
        {
            UpdateVisibility(false); //Nicht im Lichtkegel
            return;
        }

        //Sichtlinie prüfen
        if (Physics.Linecast(player.position, transform.position, obstacleMask))
        {
            UpdateVisibility(false);
        }
        else
        {
            UpdateVisibility(true);
        }
    }

    void UpdateVisibility(bool visible)
    {
        if (isVisible != visible)
        {
            isVisible = visible;
            enemyRenderer.enabled = isVisible;
        }
    }
}