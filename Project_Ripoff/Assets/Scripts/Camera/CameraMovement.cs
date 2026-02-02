using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CameraMovement : MonoBehaviour
{
    private Messaging_Service messaging_Service;
    private CinemachineCamera virtualCamera;
    private CinemachineOrbitalFollow orbitalFollow;
    private GameObject mapCenter;
    private CinemachineInputAxisController inputAxisController;
    private Vector3 currentVelocity = Vector3.zero;

    [Header("Speed")]
    [SerializeField] private float moveSpeed = 32f;
    [SerializeField] private float zoomSpeed = 50f;

    [Header("Radius")]
    [SerializeField] private float maxRadius = 100f;
    [SerializeField] private float minRadius = 5f;

    private void Awake()
    {
        messaging_Service = FindFirstObjectByType<Messaging_Service>();
        virtualCamera = FindFirstObjectByType<CinemachineCamera>();
        orbitalFollow = virtualCamera.GetComponent<CinemachineOrbitalFollow>();
        mapCenter = GameObject.FindGameObjectWithTag("MapCenter");
        inputAxisController = FindFirstObjectByType<CinemachineInputAxisController>();
    }

    private void OnEnable()
    {
        messaging_Service.moveCameraTarget += MoveCameraHorizontal;
        messaging_Service.zoomCamera += ZoomCamera;
        messaging_Service.rotateCameraWithMouse += RotateCameraWithMouse;
    }

    private void OnDisable()
    {
        messaging_Service.moveCameraTarget -= MoveCameraHorizontal;
        messaging_Service.zoomCamera -= ZoomCamera;
        messaging_Service.rotateCameraWithMouse -= RotateCameraWithMouse;
    }

    public void MoveCameraHorizontal(Vector3 input)
    {
        if (mapCenter == null || virtualCamera == null) { return; }

        // Input in Kamerarichtung umrechnen
        Vector3 forward = virtualCamera.transform.forward;
        Vector3 right = virtualCamera.transform.right;
        forward.y = 0f; right.y = 0f;
        forward.Normalize(); right.Normalize();

        // Wenn kein Input -> sofort stoppen
        if (input.sqrMagnitude < 0.0001f)
        {
            currentVelocity = Vector3.zero;
            return; // kein Move angewendet
        }

        Vector3 moveDir = (forward * input.z + right * input.x).normalized;
        currentVelocity = moveDir * moveSpeed;
        mapCenter.transform.position += currentVelocity * Time.deltaTime;
    }

    public void ZoomCamera(float scroll)
    {
        if (orbitalFollow == null) { return; }

        orbitalFollow.Radius -= scroll * zoomSpeed;
        orbitalFollow.Radius = Mathf.Clamp(orbitalFollow.Radius, 5f, maxRadius);
    }

    public void RotateCameraWithMouse(bool middleMouseButtonPressed)
    {
        if (inputAxisController == null) { return; }
        inputAxisController.enabled = middleMouseButtonPressed; 
    }
    
}
