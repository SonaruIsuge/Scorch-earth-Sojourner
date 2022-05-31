using System;
using TMPro;
using UnityEngine;

public class MachineControl : MonoBehaviour
{
    [SerializeField]private M_Forward forwardLever;
    [SerializeField]private M_TurnLeft leftLever;
    [SerializeField]private M_TurnRight rightLever;
    [SerializeField]private CoordinationManager coordinationManager;
    [SerializeField] private WindowViewControl viewControl; 

    [SerializeField] private Transform gpsPoint;
    private MachineShake shake;
    private float shakePauseTimer = 0;
    private float shakePauseTime = 1;

    public float ForwardSpeed;
    public float RotateAngle;
    
    // Machine transform data
    [SerializeField] private Vector2 machinePosInWorld;
    public Vector2 MachinePosInWorld => machinePosInWorld;
    
    private Vector2 machineForward;
    public Vector2 MachineForward => machineForward;
    
    private float currentRotateAngle;
    public float CurrentRotateAngle => currentRotateAngle;

    private readonly Vector2 ORIGIN_FORWARD = Vector2.up;

    void Awake()
    {
        machinePosInWorld = Vector2.zero;
        machineForward = Vector2.up;
        currentRotateAngle = 0;

        if (Camera.main != null) shake = transform.GetComponentInChildren<MachineShake>();
    }

    void Update()
    {
        UpdateMove();
        coordinationManager.SetCoordination(machinePosInWorld);
    }

    void UpdateMove()
    {
        var rotatePerFrame = RotateAngle * Time.deltaTime;
        if (leftLever.LeftEnable != rightLever.RightEnable)
            currentRotateAngle += leftLever.LeftEnable ? rotatePerFrame : -rotatePerFrame;

        if (currentRotateAngle >= 360) currentRotateAngle -= 360;
        if (currentRotateAngle < 0) currentRotateAngle += 360;
        
        gpsPoint.rotation = Quaternion.Euler(0, 0, currentRotateAngle);

        //x' = cos(θ) * x - sin(θ) * y
        //y' = sin(θ) * x + cos(θ) * y
        var currentRotateRadius = currentRotateAngle * Mathf.Deg2Rad;
        var cos = Mathf.Cos(currentRotateRadius);
        var sin = Mathf.Sin(currentRotateRadius);
        machineForward = new Vector2(ORIGIN_FORWARD.x * cos - ORIGIN_FORWARD.y * sin, ORIGIN_FORWARD.x * sin + ORIGIN_FORWARD.y * cos);

        if (forwardLever.ForwardEnable)
        {
            machinePosInWorld += machineForward * (ForwardSpeed * Time.deltaTime);
            viewControl.UpdateViews();

            shakePauseTimer += Time.deltaTime;
            if (shakePauseTimer >= shakePauseTime)
            {
                StartCoroutine(shake.Shake(0.1f, 0.06f));
                shakePauseTimer -= shakePauseTime;
            }
        }
        else shakePauseTimer = 0;
    }
}
