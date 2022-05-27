using System;
using TMPro;
using UnityEngine;

public class MachineControl : MonoBehaviour
{
    [SerializeField]private M_Forward forwardLever;
    [SerializeField]private M_TurnLeft leftLever;
    [SerializeField]private M_TurnRight rightLever;
    
    [SerializeField]private TMP_Text xText;
    [SerializeField]private TMP_Text yText;
    [SerializeField]private TMP_Text speedText;

    [SerializeField] private Transform gpsPoint;

    public float ForwardSpeed;
    public float RotateAngle;

    private Vector2 machinePosInWorld;
    private Vector2 machineForward;
    private float currentRotateAngle;
    private readonly Vector2 ORIGIN_FORWARD = Vector2.up;

    void Awake()
    {
        machinePosInWorld = Vector2.zero;
        machineForward = Vector2.up;
        currentRotateAngle = 0;
    }

    void Update()
    {
        UpdateMove();
        xText.text = machinePosInWorld.x.ToString("F");
        yText.text = machinePosInWorld.y.ToString("F");
        speedText.text = currentRotateAngle.ToString("F") + "°";
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
        
        if (forwardLever.ForwardEnable) machinePosInWorld += machineForward * (ForwardSpeed * Time.deltaTime);
    }
}
