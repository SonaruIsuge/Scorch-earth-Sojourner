
using UnityEngine;

public class UseMemoryCameraState : IPropState
{
    public Player player { get; private set; }
    
    private MemoryCameraInput input;
    private MemoryCamera memoryCamera;

    public UseMemoryCameraState(Player owner)
    {
        player = owner;
        memoryCamera = owner.MemoryCamera;
    }
    
    
    public void EnterState()
    {
        player.EnableInputType(InputType.MemoryCamera);
        input = player.CurrentInput as MemoryCameraInput;
        input?.Register();
        
        memoryCamera.EnableProp(true);

        // not show some object 
        memoryCamera.WorldCamera.cullingMask &= ~(1 << Constant.NotShowInPhotoTakeModeLayer);
    }

    public void StayState()
    {
        input.ReadInput();
        
        memoryCamera.MoveCamera(input.controlFrameArea.x, input.controlFrameArea.y);
        if(input.takePhoto) memoryCamera.TakePhoto();

        if (player.CommonInput.togglePhoto)
        {
            memoryCamera.EnableProp(false);
        }
        
        
        // Check change state
        if (!memoryCamera.enabled)
        {
            player.ChangePropState(UsingProp.None);
        }

        if (!memoryCamera)
        {
            player.ChangePropState(UsingProp.None);
        }
    }

    public void ExitState()
    {
        memoryCamera.WorldCamera.cullingMask |= (1 << Constant.NotShowInPhotoTakeModeLayer);
        
        input?.Unregister();
        memoryCamera.EnableProp(false);
    }
}
