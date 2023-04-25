using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimations : MonoBehaviour
{
    public static PlayerAnimations Instance { get; private set; }
    public event EventHandler OnCastingAttackDuelDamage;
    public event EventHandler OnStrongAttackDuelDamage;
    public event EventHandler OnAttackDuelDamage;
    public event EventHandler OnRangeAttackCast;

    private const string IS_WALKING = "IsWalking";
    private const string IS_RUNNING = "IsRunning";
    private const string IS_GROUNDED = "IsGrounded";
    private const string CAN_MOVE = "canMove";
    private const string ATTACK = "Attack";
    private const string RANGE_ATTACK = "RangeAttack";
    private const string STRONG_ATTACK = "StrongAttack";
    private const string CASTING_ATTACK = "RayLightAttack";
    private const string IS_ATTACKING = "IsAttacking";
    [SerializeField] private Player player;
    private Animator animator;

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        player.AttackingAction += Player_AttackingAction;
        player.RangeAttackAction += Player_RangeAttackAction;
        player.StrongAttackingAction += Player_StrongAttackingAction;
        player.CastingAttackingAction += Player_CastingAttackingAction;
    }

    private void Player_CastingAttackingAction(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CASTING_ATTACK);
    }

    private void Player_StrongAttackingAction(object sender, System.EventArgs e)
    {
        animator.SetTrigger(STRONG_ATTACK);
    }

    private void Player_RangeAttackAction(object sender, System.EventArgs e)
    {
        animator.SetTrigger(RANGE_ATTACK);
    }

    private void Player_AttackingAction(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.SetBool(IS_RUNNING, player.IsRunning());
        animator.SetBool(IS_GROUNDED, player.IsGrounded());
    }

    public bool canMove()
    {
        return animator.GetBool(CAN_MOVE);
    }

    public bool isAttacking()
    {
        return animator.GetBool(IS_ATTACKING);
    }

    public void CastingAttackDuelDamage()
    {
        OnCastingAttackDuelDamage?.Invoke(this, EventArgs.Empty);
    }

    public void StrongAttackDuelDamge()
    {
        OnStrongAttackDuelDamage?.Invoke(this, EventArgs.Empty);
    }

    public void AttackDuelDamage()
    {
        OnAttackDuelDamage?.Invoke(this, EventArgs.Empty);
    }

    public void RangeAttackCast()
    {
        OnRangeAttackCast?.Invoke(this, EventArgs.Empty);
    }
}
