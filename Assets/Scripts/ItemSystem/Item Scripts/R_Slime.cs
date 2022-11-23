using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Slime : CameraRecordableBehaviour
{
    [SerializeField] private float moveSpeed;


    private void Update()
    {
        
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
