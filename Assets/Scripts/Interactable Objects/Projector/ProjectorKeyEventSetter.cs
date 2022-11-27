using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorKeyEventSetter : MonoBehaviour
{
    public void SetKeyEventClear(KeyEvent keyEvent)
    {
        GameFlowHandler.Instance.SetKeyEventClear(keyEvent);
    }
}
