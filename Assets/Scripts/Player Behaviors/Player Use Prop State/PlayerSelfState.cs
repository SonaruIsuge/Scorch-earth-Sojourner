
public class PlayerSelfState : IPropState
{
    public Player player { get; private set; }


    public PlayerSelfState(Player owner)
    {
        player = owner;
    }
    
    
    public void EnterState()
    {
        player.PlayerMove.EnableMove(true);
        player.InteractHandler.EnableInteract(true);
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
        if (player.PlayerInput.enablePhoto)
        {
            player.ChangePropState(UsingProp.MemoryCamera);
        }
    }

    public void ExitState()
    {
        player.PlayerMove.EnableMove(false);
        player.InteractHandler.EnableInteract(false);
    }
}
