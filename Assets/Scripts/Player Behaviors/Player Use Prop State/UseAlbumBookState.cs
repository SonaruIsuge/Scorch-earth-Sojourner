
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UseAlbumBookState : IPropState
{
    public Player player { get; private set; }

    private AlbumInput input;
    private AlbumBook albumBook;
    
    private bool enableAlbumStateChange;

    
    public FilePhotoData? currentSubmitData;
    private string detectSubmitName;



    public UseAlbumBookState(Player owner)
    {
        player = owner;
        albumBook = owner.AlbumBook;
    }
    
    
    public void EnterState()
    {
        player.EnableInputType(InputType.AlbumBook);
        input = player.CurrentInput as AlbumInput;
        input?.Register();
        
        albumBook.EnableProp(true);
        
        enableAlbumStateChange = player.CommonInput.toggleAlbum;

        detectSubmitName = albumBook.CurrentSubmitPhotoName;
    }

    public void StayState()
    {
        input.ReadInput();

        if (input.LeftPage) albumBook.SetCurrentPage(-1);
        if (input.RightPage) albumBook.SetCurrentPage(1);
        albumBook.SetCurrentChoosePhoto(input.Up, input.Down, input.Left, input.Right);
        
        
        // Check change state
        if (enableAlbumStateChange != player.CommonInput.toggleAlbum)
        {
            player.ChangePropState(UsingProp.None);
        }

        if (!albumBook)
        {
            player.ChangePropState(UsingProp.None);
        }
        

        if (input.Submit)
        {
            albumBook.SetSubmitPhoto();
        }
    }

    public void ExitState()
    {
        input?.Unregister();
        albumBook.EnableProp(false);
    }
}
