
using System.Threading.Tasks;
using SonaruUtilities;
using Tools;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartTransitionState : IPropState
{
    public Player player { get; }
    private Animator playerAni;
    private AnimatorStateInfo animatorStateInfo;

    private bool isPlayingTransition;
    
    
    public StartTransitionState(Player owner)
    {
        player = owner;
        playerAni = player.GetComponentInChildren<Animator>();
    }
    
    
    public void EnterState()
    {
        isPlayingTransition = false;
    }
    

    public void StayState()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame && !isPlayingTransition)
        {
            DelayPlayerTransition();
        }
    }
    

    public void ExitState()
    {
        
    }


    private async void DelayPlayerTransition(float delaySec = 0)
    {
        if(isPlayingTransition) return;
        isPlayingTransition = true;

        await Task.Delay((int)(delaySec * 1000));
        playerAni.SetTrigger(AnimatorParam.WakeUp);
        animatorStateInfo = playerAni.GetCurrentAnimatorStateInfo(0);

        await Task.Delay((int)(animatorStateInfo.length * 1000));
        player.ChangePropState(UsingProp.None);
    }
}
