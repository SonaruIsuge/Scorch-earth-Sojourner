using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapControl : MonoBehaviour
{
    [SerializeField] private Transform worldMapCamera;

    public float Speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MapCameraMove(Vector2 machineDirection)
    {
        worldMapCamera.Translate(machineDirection * (Speed * Time.deltaTime));
    }
}
