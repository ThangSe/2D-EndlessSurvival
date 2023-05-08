using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler<OnToggleShiftEventArgs> OnToggleShift;
    public event EventHandler OnJumpAction;
    public event EventHandler OnAttackAction;
    public event EventHandler OnRangeAttackAction;
    public event EventHandler OnStrongAttackAction;
    public event EventHandler OnCastingAttackAction;
    public event EventHandler OnPauseAction;

    public class OnToggleShiftEventArgs : EventArgs
    {
        public bool isToggle;
    }

    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Run.performed += Run_performed;
        inputActions.Player.Run.canceled += Run_canceled;
        inputActions.Player.Jump.performed += Jump_performed;
        inputActions.Player.Attack1.performed += Attack1_performed;
        inputActions.Player.RangeAttack.performed += RangeAttack_performed;
        inputActions.Player.Attack2.performed += Attack2_performed;
        inputActions.Player.CastingAttack.performed += CastingAttack_performed;
        inputActions.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void CastingAttack_performed(InputAction.CallbackContext obj)
    {
        OnCastingAttackAction?.Invoke(this, EventArgs.Empty);
    }

    private void Attack2_performed(InputAction.CallbackContext obj)
    {
        OnStrongAttackAction?.Invoke(this, EventArgs.Empty);
    }

    private void RangeAttack_performed(InputAction.CallbackContext obj)
    {
        OnRangeAttackAction?.Invoke(this, EventArgs.Empty);
    }

    private void Attack1_performed(InputAction.CallbackContext obj)
    {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    private void Run_canceled(InputAction.CallbackContext obj)
    {
        OnToggleShift?.Invoke(this, new OnToggleShiftEventArgs
        {
            isToggle = false
        });
    }

    private void Run_performed(InputAction.CallbackContext obj)
    {
        OnToggleShift?.Invoke(this, new OnToggleShiftEventArgs
        {
            isToggle = true
        });
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
