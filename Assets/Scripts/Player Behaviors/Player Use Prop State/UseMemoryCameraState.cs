
public class UseMemoryCameraState : IPropState
{
    public Player player { get; private set; }

    private MemoryCamera memoryCamera;



    public UseMemoryCameraState(Player owner)
    {
        player = owner;
        memoryCamera = owner.MemoryCamera;
    }
    
    
    public void EnterState()
    {
        memoryCamera.EnableProp(true);
    }

    public void StayState()
    {
        memoryCamera.MoveCamera(player.PlayerInput.controlFrameArea.x, player.PlayerInput.controlFrameArea.y);
        if(player.PlayerInput.takePhoto) memoryCamera.TakePhoto();
        
        
        // Check change state
        if (!player.PlayerInput.enablePhoto)
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
        memoryCamera.EnableProp(false);
    }
}
