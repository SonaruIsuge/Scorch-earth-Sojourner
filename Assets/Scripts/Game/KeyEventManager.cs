using System;
using System.Collections.Generic;
using UnityEngine;

public class KeyEventManager : MonoBehaviour
{
    [SerializeField] private List<KeyEvent> allKeyEventInLevel;
    private Dictionary<KeyEvent, bool> keyEventDict;

    public static event Action OnGameOver;
    public static event Action<KeyEvent> OnKeyEventClear;


    private void Awake()
    {
        keyEventDict = new Dictionary<KeyEvent, bool>();
        foreach (var key in allKeyEventInLevel) keyEventDict.Add(key, false);
    }


    public void SetKeyEventState(KeyEvent key, bool isClear)
    {
        if (!keyEventDict.ContainsKey(key)) return;
        if (keyEventDict[key] == isClear) return;
        
        keyEventDict[key] = isClear;
        OnKeyEventClear?.Invoke(key);
    }


    public bool CheckKeyEventState(KeyEvent key)
    {
        return keyEventDict.ContainsKey(key) && keyEventDict[key];
    }


    public void GameOver()
    {
        OnGameOver?.Invoke();
    }
}
