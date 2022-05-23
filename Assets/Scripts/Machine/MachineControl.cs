using System;
using UnityEngine;

public class MachineControl : MonoBehaviour
{
    [SerializeField]private GameObject XControlDisplay;
    [SerializeField]private GameObject YControlDisplay;
    [SerializeField]private GameObject SpeedControlDisplay;

    public float ForwardSpeed;
    public float RotateAngle;
    
    event Action OnXAxisChange;
    event Action OnYAxisChange;
    event Action OnSpeedChange;
    
    void Awake()
    {
        
    }
}
