using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_PowerSupplyRoomDoor : MonoBehaviour
{
    [SerializeField] private Sprite defaultDoor;
    [SerializeField] private Sprite meltDoor;
    [SerializeField] private TransportPortal doorEntry;
    
    [SerializeField] private KeyEvent keyEvent;
    private Collider2D doorCollider;
    private SpriteRenderer doorRenderer;
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        doorCollider = GetComponent<Collider2D>();
        doorRenderer = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        doorEntry.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        LevelManager.OnKeyEventClear += ChangeDoorStyle;
    }
    
    
    private void OnDisable()
    {
        LevelManager.OnKeyEventClear -= ChangeDoorStyle;
    }


    private void ChangeDoorStyle(KeyEvent clearEvent)
    {
        if(keyEvent != clearEvent) return;

        doorRenderer.sprite = meltDoor;
        doorCollider.isTrigger = true;
        doorEntry.gameObject.SetActive(true);
    }
    
}
