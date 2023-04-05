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
    private LevelManager levelManager;

    
    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
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
        if(!levelManager) return;
        if(item.ItemData == targetProjectItem) levelManager.SetKeyEventState(keyEvent, true);
    }
}
