using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIControl : MonoBehaviour
{
    [SerializeField] private Button StartGameBtn;
    [SerializeField] private Button ExitGameBtn;
    

    private void OnEnable()
    {
        StartGameBtn.onClick.AddListener(StartGame);
        ExitGameBtn.onClick.AddListener(ExitGame);
    }


    private void OnDisable()
    {
        StartGameBtn.onClick.RemoveAllListeners();
        ExitGameBtn.onClick.RemoveAllListeners();
    }


    private void StartGame()
    {
        GameFlowHandler.Instance.LoadScene(SceneIndex.Level, new LevelData());
    }


    private void ExitGame()
    {
        Application.Quit();
    }
}
