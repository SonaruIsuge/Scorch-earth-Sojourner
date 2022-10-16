
public class UseAlbumBookState : IPropState
{
    public Player player { get; private set; }

    private AlbumBook albumBook;


    public UseAlbumBookState(Player owner)
    {
        player = owner;
        albumBook = owner.AlbumBook;
    }
    
    
    public void EnterState()
    {
        albumBook.EnableProp(true);
    }

    public void StayState()
    {
        
        
        // Check change state
        
    }

    public void ExitState()
    {
        albumBook.EnableProp(false);
    }
}
