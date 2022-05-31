using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollowControl : MonoBehaviour
{
    [SerializeField]private Transform target;
    private Camera mainCamera;
    [SerializeField] private float xMinPos;
    [SerializeField] private float xMaxPos;
    [SerializeField] private float yMinPos;
    [SerializeField] private float yMaxPos;

    private float cameraHalfHeight;
    private float cameraHalfWidth;
    private bool hasTarget => target != null;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
        cameraHalfHeight = mainCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * ((float)Screen.width / Screen.height);
    }

    void Update()
    {
        if (!hasTarget) return;
        
        var cameraX = Mathf.Clamp(target.position.x, xMinPos + cameraHalfWidth, xMaxPos - cameraHalfWidth);
        var cameraY = Mathf.Clamp(target.position.y, yMinPos + cameraHalfHeight, yMaxPos - cameraHalfHeight);
        transform.position = new Vector3(cameraX, cameraY, transform.position.z);
    }
}
