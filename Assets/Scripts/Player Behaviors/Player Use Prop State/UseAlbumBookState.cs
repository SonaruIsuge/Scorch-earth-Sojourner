
public class UseAlbumBookState : IPropState
{
    public Player player { get; private set; }

    private bool enableAlbumStateChange;
    
    private AlbumBook albumBook;

    public FilePhotoData? currentData; 

    public UseAlbumBookState(Player owner)
    {
        player = owner;
        albumBook = owner.AlbumBook;
    }
    
    
    public void EnterState()
    {
        albumBook.EnableProp(true);
        currentData = null;
        enableAlbumStateChange = player.PlayerInput.toggleAlbum;
    }

    public void StayState()
    {
        
        
        // Check change state
        if (enableAlbumStateChange != player.PlayerInput.toggleAlbum)
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
        albumBook.EnableProp(false);
    }
}
