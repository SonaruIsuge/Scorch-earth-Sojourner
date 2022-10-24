using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPropState
{
    Player player { get; }
    
    
    void EnterState(MoreInfo info = MoreInfo.None);
    void StayState();
    void ExitState();
}
