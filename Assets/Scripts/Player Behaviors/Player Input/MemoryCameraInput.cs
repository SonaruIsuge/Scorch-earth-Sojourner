

using UnityEngine;
using UnityEngine.InputSystem;

public class MemoryCameraInput : IInput
{
    private InputControl inputControl;
    
    public Vector2 controlFrameArea { get; private set; }
    public bool takePhoto => inputControl.MemoryCamera.TakePicture.WasPressedThisFrame();

    public MemoryCameraInput(InputControl input)
    {
        inputControl = input;
        inputControl.MemoryCamera.Enable();
    }
    
    public void EnableInput(bool enable)
    {
        if(enable) inputControl.MemoryCamera.Enable();
        else inputControl.MemoryCamera.Disable();
    }

    public void Register()
    {
        inputControl.MemoryCamera.ControlPictureArea.performed += OnFrameControlPerformed;
        inputControl.MemoryCamera.ControlPictureArea.canceled += OnFrameControlCanceled;
    }

    
    public void Unregister()
    {
        // reset data value
        controlFrameArea = Vector2.zero;

        // unregister event
        inputControl.MemoryCamera.ControlPictureArea.performed -= OnFrameControlPerformed;
        inputControl.MemoryCamera.ControlPictureArea.canceled -= OnFrameControlCanceled;
    }


    private void OnFrameControlPerformed(InputAction.CallbackContext ctx)
    {
        controlFrameArea = ctx.ReadValue<Vector2>();
    }
    
    private void OnFrameControlCanceled(InputAction.CallbackContext ctx)
    {
        //controlFrameArea = Vector2.zero;
    }
}
