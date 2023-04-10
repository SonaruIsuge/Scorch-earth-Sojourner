using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class TransitionUI : MonoBehaviour
{
    [SerializeField] private Image transitionImage;
    [SerializeField] private float fadeTime;

    private bool isFadeProgress;


    private void Awake()
    {
        isFadeProgress = false;
        transitionImage.gameObject.SetActive(false);
    }


    public async Task FadeIn(Action onComplete = null)
    {
        if(isFadeProgress) return;
        isFadeProgress = true;
        
        transitionImage.gameObject.SetActive(true);
        var imgColor = transitionImage.color;
        imgColor.a = 0;
        transitionImage.color = imgColor;
        
        imgColor.a = 1;
        transitionImage.DOColor(imgColor, fadeTime);
        
        await Task.Delay((int)(fadeTime * 1000));
        
        onComplete?.Invoke();
        isFadeProgress = false;
    }


    public async Task FadeOut(Action onComplete = null)
    {
        if(isFadeProgress) return;
        isFadeProgress = true;
        
        transitionImage.gameObject.SetActive(true);
        var imgColor = transitionImage.color;
        imgColor.a = 1;
        transitionImage.color = imgColor;
        
        imgColor.a = 0;
        transitionImage.DOColor(imgColor, fadeTime);
        
        await Task.Delay((int)(fadeTime * 1000));
        
        onComplete?.Invoke();
        isFadeProgress = false;
        transitionImage.gameObject.SetActive(false);
    }
}
