using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(M_ProjectMachine))]
public class ProjectorKeyEventSetter : MonoBehaviour
{
    [SerializeField] private RecordableItem targetProjectItem;
    [SerializeField] private KeyEvent keyEvent;
    private M_ProjectMachine projector;
    private KeyEventManager keyEventManager;

    
    private void Awake()
    {
        keyEventManager = FindObjectOfType<KeyEventManager>();
        projector = GetComponent<M_ProjectMachine>();
    }


    private void OnEnable()
    {
        projector.OnItemUse += SetKeyEventClear;
    }


    private void OnDisable()
    {
        projector.OnItemUse -= SetKeyEventClear;
    }
    

    public void SetKeyEventClear(M_ProjectMachine proj, CameraRecordableBehaviour item)
    {
        //GameFlowHandler.Instance.SetKeyEventClear(keyEvent);
        if(!keyEventManager) return;
        if(item.ItemData == targetProjectItem) keyEventManager.SetKeyEventState(keyEvent, true);
    }
}
