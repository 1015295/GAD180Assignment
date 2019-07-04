using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public List<Transform> targets; // Set targets in the inspector (eg. Player1, Player 2, Doll)
    private Camera cam;

    // Oscar, this needs to be set and adjusted individually in the inspector for every level.
    // This is the maximum and minimum zoom limits for the camera.
    public float minZoom;
    public float maxZoom;
    public float zoomLimiter;
    
    public Vector3 offset; // For othographic, this value only needs to be < foreground position so we can see everything.

    // The speed the camera is capable of moving.
    private Vector3 velocity;
    public float smoothTime = .5f;

    // Start is called before the first frame update
    void Start()
    {
        // Store our camera.
        cam = GetComponent<Camera>();
    }

    // Called after all Update() methodds are processed
    void LateUpdate()
    {
        if (targets.Count == 0)
        return;

        CameraZoom();
        CameraMove();  
    }

    void CameraZoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);

        // Comment or uncomment depending on the camera projection type.
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime); // Perspective Projection
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime); // Orthographic Projection
    }

    void CameraMove()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}