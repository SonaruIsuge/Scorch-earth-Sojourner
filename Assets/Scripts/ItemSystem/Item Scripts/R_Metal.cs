using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_Metal : CameraRecordableBehaviour
{
    [SerializeField] private R_Slime metalSlime;
    [SerializeField] private float detectMetalSlimeRange;

    private void Update()
    {
        
    }


    public override void ItemUse(M_ProjectMachine machine)
    {
        base.ItemUse(machine);
        
        // get metal slime instance
        
    }
}
