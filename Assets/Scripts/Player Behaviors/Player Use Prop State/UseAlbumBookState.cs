
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UseAlbumBookState : IPropState
{
    public Player player { get; private set; }

    private AlbumInput input;
    private AlbumBook albumBook;
    
    private bool enableAlbumStateChange;

    private M_Projector interactProjector;
    public FilePhotoData? currentSubmitData;
    private string detectSubmitName;

    private MoreInfo currentMoreInfo = MoreInfo.None;
    
    
    
    public UseAlbumBookState(Player owner)
    {
        player = owner;
        albumBook = owner.AlbumBook;
    }
    
    
    public void EnterState(MoreInfo info)
    {
        player.EnableInputType(InputType.AlbumBook);
        input = player.CurrentInput as AlbumInput;
        input?.Register();
        
        albumBook.EnableProp(true);
        
        enableAlbumStateChange = player.CommonInput.toggleAlbum;

        detectSubmitName = albumBook.BookView.SubmitPhotoName;
        currentMoreInfo = info;
        
        if (currentMoreInfo == MoreInfo.FromProjector) interactProjector = (M_Projector) player.InteractHandler.currentSelectObj;
        else interactProjector = null;
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
        
        if (interactProjector && albumBook.CurrentSubmitPhotoName != null)
        {
            albumBook.TryGetPhotoData(out var photo, out var data);
            interactProjector.SetProjectPhoto(photo, data);
            player.ChangePropState(UsingProp.None);
        }
    }

    public void ExitState()
    {
        input?.Unregister();
        
        if(interactProjector) interactProjector.OnChoosePhotoOver();
        currentMoreInfo = MoreInfo.None;
        
        albumBook.EnableProp(false);
    }
}
