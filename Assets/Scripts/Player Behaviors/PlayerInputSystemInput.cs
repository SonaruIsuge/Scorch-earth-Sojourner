using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputSystemInput : IInput
{
    [field: SerializeField] public float vertical { get; private set; }
    [field: SerializeField] public float horizontal { get; private set; }
    [field: SerializeField] public bool interact { get; private set; }
    
    [field: SerializeField] public bool enablePhoto { get; private set; }
    [field: SerializeField] public Vector2 controlFrameArea { get; private set; }
    [field: SerializeField] public bool takePhoto { get; private set; }
    
    private readonly InputControl inputControl;
    
    public PlayerInputSystemInput()
    {
        inputControl = new InputControl();
        inputControl.Enable();

        enablePhoto = false;
    }
    public void Register()
    {
        inputControl.Player.Move.performed += OnMovePerformed;
        inputControl.Player.Move.canceled += OnMoveCanceled;
        inputControl.Player.ControlPictureArea.performed += OnFrameControlPerformed;
        inputControl.Player.ControlPictureArea.canceled += OnFrameControlCanceled;
    }

    public void Unregister()
    {
        inputControl.Player.Move.performed -= OnMovePerformed;
        inputControl.Player.Move.canceled -= OnMoveCanceled;
        inputControl.Player.ControlPictureArea.performed -= OnFrameControlPerformed;
        inputControl.Player.ControlPictureArea.canceled -= OnFrameControlCanceled;
    }

    public void ReadInput()
    {
        interact = inputControl.Player.Interact.WasPressedThisFrame();
        takePhoto = inputControl.Player.TakePicture.WasPressedThisFrame();
        if (inputControl.Player.EnablePicture.WasPressedThisFrame()) enablePhoto = !enablePhoto;
    }
    

    #region inputAction callback function

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        horizontal = ctx.ReadValue<Vector2>().x;
        vertical = ctx.ReadValue<Vector2>().y;
    }
    
    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        horizontal = 0;
        vertical = 0;
    }

    private void OnFrameControlPerformed(InputAction.CallbackContext ctx)
    {
        controlFrameArea = ctx.ReadValue<Vector2>();
    }
    
    private void OnFrameControlCanceled(InputAction.CallbackContext ctx)
    {
        controlFrameArea = Vector2.zero;
    }
    
    #endregion
}
