using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class V2FieldOfView : MonoBehaviour
{
    [Header("Einstellungen")]
    public float viewRadius = 8f; // Wie weit der Charakter sehen kann
    [Range(0, 360)]
    public float viewAngle = 90f; // Der Sichtwinkel (90 Grad = ein Viertelkreis)
    public int rayCount = 50;     // Wie detailliert der Kegel ist (mehr = flüssigere Rundungen, kostet minimal mehr Leistung)

    public LayerMask obstacleMask; // Hier im Inspector "Obstacle" auswählen!

    private Mesh mesh;

    void Start()
    {
        // Ein leeres Mesh erstellen und dem MeshFilter zuweisen
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void LateUpdate()
    {
        // Wir zeichnen den Kegel jeden Frame neu, am besten im LateUpdate (nachdem der Charakter sich bewegt hat)
        DrawFieldOfView();
    }

    void DrawFieldOfView()
    {
        float stepAngleSize = viewAngle / rayCount;
        Vector3[] vertices = new Vector3[rayCount + 2];
        int[] triangles = new int[rayCount * 3];

        // Der Startpunkt des Meshes ist genau beim Charakter (Lokal 0,0,0)
        vertices[0] = Vector3.zero;

        // Wir fangen links an zu scannen (die Hälfte des Winkels nach links)
        float currentAngle = -viewAngle / 2f;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 dir = DirFromAngle(currentAngle, false);
            RaycastHit hit;
            Vector3 vertexPos;

            // Wir schießen einen Raycast vom Charakter in die aktuelle Richtung
            if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
            {
                // Wenn wir eine Wand treffen, setzen wir den Punkt exakt dorthin.
                // transform.InverseTransformPoint wandelt die Weltkoordinate in die lokale Koordinate des Meshes um.
                vertexPos = transform.InverseTransformPoint(hit.point);
            }
            else
            {
                // Wenn wir nichts treffen, ist der Punkt bei der maximalen Sichtweite
                vertexPos = transform.InverseTransformDirection(dir) * viewRadius;
            }

            // Wir speichern den Punkt im Array (i + 1, weil Index 0 unser Ursprung ist)
            vertices[i + 1] = vertexPos;

            // Wir verbinden die Punkte zu Dreiecken (Triangles formen das 3D-Objekt)
            if (i < rayCount)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }

            currentAngle += stepAngleSize; // Zum nächsten Winkel weitergehen
        }

        // Das Mesh mit den neuen Daten füttern
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); // Wichtig, damit Licht korrekt berechnet wird (falls nötig)
    }

    // Hilfsfunktion: Wandelt einen Winkel in eine 3D-Richtung um (basierend auf der Y-Rotation)
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}