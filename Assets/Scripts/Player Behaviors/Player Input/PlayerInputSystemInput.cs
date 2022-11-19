using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputSystemInput : IInput
{
    [field: SerializeField] public float vertical { get; private set; }
    [field: SerializeField] public float horizontal { get; private set; }
    [field: SerializeField] public bool interact { get; private set; }
    

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
        interact = false;
        
        // unregister event
        inputControl.Player.Move.performed -= OnMovePerformed;
        inputControl.Player.Move.canceled -= OnMoveCanceled;
    }

    public void ReadInput()
    {
        interact = inputControl.Player.Interact.WasPressedThisFrame();
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
