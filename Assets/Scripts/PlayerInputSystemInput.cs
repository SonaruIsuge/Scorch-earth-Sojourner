using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInputSystemInput : MonoBehaviour, IInput
{
    public float vertical { get; private set; }
    public float horizontal { get; private set; }
    public bool interact { get; private set; }
    

    private InputControl inputControl;

    private void Awake()
    {
        inputControl = new InputControl();
        inputControl.Enable();
    }

    private void OnEnable()
    {
        inputControl.Player.Move.performed += OnMovePerformed;
        inputControl.Player.Move.canceled += OnMoveCanceled;
        inputControl.Player.Interact.performed += OnInteractPerformed;
        inputControl.Player.Interact.canceled += OnInteractCanceled;
    }

    private void OnDisable()
    {
        inputControl.Player.Move.performed -= OnMovePerformed;
        inputControl.Player.Move.canceled -= OnMoveCanceled;
        inputControl.Player.Interact.performed -= OnInteractPerformed;
        inputControl.Player.Interact.canceled -= OnInteractCanceled;
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

    private void OnInteractPerformed(InputAction.CallbackContext ctx)
    {
        interact = true;
    }

    private void OnInteractCanceled(InputAction.CallbackContext ctx)
    {
        interact = false;
    }
    
    #endregion
}
