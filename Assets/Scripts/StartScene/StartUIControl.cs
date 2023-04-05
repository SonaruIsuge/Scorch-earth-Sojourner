using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartUIControl : MonoBehaviour
{
    [SerializeField] private Button startGameBtn;
    [SerializeField] private Button exitGameBtn;
    [SerializeField] private Image transitionImage;
    private EventSystem EventSystem => EventSystem.current;
    private bool isStartProgress;
    

    private void Awake()
    {
        EventSystem.SetSelectedGameObject(startGameBtn.gameObject);
        isStartProgress = false;
    }


    private void OnEnable()
    {
        startGameBtn.onClick.AddListener(StartGame);
        exitGameBtn.onClick.AddListener(ExitGame);
    }


    private void OnDisable()
    {
        startGameBtn.onClick.RemoveAllListeners();
        exitGameBtn.onClick.RemoveAllListeners();
    }


    private async void StartGame()
    {
        if(isStartProgress) return;
        isStartProgress = true;

        var color = transitionImage.color;
        color.a = 1;
        transitionImage.DOColor(color, 1f);

        await Task.Delay(1000);
        
        GameFlowHandler.Instance.LoadScene(SceneIndex.Level, new LevelData());
    }


    private void ExitGame()
    {
        Application.Quit();
    }
}
