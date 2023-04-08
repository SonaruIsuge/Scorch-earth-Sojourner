using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeKeyEventReaction : MonoBehaviour, IInteractReact
{
    [SerializeField] private KeyEvent keyEvent;
    private KeyEventManager keyEventManager;


    private void Awake()
    {
        keyEventManager = FindObjectOfType<KeyEventManager>();
    }

    public void React(Player player)
    {
        //GameFlowHandler.Instance.SetKeyEventClear(keyEvent);
        if(!keyEventManager) return;
        keyEventManager.SetKeyEventState(keyEvent, true);
        gameObject.SetActive(false);
    }
}
