
using UnityEngine;

public class UseProjectorState : IPropState
{
    public Player player { get; private set; }
    private AlbumInput input;
    
    private M_Projector targetProjector;

    private bool exitInputState;
    
    
    public UseProjectorState(Player owner)
    {
        player = owner;
    }
    
    
    public void EnterState()
    {
        player.EnableInputType(InputType.AlbumBook);
        input = player.CurrentInput as AlbumInput;
        input?.Register();
        
        targetProjector = (M_Projector) player.InteractHandler.currentSelectObj;
        exitInputState = player.CommonInput.toggleSetting;
        
        if (!targetProjector) player.ChangePropState(UsingProp.None);
    }

    
    public void StayState()
    {
        input.ReadInput();
        
        if (input.Left) targetProjector.ChangeCurrentPhoto(-1);
        if (input.Right) targetProjector.ChangeCurrentPhoto(1);

        
        if (input.Submit)
        {
            targetProjector.SubmitPhoto();
        }
        
        // detect trigger exit input
        if (exitInputState != player.CommonInput.toggleSetting)
        {
            targetProjector.CancelChoosePhoto();
        }

        if (!targetProjector.isInteract)
        {
            player.ChangePropState(UsingProp.None);
            return;
        }


        if (!targetProjector) player.ChangePropState(UsingProp.None);
    }

    
    public void ExitState()
    {
        input?.Unregister();
        targetProjector = null;
    }
}
