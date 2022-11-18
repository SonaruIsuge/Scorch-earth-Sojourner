
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
    }

    public void StayState()
    {
        input.ReadInput();

        if (input.Left || input.Right)
        {
            albumBook.SetCurrentChoosePhoto(input.Left, input.Right);
        }
        
        if (enableAlbumStateChange != player.CommonInput.toggleAlbum)
        {
            albumBook.EnableProp(false);
        }
        
        
        // Change state
        
        if (!albumBook.enabled)
        {
            player.ChangePropState(UsingProp.None);
        }

        if (!albumBook)
        {
            player.ChangePropState(UsingProp.None);
        }
        

        if (input.Submit)
        {
            
        }
    }

    public void ExitState()
    {
        input?.Unregister();
        albumBook.EnableProp(false);
    }
}
