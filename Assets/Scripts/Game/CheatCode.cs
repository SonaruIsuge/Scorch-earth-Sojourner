using SonaruUtilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class CheatCode : MonoBehaviour
{
    
    private SimpleTimer cheatPressTimer;
    private bool cheatUsed;
    
    public UnityEvent OnCheatTrigger;

    private void Awake()
    {
        cheatPressTimer = new SimpleTimer(3);
        cheatPressTimer.Pause();
    }
    

    private void Update()
    {
        
        if (Keyboard.current.kKey.isPressed) cheatPressTimer.Resume();
        else cheatPressTimer.Reset();

        if (!cheatPressTimer.IsFinish) return;
        
        OnCheatTrigger?.Invoke();
        Debug.Log("Use Cheat Code!");
        cheatPressTimer.Pause();
    }
}
