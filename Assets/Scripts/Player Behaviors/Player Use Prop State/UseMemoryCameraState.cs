
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
    
    
    public void EnterState(MoreInfo info)
    {
        player.EnableInputType(InputType.MemoryCamera);
        input = player.CurrentInput as MemoryCameraInput;
        input?.Register();
        
        memoryCamera.EnableProp(true);
    }

    public void StayState()
    {
        input.ReadInput();
        
        memoryCamera.MoveCamera(input.controlFrameArea.x, input.controlFrameArea.y);
        if(input.takePhoto) memoryCamera.TakePhoto();
        
        
        // Check change state
        if (!player.CommonInput.togglePhoto)
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
        input?.Unregister();
        memoryCamera.EnableProp(false);
    }
}
