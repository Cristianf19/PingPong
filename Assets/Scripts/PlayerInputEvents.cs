using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputEvents : MonoBehaviour
{
    public static event Action<Vector2> OnMove;
    private GameInput input;

    private void Awake()
    {
        input = new GameInput();
    }

    private void OnEnable()
    {
        input.Player.Enable();

        input.Player.Move.performed += OnMovePerformed;
        input.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        input.Player.Move.performed -= OnMovePerformed;
        input.Player.Move.canceled -= OnMoveCanceled;

        input.Player.Disable();
    }
    private Vector2 moveInput;

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        OnMove?.Invoke(moveInput);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
        OnMove?.Invoke(moveInput);
    }
}
