using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeKeyEventReaction : MonoBehaviour, IInteractReact
{
    [SerializeField] private KeyEvent keyEvent;
    private LevelManager levelManager;


    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void React(Player player)
    {
        //GameFlowHandler.Instance.SetKeyEventClear(keyEvent);
        if(!levelManager) return;
        levelManager.SetKeyEventState(keyEvent, true);
        gameObject.SetActive(false);
    }
}
