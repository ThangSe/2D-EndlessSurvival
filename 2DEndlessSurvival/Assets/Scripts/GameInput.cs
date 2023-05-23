using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler<OnToggleShiftEventArgs> OnToggleShift;
    public event EventHandler<UseItemSlotEventArgs> UseItemSlot;
    public event EventHandler OnJumpAction;
    public event EventHandler OnAttackAction;
    public event EventHandler OnRangeAttackAction;
    public event EventHandler OnStrongAttackAction;
    public event EventHandler OnCastingAttackAction;
    public event EventHandler OnPauseAction;
    public event EventHandler OpenTutorialMenu;

    public class UseItemSlotEventArgs: EventArgs
    {
        public int slot;
    }
    public class OnToggleShiftEventArgs : EventArgs
    {
        public bool isToggle;
    }

    private InputActions inputActions;

    private void Awake()
    {
        Instance = this;
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
        inputActions.Player.UseItem1.performed += UseItem1_performed;
        inputActions.Player.UseItem2.performed += UseItem2_performed;
        inputActions.Player.UseItem3.performed += UseItem3_performed;
        inputActions.Player.UseItem4.performed += UseItem4_performed;
        inputActions.Player.UseItem5.performed += UseItem5_performed;
        inputActions.Player.TutorialMenu.performed += TutorialMenu_performed;
    }

    private void OnDestroy()
    {
        inputActions.Player.Enable();
        inputActions.Player.Run.performed -= Run_performed;
        inputActions.Player.Run.canceled -= Run_canceled;
        inputActions.Player.Jump.performed -= Jump_performed;
        inputActions.Player.Attack1.performed -= Attack1_performed;
        inputActions.Player.RangeAttack.performed -= RangeAttack_performed;
        inputActions.Player.Attack2.performed -= Attack2_performed;
        inputActions.Player.CastingAttack.performed -= CastingAttack_performed;
        inputActions.Player.Pause.performed -= Pause_performed;
        inputActions.Player.UseItem1.performed -= UseItem1_performed;
        inputActions.Player.UseItem2.performed -= UseItem2_performed;
        inputActions.Player.UseItem3.performed -= UseItem3_performed;
        inputActions.Player.UseItem4.performed -= UseItem4_performed;
        inputActions.Player.UseItem5.performed -= UseItem5_performed;
        inputActions.Player.TutorialMenu.performed -= TutorialMenu_performed;

        inputActions.Dispose();
    }

    private void TutorialMenu_performed(InputAction.CallbackContext obj)
    {
        OpenTutorialMenu?.Invoke(this, EventArgs.Empty);
    }

    private void UseItem5_performed(InputAction.CallbackContext obj)
    {
        UseItemSlot?.Invoke(this, new UseItemSlotEventArgs
        {
            slot = 5
        });
    }

    private void UseItem4_performed(InputAction.CallbackContext obj)
    {
        UseItemSlot?.Invoke(this, new UseItemSlotEventArgs
        {
            slot = 4
        });
    }

    private void UseItem3_performed(InputAction.CallbackContext obj)
    {
        UseItemSlot?.Invoke(this, new UseItemSlotEventArgs
        {
            slot = 3
        });
    }

    private void UseItem2_performed(InputAction.CallbackContext obj)
    {
        UseItemSlot?.Invoke(this, new UseItemSlotEventArgs
        {
            slot = 2
        });
    }

    private void UseItem1_performed(InputAction.CallbackContext obj)
    {
        UseItemSlot?.Invoke(this, new UseItemSlotEventArgs
        {
            slot = 1
        });
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
