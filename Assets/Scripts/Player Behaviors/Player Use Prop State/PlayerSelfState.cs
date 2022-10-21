
public class PlayerSelfState : IPropState
{
    public Player player { get; private set; }

    private bool enableAlbumStateChange;
    

    public PlayerSelfState(Player owner)
    {
        player = owner;
    }
    
    
    public void EnterState()
    {
        player.PlayerMove.EnableMove(true);
        player.InteractHandler.EnableInteract(true);
        
        enableAlbumStateChange = player.PlayerInput.toggleAlbum;
    }

    public void StayState()
    {
        player.PlayerMove.UpdateMove(player.PlayerInput.horizontal, player.PlayerInput.vertical);
        player.InteractHandler.UpdateSelect();
        
        if (player.PlayerInput.interact)
        {
            player.InteractHandler.Interact();
        }
        
        
        // Check change state
        if (player.MemoryCamera && player.PlayerInput.enablePhoto)
        {
            player.ChangePropState(UsingProp.MemoryCamera);
        }

        if (player.AlbumBook && enableAlbumStateChange != player.PlayerInput.toggleAlbum)
        {
            player.ChangePropState(UsingProp.AlbumBook);
        }
    }

    public void ExitState()
    {
        player.PlayerMove.EnableMove(false);
        player.InteractHandler.EnableInteract(false);
    }
}
