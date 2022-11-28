using SonaruUtilities;
using UnityEngine;
using UnityEngine.Events;


public class SlimeEatMetalFeature : MonoBehaviour
{
    [SerializeField] private M_ProjectMachine detectMachine;
    [SerializeField] private float slimeNervousRange;
    
    [TextArea(3, 5)]
    [SerializeField] private string hintSentence;

    private R_Slime thisSlime;
    private R_Metal targetMetal;
    private Player player;
    private bool showHint;
    private SimpleTimer timer;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        thisSlime = GetComponent<R_Slime>();
        showHint = false;
        
        timer = new SimpleTimer(2);
        timer.Pause();
    }
    
    
    private void Update()
    {
        var detectItem = detectMachine.GetCurrentRecordItem();
        if (detectItem is R_Metal item)
        {
            targetMetal = item;
            TryGoToMetal();
        }

        
    }


    private void TryGoToMetal()
    {
        var distanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        // Player not in this room
        if (distanceFromPlayer >= slimeNervousRange)
        {
            timer.Resume();
            if(timer.IsFinish)
            {
                transform.position = targetMetal.transform.position + Vector3.right;
                // let slime escape from player
                thisSlime.SetDetectRange(5);
                if(!showHint) HintToPlayer();
                
                AudioHandler.Instance.SpawnAudio(AudioType.BedroomSound);
            }
        }
    }
    
    
    private void HintToPlayer()
    {
        DialogueHandler.Instance.StartSentence(hintSentence);
        showHint = true;
    }
    
    
    // register in slime escape event
    public void SlimeEatPowerRoomDoor() => GameFlowHandler.Instance.SetKeyEventClear(KeyEvent.DoorEatenBySlime);
}
