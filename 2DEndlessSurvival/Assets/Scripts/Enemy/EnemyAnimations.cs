using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAnimations : MonoBehaviour
{
    public event EventHandler OnAttackAction;
    public event EventHandler OnDeathAction;
    public event EventHandler OnDeathEffect;
    public event EventHandler OnRangeAttackAction;

    [SerializeField] private Enemy enemy;
    [SerializeField] private EnemyAI enemyAI;

    private const string ATTACK = "Attack";
    private const string IS_ATTACKED = "Attacked";
    private const string CAN_MOVE = "canMove";
    private const string IS_WALKING = "IsWalking";
    private const string IS_DEATH = "Die";
    private const string RANGE_ATTACK = "ShootArrow";
    private const string BLOCK_ANIMATION = "Block";
    private const string DODGE = "Dodge";
    private const string PROTECT = "Protect";
    private const string IS_PROTECT = "isProtect";
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemy.OnAttacked += Enemy_OnAttacked;
        enemyAI.OnAttackAction += EnemyAI_OnAttackAction;
        enemyAI.OnRangeAttackAction += EnemyAI_OnRangeAttackAction;
        enemyAI.OnDodgeAttackAction += EnemyAI_OnDodgeAttackAction;
        enemyAI.OnBlockAttackAction += EnemyAI_OnBlockAttackAction;
        enemy.OnDeath += Enemy_OnDeath;
    }

    private void EnemyAI_OnBlockAttackAction(object sender, EventArgs e)
    {
        animator.SetTrigger(PROTECT);
    }

    private void EnemyAI_OnDodgeAttackAction(object sender, EventArgs e)
    {
        animator.SetTrigger(DODGE);
    }

    private void EnemyAI_OnRangeAttackAction(object sender, EventArgs e)
    {
        animator.SetTrigger(RANGE_ATTACK);
    }

    private void Enemy_OnDeath(object sender, EventArgs e)
    {
        animator.SetBool(BLOCK_ANIMATION, true);
        animator.SetTrigger(IS_DEATH);
    }

    private void EnemyAI_OnAttackAction(object sender, System.EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

    private void Enemy_OnAttacked(object sender, System.EventArgs e)
    {
        animator.SetTrigger(IS_ATTACKED);
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, enemyAI.IsWalking());
    }

    public bool canMove()
    {
        return animator.GetBool(CAN_MOVE);
    }

    public bool IsProtect()
    {
        return animator.GetBool(IS_PROTECT);
    }

    public void Attack()
    {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }

    public void Die()
    {
        OnDeathAction?.Invoke(this, EventArgs.Empty);
    }
    public void DeathEffect()
    {
        OnDeathEffect?.Invoke(this, EventArgs.Empty);
    }

    public void ShootingArrow()
    {
        OnRangeAttackAction?.Invoke(this, EventArgs.Empty);
    }
}
