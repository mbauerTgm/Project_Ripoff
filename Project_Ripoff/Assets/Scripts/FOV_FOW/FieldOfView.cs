using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FieldOfView : MonoBehaviour
{
    [Header("View Cone Settings")]
    public float viewRadius = 10f;
    [Range(0, 360)] public float viewAngle = 90f;
    public int rayCount = 100;

    [Header("Inner Circle Settings")]
    [Range(0f, 1f)] public float innerRadiusRatio = 0.25f;
    public int circleRayCount = 50;

    [Header("Target Detection")]
    public LayerMask targetMask;       // Layer für Dinge, die versteckt/gezeigt werden sollen (z.B. "Walls", "Enemies")
    public List<Transform> visibleTargets = new List<Transform>();

    [Header("General")]
    public LayerMask obstacleMask;

    private Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        gameObject.layer = LayerMask.NameToLayer("FogClearing");
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }

    void LateUpdate()
    {
        GenerateMesh();
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void FindVisibleTargets()
    {
        foreach (Transform target in visibleTargets)
        {
            if (target != null)
                target.GetComponent<Renderer>().enabled = false;
        }
        visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            float distToTarget = Vector3.Distance(transform.position, target.position);
            bool inInnerCircle = distToTarget < (viewRadius * innerRadiusRatio);

            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2 || inInnerCircle)
            {
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);

                    Renderer rnd = target.GetComponent<Renderer>();
                    if (rnd != null) rnd.enabled = true;
                }
                else
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, dirToTarget, out hit, distToTarget))
                    {
                        if (hit.transform == target)
                        {
                            visibleTargets.Add(target);
                            if (target.GetComponent<Renderer>() != null)
                                target.GetComponent<Renderer>().enabled = true;
                        }
                    }
                }
            }
        }
    }

    void GenerateMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        // --- VIEW CONE (Sichtkegel) ---
        float currentAngle = -viewAngle / 2;
        float angleIncrease = viewAngle / rayCount;

        vertices.Add(Vector3.zero);

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            Vector3 dir = DirFromAngle(currentAngle, transform.eulerAngles.y);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
            {
                vertex = transform.InverseTransformPoint(hit.point);
            }
            else
            {
                vertex = transform.InverseTransformPoint(transform.position + dir * viewRadius);
            }

            vertices.Add(vertex);

            // Dreiecke generieren
            if (i < rayCount)
            {
                triangles.Add(0);               // Zentrum des Kegels
                triangles.Add(i + 1);           // Aktueller Punkt
                triangles.Add(i + 2);           // Nächster Punkt
            }

            currentAngle += angleIncrease;
        }

        // --- INNER CIRCLE (360 Grad Umgebung) ---
        int circleStartIndex = vertices.Count;

        float innerRadius = viewRadius * innerRadiusRatio;
        float circleAngleStep = 360f / circleRayCount;
        currentAngle = 0f;

        vertices.Add(Vector3.zero);

        for (int i = 0; i <= circleRayCount; i++)
        {
            Vector3 vertex;
            Vector3 dir = DirFromAngle(currentAngle, transform.eulerAngles.y);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir, out hit, innerRadius, obstacleMask))
            {
                vertex = transform.InverseTransformPoint(hit.point);
            }
            else
            {
                vertex = transform.InverseTransformPoint(transform.position + dir * innerRadius);
            }

            vertices.Add(vertex);

            if (i < circleRayCount)
            {
                triangles.Add(circleStartIndex);
                triangles.Add(circleStartIndex + i + 1);
                triangles.Add(circleStartIndex + i + 2);
            }

            currentAngle += circleAngleStep;
        }

        // --- MESH UPDATE ---
        mesh.Clear();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateNormals();
    }

    Vector3 DirFromAngle(float angle, float rotationY)
    {
        // angle += rotationY sorgt dafür, dass sich der Kegel mit dem Spieler dreht
        float rad = (angle + rotationY) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
    }
}