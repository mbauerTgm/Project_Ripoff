using UnityEngine;

public class LaserCollisionHandler : MonoBehaviour
{
    public float lifeTime = 5f;

    void Start()
    {
        // Zerstört automatisch nach 5 Sekunden
        Destroy(gameObject, lifeTime);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Door"))
        {

            Messaging_Service messaging_Service = FindFirstObjectByType<Messaging_Service>();

            messaging_Service.playSFXEvent?.Invoke("LevelImpact", transform.position);
            Debug.Log("Bitte hier VFX hinzufügen @Viktor! LG & Danke");

            Destroy(gameObject);
        }
    }
}
