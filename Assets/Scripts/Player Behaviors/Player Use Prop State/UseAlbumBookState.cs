
using UnityEngine;

public class UseAlbumBookState : IPropState
{
    public Player player { get; private set; }

    private AlbumInput input;
    private AlbumBook albumBook;
    
    private bool enableAlbumStateChange;
    //private readonly AlbumInput albumInput;

    public FilePhotoData? currentData; 

    
    public UseAlbumBookState(Player owner)
    {
        player = owner;
        albumBook = owner.AlbumBook;

        //albumInput = new AlbumInput(player.InputControl);
    }
    
    
    public void EnterState()
    {
        player.EnableInputType(InputType.AlbumBook);
        input = player.CurrentInput as AlbumInput;
        input?.Register();
        
        albumBook.EnableProp(true);
        currentData = null;
        enableAlbumStateChange = player.CommonInput.toggleAlbum;
    }

    public void StayState()
    {
        input.ReadInput();

        if (input.LeftPage) albumBook.SetCurrentPage(-1);
        if (input.RightPage) albumBook.SetCurrentPage(1);
        
        albumBook.SetCurrentPhoto(input.Up, input.Down, input.Left, input.Right);
        
        
        // Check change state
        if (enableAlbumStateChange != player.CommonInput.toggleAlbum)
        {
            player.ChangePropState(UsingProp.None);
        }

        if (!albumBook)
        {
            player.ChangePropState(UsingProp.None);
        }
    }

    public void ExitState()
    {
        input?.Unregister();
        albumBook.EnableProp(false);
    }
}
