using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUIControl : MonoBehaviour
{
    [SerializeField] private Button StartGameBtn;


    private void OnEnable()
    {
        StartGameBtn.onClick.AddListener(StartGame);
    }


    private void OnDisable()
    {
        StartGameBtn.onClick.RemoveAllListeners();
    }


    private void StartGame()
    {
        GameFlowHandler.Instance.LoadScene(SceneIndex.Level, new LevelData());
    }
}
