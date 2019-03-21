using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]

    public class CameraController : MonoBehaviour
    {
    public Transform targetToFollow;
    public Vector3 offset;
   
    public float pitch = 2f;

    public float zoomSpeed = 4f;

    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float currentZoom = 10f;

    public float yawSpeed = 100f;
    private float CurrentYaw = 0f;

    private void Update()
    {
     currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

       CurrentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime; 
    }


    private void LateUpdate()
    {
        transform.position = targetToFollow.position + offset * currentZoom;
        transform.LookAt(targetToFollow.position + Vector3.up * pitch);

        transform.RotateAround(targetToFollow.position, Vector3.up, CurrentYaw);
    }
}
