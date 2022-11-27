using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWorkReaction : MonoBehaviour, IInteractReact
{
    private Animator doorAni;
    [SerializeField] private TransportPortal doorEntry;
    [SerializeField] private string TriggerDoorParamName;
    [TextArea(3, 5)] [SerializeField] private string investigateSentence;

    private AnimatorStateInfo openDoorAniInfo;

    private void Awake()
    {
        doorAni = GetComponent<Animator>();
        doorEntry.gameObject.SetActive(false);
    }



    public void React(Player player)
    {
        DialogueHandler.Instance.StartSentence(investigateSentence);
        doorAni.SetTrigger(TriggerDoorParamName);

        StartCoroutine(EnableEntry());
    }


    private IEnumerator EnableEntry()
    {
        openDoorAniInfo = doorAni.GetCurrentAnimatorStateInfo(0);
        var currentTime = 0f;
        while (currentTime < openDoorAniInfo.length)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        doorEntry.gameObject.SetActive(true);
    }
    
}
