using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using SonaruUtilities;
using TMPro;
using UnityEngine;

public class DialogueHandler : TSingletonMonoBehaviour<DialogueHandler>
{
    [SerializeField] private RectTransform dialogueDisplayArea;
    private CanvasGroup dialogueCanvasGroup;
    [SerializeField] private TMP_Text dialogueText;

    private bool hasDialogueProgress;
    
    protected override void Awake()
    {
        base.Awake();
        dialogueCanvasGroup = dialogueDisplayArea.GetComponent<CanvasGroup>();
        hasDialogueProgress = false;
    }


    public void StartSentence(string sentence, float wordSpeed = 0.02f, float wordShowTime = 1.0f, float fadeTime = 1.0f)
    {
        if(hasDialogueProgress) StopAllCoroutines();
        
        StartCoroutine(PrintSentenceProgress(sentence, wordSpeed, wordShowTime, fadeTime));
    }


    private IEnumerator PrintSentenceProgress(string sentence, float wordSpeed, float wordShowTime, float fadeTime)
    {
        hasDialogueProgress = true;
        dialogueText.text = "";
        dialogueCanvasGroup.alpha = 1;
        
        // print words
        foreach (var word in sentence.ToCharArray())
        {
            dialogueText.text += word;
            yield return new WaitForSeconds(wordSpeed);
        }
        
        //wait seconds
        yield return new WaitForSeconds(wordShowTime);
        
        // fade out text display area
        while (dialogueCanvasGroup.alpha >= 0.05)
        {
            dialogueCanvasGroup.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
        dialogueCanvasGroup.alpha = 0;

        hasDialogueProgress = false;
    }
    
    
    // public async void StartSentence(string sentence, float wordSpeed = 0.02f, float wordShowTime = 1.0f, float fadeTime = 1.0f)
    // {
    //     hasDialogueProgress = true;
    //     
    //     dialogueCanvasGroup.alpha = 1;
    //
    //     await PrintGradual(sentence, wordSpeed);
    //     await Task.Delay((int)(wordShowTime * 1000));
    //     await FadeDialogue(fadeTime);
    //
    //     hasDialogueProgress = false;
    // }


    // private async UniTask PrintGradual(string sentence, float wordSpeed)
    // {
    //     dialogueText.text = "";
    //
    //     foreach (var word in sentence.ToCharArray())
    //     {
    //         dialogueText.text += word;
    //         await UniTask.Delay((int)(wordSpeed * 1000));
    //     }
    // }


    // private async UniTask FadeDialogue(float fadeTime)
    // {
    //     while (dialogueCanvasGroup.alpha >= 0.05)
    //     {
    //         dialogueCanvasGroup.alpha -= Time.deltaTime / fadeTime;
    //         await UniTask.Yield();
    //     }
    //
    //     dialogueCanvasGroup.alpha = 0;
    // }
}
