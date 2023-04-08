using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGoal : MonoBehaviour
{
    [SerializeField] private GameObject elevatorDoor;
    
    private Animator doorAni;
    private Player player;
    private SpriteRenderer[] doorSprites;
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        doorAni = elevatorDoor.GetComponent<Animator>();

        doorSprites = elevatorDoor.GetComponentsInChildren<SpriteRenderer>();
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Game Over
            player = other.GetComponent<Player>();
            player.PlayerMove.EnableMove(false);
            player.InteractHandler.EnableInteract(false);
            
            player.DelayDo(ClearProgress, .5f);
        }
    }


    private void ClearProgress()
    {
        foreach (var doorSprite in doorSprites)
        {
            doorSprite.sortingLayerName = "W_Foreground";
        }
        doorAni.Play("ElevatorDoorClose");

        var info = doorAni.GetCurrentAnimatorStateInfo(0);
        
        player.DelayDo(EnterGameOver, info.length);
        
    }


    private void EnterGameOver()
    {
        if(levelManager) levelManager.GameOver();
    }
}
