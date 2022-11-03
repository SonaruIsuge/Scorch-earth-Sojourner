using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class R_Lamp : CameraRecordableBehaviour
{
    private Light2D lampLight;


    private void Awake()
    {
        lampLight = GetComponentInChildren<Light2D>();
    }
    
    
    public override void CameraHit()
    {
        base.CameraHit();
    }

    public override void ItemUse()
    {
        base.ItemUse();
    }
}
