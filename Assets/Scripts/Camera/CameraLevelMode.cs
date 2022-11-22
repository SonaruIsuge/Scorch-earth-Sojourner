using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLevelMode : MonoBehaviour
{
    private Camera targetCamera;
    [SerializeField] private Transform LevelCenter;
    [SerializeField] private Texture FullRoom;


    private void Awake()
    {
        targetCamera = GetComponent<Camera>();
    }
    
    
    public void ChangeCurrentRoom()
    {
        targetCamera.orthographicSize = FullRoom.height / 100.0f / 2;
        
    }
}
