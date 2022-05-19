using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputSystemInput : IInput
{
    [field: SerializeField] public float vertical { get; private set; }
    [field: SerializeField] public float horizontal { get; private set; }
    [field: SerializeField] public bool interact { get; private set; }
    
    private readonly InputControl inputControl;
    
    public PlayerInputSystemInput()
    {
        inputControl = new InputControl();
        inputControl.Enable();
    }
    public void Create()
    {
        inputControl.Player.Move.performed += OnMovePerformed;
        inputControl.Player.Move.canceled += OnMoveCanceled;
    }

    public void Destroy()
    {
        inputControl.Player.Move.performed -= OnMovePerformed;
        inputControl.Player.Move.canceled -= OnMoveCanceled;
    }

    public void ReadInput()
    {
        interact = inputControl.Player.Interact.WasPressedThisFrame();
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

    #endregion
}
