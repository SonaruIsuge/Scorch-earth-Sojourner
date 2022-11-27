using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWorkReaction : MonoBehaviour, IInteractReact
{
    private Animator doorAni;
    [SerializeField] private TransportPortal doorEntry;
    [SerializeField] private string TriggerDoorParamName;
    [TextArea(3, 5)] [SerializeField] private string investigateSentence;
    
    private void Awake()
    {
        doorAni = GetComponent<Animator>();
        doorEntry.gameObject.SetActive(false);
    }
    
    
    public void React(Player player)
    {
        DialogueHandler.Instance.StartSentence(investigateSentence);
        doorAni.SetTrigger(TriggerDoorParamName);
        
        doorEntry.gameObject.SetActive(true);
    }
}
