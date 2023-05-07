using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputSystemInput : IInput
{
    public float vertical { get; private set; }
    public float horizontal { get; private set; }
    public bool interact => inputControl.Player.Interact.WasPressedThisFrame();
    public bool openMap => inputControl.Player.OpenMap.WasPressedThisFrame();
    

    private readonly InputControl inputControl;
    
    public PlayerInputSystemInput(InputControl input)
    {
        inputControl = input;
        inputControl.Player.Enable();
    }

    public void EnableInput(bool enable)
    {
        if(enable) inputControl.Player.Enable();
        else inputControl.Player.Disable();
    }

    public void Register()
    {
        inputControl.Player.Move.performed += OnMovePerformed;
        inputControl.Player.Move.canceled += OnMoveCanceled;
    }

    public void Unregister()
    {
        // reset data value
        horizontal = 0;
        vertical = 0;
        
        // unregister event
        inputControl.Player.Move.performed -= OnMovePerformed;
        inputControl.Player.Move.canceled -= OnMoveCanceled;
    }


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

    
}
