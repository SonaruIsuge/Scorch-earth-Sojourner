using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPropState
{
    Player player { get; }
    
    
    void EnterState();
    void StayState();
    void ExitState();
}
