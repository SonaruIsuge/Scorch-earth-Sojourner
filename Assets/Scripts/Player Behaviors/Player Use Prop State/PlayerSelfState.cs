
using UnityEngine;

public class PlayerSelfState : IPropState
{
    public Player player { get; private set; }

    private PlayerInputSystemInput input;

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
    }

    public void StayState()
    {

        player.PlayerMove.UpdateMove(input.horizontal, input.vertical);
        player.InteractHandler.UpdateSelect();

        if (input.openMap)
        {
            player.ToggleMap();
        }
        
        if (input.interact)
        {
            player.InteractHandler.Interact();
        }

        if (player.AlbumBook && player.CommonInput.toggleAlbum)
        {
            player.AlbumBook.EnableProp(true);
        }

        if (player.MemoryCamera && player.CommonInput.togglePhoto)
        {
            player.MemoryCamera.EnableProp(true);
        }
        
        
        // // Change state
        
        if (player.MemoryCamera.enabled)
        {
            player.ChangePropState(UsingProp.MemoryCamera);
        }

        if (player.AlbumBook.enabled)
        {
            player.ChangePropState(UsingProp.AlbumBook);
        }

        if (player.InteractHandler.currentSelectObj is M_ProjectMachine &&
            player.InteractHandler.currentSelectObj.isInteract)
        {
            player.ChangePropState(UsingProp.Projector);
        }
    }

    public void ExitState()
    {
        input?.Unregister();
        player.PlayerMove.EnableMove(false);
        player.InteractHandler.EnableInteract(false);
    }
}
