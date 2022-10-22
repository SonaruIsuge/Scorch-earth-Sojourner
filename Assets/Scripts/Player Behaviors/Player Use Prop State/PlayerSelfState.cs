
public class PlayerSelfState : IPropState
{
    public Player player { get; private set; }

    private PlayerInputSystemInput input;
    private bool enableAlbumStateChange;
    

    public PlayerSelfState(Player owner)
    {
        player = owner;
    }
    
    
    public void EnterState()
    {
        player.EnableInputType(InputType.Player);
        input = player.CurrentInput as PlayerInputSystemInput;
        input?.Register();
        
        player.PlayerMove.EnableMove(true);
        player.InteractHandler.EnableInteract(true);
        
        enableAlbumStateChange = player.CommonInput.toggleAlbum;
    }

    public void StayState()
    {
        input.ReadInput();
        
        player.PlayerMove.UpdateMove(input.horizontal, input.vertical);
        player.InteractHandler.UpdateSelect();
        
        if (input.interact)
        {
            player.InteractHandler.Interact();
        }
        
        
        // Check change state
        if (player.MemoryCamera && player.CommonInput.togglePhoto)
        {
            player.ChangePropState(UsingProp.MemoryCamera);
        }

        if (player.AlbumBook && enableAlbumStateChange != player.CommonInput.toggleAlbum)
        {
            player.ChangePropState(UsingProp.AlbumBook);
        }
    }

    public void ExitState()
    {
        input?.Unregister();
        player.PlayerMove.EnableMove(false);
        player.InteractHandler.EnableInteract(false);
    }
}
