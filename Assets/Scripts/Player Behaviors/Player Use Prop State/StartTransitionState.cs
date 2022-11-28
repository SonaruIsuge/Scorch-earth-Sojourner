
using SonaruUtilities;
using Tools;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartTransitionState : IPropState
{
    public Player player { get; private set; }
    private Animator playerAni;
    private AnimatorStateInfo animatorStateInfo;

    private SimpleTimer timer;
    private bool isPlayingTransition;
    
    public StartTransitionState(Player owner)
    {
        player = owner;
        playerAni = player.GetComponentInChildren<Animator>();
    }
    
    
    public void EnterState()
    {
        isPlayingTransition = false;
        timer = new SimpleTimer(1);
        timer.Pause();
    }

    public void StayState()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame && !isPlayingTransition)
        {
            isPlayingTransition = true;
            player.DelayDo(PlayTransition, .5f);
        }
        
        
        if(timer.IsFinish) player.ChangePropState(UsingProp.None);
    }

    public void ExitState()
    {
        
    }


    private void PlayTransition()
    {
        playerAni.SetTrigger(AnimatorParam.WakeUp);
        animatorStateInfo = playerAni.GetCurrentAnimatorStateInfo(0);
        
        timer.Reset(animatorStateInfo.length);
        timer.Resume();
    }
}
