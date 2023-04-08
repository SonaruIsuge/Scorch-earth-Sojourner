

using UnityEngine;
using UnityEngine.InputSystem;

public class AlbumInput : IInput
{
    private InputControl inputControl;

    public bool Up => inputControl.AlbumBook.Up.WasPressedThisFrame();
    public bool Down => inputControl.AlbumBook.Down.WasPressedThisFrame();
    public bool Left => inputControl.AlbumBook.Left.WasPressedThisFrame();
    public bool Right => inputControl.AlbumBook.Right.WasPressedThisFrame();
    public bool LeftPage => inputControl.AlbumBook.LastPage.WasPressedThisFrame();
    public bool RightPage => inputControl.AlbumBook.NextPage.WasPressedThisFrame();
    public bool Submit => inputControl.AlbumBook.Submit.WasPressedThisFrame();

    private Vector2 choosePhotoValue;
    private float choosePageValue;
    
    
    public AlbumInput(InputControl input)
    {
        inputControl = input;
        inputControl.AlbumBook.Enable();
    }


    public void EnableInput(bool enable)
    {
        if(enable) inputControl.AlbumBook.Enable();
        else inputControl.AlbumBook.Disable();
    }

    public void Register()
    {
        
    }

    public void Unregister()
    {
        
    }
}
