

using UnityEngine;

public class CommonInput : IInput
{
    private InputControl inputControl;

    public bool togglePhoto => inputControl.CommonInput.ToggleCamera.WasPressedThisFrame();
    public bool toggleAlbum => inputControl.CommonInput.ToggleAlbum.WasPressedThisFrame();
    public bool toggleSetting => inputControl.CommonInput.ToggleSetting.WasPressedThisFrame();
    

    public CommonInput(InputControl input)
    {
        inputControl = input;
        inputControl.CommonInput.Enable();
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
        
    }
}
