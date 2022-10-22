

using UnityEngine;
using UnityEngine.InputSystem;

public class AlbumInput : IInput
{
    private InputControl inputControl;

    public bool Up { get; private set; }
    public bool Down { get; private set; }
    public bool Left { get; private set; }
    public bool Right { get; private set; }
    public bool LeftPage { get; private set; }
    public bool RightPage { get; private set; }
    public bool Submit { get; private set; }

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


    public void ReadInput()
    {
        Up = inputControl.AlbumBook.Up.WasPressedThisFrame();
        Down = inputControl.AlbumBook.Down.WasPressedThisFrame();
        Left = inputControl.AlbumBook.Left.WasPressedThisFrame();
        Right = inputControl.AlbumBook.Right.WasPressedThisFrame();

        LeftPage = inputControl.AlbumBook.LastPage.WasPressedThisFrame();
        RightPage = inputControl.AlbumBook.NextPage.WasPressedThisFrame();

        Submit = inputControl.AlbumBook.Submit.WasPressedThisFrame();
    }
    
    
    
}
