

using UnityEngine;

public class CommonInput : IInput
{
    private InputControl inputControl;
    
    public bool togglePhoto { get; private set; }
    public bool toggleAlbum { get; private set; }
    public bool toggleSetting { get; private set; }
    

    public CommonInput(InputControl input)
    {
        inputControl = input;
        inputControl.CommonInput.Enable();
        
        togglePhoto = false;
        toggleAlbum = false;
    }
    
    
    public void EnableInput(bool enable)
    {
        if(enable) inputControl.CommonInput.Enable();
        else inputControl.CommonInput.Disable();
    }

    public void Register()
    {
        
    }

    public void Unregister()
    {
        
    }

    public void ReadInput()
    {
        if (inputControl.CommonInput.ToggleCamera.WasPressedThisFrame()) togglePhoto = !togglePhoto;
        if (inputControl.CommonInput.ToggleAlbum.WasPressedThisFrame()) toggleAlbum = !toggleAlbum;
        if (inputControl.CommonInput.ToggleSetting.WasPressedThisFrame()) toggleSetting = !toggleSetting;
    }
}
