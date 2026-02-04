using UnityEngine;

/**
 * Bei Auslösung eines Bestimmten Events wird ein Laserbeam erstellt und abgefeuert.
 * @author Maximilian Bauer
 * @version 1.0.
 */
public class Laser_Shooter : MonoBehaviour
{

    private Messaging_Service messaging_Service;

    public enum Shooter_Type { PleaseSelect, Player, Teammate, Enemy };

    [SerializeField]
    private Shooter_Type characterType;
    public GameObject laserPrefab;
    public GameObject shotSpawnPoint;
    [SerializeField]
    private float force = 70f;

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
    }

    private void OnEnable()
    {
        switch (characterType)
        {
            case Shooter_Type.PleaseSelect:
                Debug.Log("Default Type Selected!");
                break;
            case Shooter_Type.Player:
                messaging_Service.fireLaserShotPlayer += Shoot;
                break;
            case Shooter_Type.Teammate:
                Debug.Log("Shooter Type TEAMMATE not implemented");
                break;
            case Shooter_Type.Enemy:
                Debug.Log("Shooter Type ENEMY not implemented");
                break;
        }
        
    }

    private void OnDisable()
    {
        messaging_Service.fireLaserShotPlayer -= Shoot;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(laserPrefab, shotSpawnPoint.transform.position, shotSpawnPoint.transform.rotation);

        messaging_Service.playSFXEvent?.Invoke("ShootLaser", gameObject.transform.position);
        Debug.Log("Bitte hier VFX hinzufügen @Viktor! LG & Danke");

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(shotSpawnPoint.transform.forward * force, ForceMode.Impulse);
    }
}
