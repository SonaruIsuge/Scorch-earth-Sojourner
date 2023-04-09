using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGoal : MonoBehaviour
{
    [SerializeField] private GameObject elevatorDoor;
    
    private Animator doorAni;
    private SpriteRenderer[] doorSprites;

    private bool underGameOverProgress;

    public event Action OnGameOverProgress;

    private void Awake()
    {
        doorAni = elevatorDoor.GetComponent<Animator>();
        doorSprites = elevatorDoor.GetComponentsInChildren<SpriteRenderer>();

        underGameOverProgress = false;
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if(underGameOverProgress) return;

        underGameOverProgress = true;
        OnGameOverProgress?.Invoke();
    }


    public void GameOverAni()
    {
        foreach (var doorSprite in doorSprites)
        {
            doorSprite.sortingLayerName = "W_Foreground";
        }
        doorAni.Play("ElevatorDoorClose");

        var info = doorAni.GetCurrentAnimatorStateInfo(0);
    }
}
