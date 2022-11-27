using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_FluorescentLight : CameraRecordableBehaviour
{
    [SerializeField] private KeyEvent keyEvent;
    public override void ItemUse(M_ProjectMachine machine)
    {
        base.ItemUse(machine);

        var keyEventSetter = machine.GetComponent<ProjectorKeyEventSetter>();
        
        if(keyEventSetter) keyEventSetter.SetKeyEventClear(keyEvent);
    }
    
}
