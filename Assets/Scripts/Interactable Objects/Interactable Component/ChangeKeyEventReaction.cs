using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeKeyEventReaction : MonoBehaviour, IInteractReact
{
    [SerializeField] private KeyEvent keyEvent;

    public void React(Player player)
    {
        GameFlowHandler.Instance.SetKeyEventClear(keyEvent);
        
        gameObject.SetActive(false);
    }
}
