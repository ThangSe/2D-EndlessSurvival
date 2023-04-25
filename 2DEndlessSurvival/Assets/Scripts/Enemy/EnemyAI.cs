using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI : MonoBehaviour
{  
    private enum State{
        Roaming,
        ChaseTarget,
        ShootingTarget,
        DodgeAttack,
        BlockAttack,
    }

    public event EventHandler OnAttackAction;
    public event EventHandler OnRangeAttackAction;
    public event EventHandler OnDodgeAttackAction;
    public event EventHandler OnBlockAttackAction;

    [SerializeField] private EnemyAnimations enemyAnimations;
    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float moveSpeedMultipler = 1f;
    [SerializeField] private float attackRange = 10f;

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private Rigidbody2D rigidBody2D;
    private BoxCollider2D boxCillider2D;
    private Vector2 movementVector = Vector2.zero;
    private bool isWalking;
    private bool cooldownHit;
    private int normalDamage = 5;
    private float attackRadius = .3f;
    private float distancePerformMelleAttack = 2f;
    private float distancePerformRangeAttack = 5f;
    private float distancePerformDodge = 4f;
    private float distancePerformBlock = 5f;
    private float dodgeSpeed;
    private float blockTimer;
    private bool cooldownDodge;
    private State state;

    private void Awake()
    {
        state = State.Roaming;
        rigidBody2D = transform.GetComponent<Rigidbody2D>();
        boxCillider2D = transform.GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        enemyAnimations.OnAttackAction += EnemyAnimations_OnAttackAction;
        enemyAnimations.OnRangeAttackAction += EnemyAnimations_OnRangeAttackAction;
    }

    private void EnemyAnimations_OnRangeAttackAction(object sender, EventArgs e)
    {
        Transform arrowTransform = Instantiate(CommonAssetsUsing.i.pfSkeletonArrow, transform.Find("ArrowPoint").position, Quaternion.identity);
        Vector3 shootDir = new Vector3();
        if (transform.localScale.x == 1)
        {
            shootDir = Vector3.right;
        }
        if (transform.localScale.x == -1)
        {
            shootDir = Vector3.left;
        }
        arrowTransform.GetComponent<SkeletonArrow>().Setup(shootDir);
    }

    private void EnemyAnimations_OnAttackAction(object sender, EventArgs e)
    {
        Collider2D[] colliderPlayers = Physics2D.OverlapCircleAll(transform.Find("AttackPoint").position, attackRadius, LayerMask.GetMask("Player"));
        Hit(colliderPlayers, normalDamage);
    }

    private void Update()
    {
        if (!IsGrounded())
        {
            movementVector.y += Physics2D.gravity.y * Time.deltaTime;
            rigidBody2D.MovePosition(rigidBody2D.position + movementVector * Time.deltaTime);
        }
        switch (state)
        {
            default:
            case State.Roaming:
                dodgeSpeed = 60f;
                blockTimer = 1f;
                if (IsGrounded() && enemyAnimations.canMove())
                {
                    MoveTo(roamPosition);
                }                
                float reachedPositionDistance = 1f;
                if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
                {
                    // Reached Roam Position
                    roamPosition = GetRoamingPosition();
                }
                FindTarget();
                break;
            case State.ChaseTarget:
                float facingDir = Player.Instance.GetPosition().x - transform.position.x;
                if (facingDir > 0) transform.localScale = new Vector3(1, 1, 1);
                if (facingDir < 0) transform.localScale = new Vector3(-1, 1, 1);
                float distance = Vector3.Distance(transform.position, Player.Instance.GetPosition());
                if(distance <= distancePerformDodge && PlayerAnimations.Instance.isAttacking() && !cooldownDodge && attackRange > distancePerformRangeAttack)
                {
                    state = State.DodgeAttack;
                }
                if (distance > distancePerformBlock && PlayerAnimations.Instance.isAttacking() && attackRange <= distancePerformMelleAttack)
                {
                    state = State.BlockAttack;
                }
                if (distance <= attackRange)
                {
                    if (IsGrounded() && enemyAnimations.canMove() && attackRange <= distancePerformMelleAttack)
                    {
                        StopMoving();
                        if (!cooldownHit)
                        {
                            float delayHit = 2f;
                            StartCoroutine(CooldownHit(delayHit, cooldownHit));
                        }
                    }
                    if (IsGrounded() && enemyAnimations.canMove() && attackRange > distancePerformRangeAttack)
                    {
                        StopMoving();
                        if(distance > distancePerformMelleAttack)
                        {
                            state = State.ShootingTarget;
                        } else
                        {
                            if (!cooldownHit)
                            {
                                float delayHit = 2f;
                                StartCoroutine(CooldownHit(delayHit, cooldownHit));
                            }
                        }
                    }
                } else
                {
                    if (IsGrounded() && enemyAnimations.canMove())
                    {
                        MoveTo(Player.Instance.GetPosition());
                    }    
                }
                break;
            case State.ShootingTarget:
                distance = Vector3.Distance(transform.position, Player.Instance.GetPosition());
                if (distance > attackRange)
                {
                    state = State.ChaseTarget;
                } else
                {
                    if(distance <= distancePerformMelleAttack)
                    {
                        state = State.ChaseTarget;
                    } else
                    {
                        if (!cooldownHit)
                        {
                            float delayHit = 2f;
                            StartCoroutine(CooldownShootingArrow(delayHit, cooldownHit));
                        }
                    }
                }
                break;
            case State.DodgeAttack:
                float dodgeSpeedDropMultipler = 2f;
                float dodgeSpeedMinimum = 7f;
                dodgeSpeed -= dodgeSpeed * dodgeSpeedDropMultipler * Time.deltaTime;
                DodgeBackward();
                if (!cooldownDodge)
                {
                    float cooldownTime = 4f;
                    OnDodgeAttackAction?.Invoke(this, EventArgs.Empty);
                    StartCoroutine(CooldownDodge(cooldownTime, cooldownDodge));
                }
                if(dodgeSpeed < dodgeSpeedMinimum)
                {
                    state = State.Roaming;
                }
                break;
            case State.BlockAttack:
                OnBlockAttackAction?.Invoke(this, EventArgs.Empty);
                blockTimer -= Time.deltaTime;
                if(blockTimer <= 0 )
                {
                    state = State.Roaming;
                }
                break;
        }
    }

    private IEnumerator CooldownShootingArrow(float delayTime, bool cooldownHit)
    {
        this.cooldownHit = !cooldownHit;
        OnRangeAttackAction?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(delayTime);
        this.cooldownHit = cooldownHit;
    }

    private IEnumerator CooldownHit(float delayTime, bool cooldownHit)
    {
        this.cooldownHit = !cooldownHit;
        OnAttackAction?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(delayTime);
        this.cooldownHit = cooldownHit;
    }
    private IEnumerator CooldownDodge(float cooldownTime, bool cooldownDodge)
    {
        this.cooldownDodge = !cooldownDodge;
        yield return new WaitForSeconds(cooldownTime);
        this.cooldownDodge = cooldownDodge;
    }

    private void Hit(Collider2D[] colliderPlayers, int normalDamage)
    {
        bool isCrit = false;
        foreach (Collider2D player in colliderPlayers)
        {
            IDamageable damageable = player.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(normalDamage, isCrit);
            }
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * UnityEngine.Random.Range (10f, 0);
    }

    public Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), 0).normalized;
    }

    private void MoveTo (Vector3 roamPosition)
    {
        if(roamPosition.x - transform.position.x >= 1f)
        {
            movementVector = Vector3.right;
            transform.localScale = new Vector3(1, 1, 1);
        } else
        {
            movementVector = Vector3.left;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        bool canMove = !Physics2D.BoxCast(boxCillider2D.bounds.center, boxCillider2D.bounds.size, 0f, movementVector, .1f, wallLayerMask);
        if(canMove)
        {
            rigidBody2D.MovePosition(rigidBody2D.position + movementVector * moveSpeed * moveSpeedMultipler * Time.deltaTime);
            isWalking = true;
        }      
    }

    private void FindTarget()
    {
        float targetRange = 15f;
        if(Vector3.Distance(transform.position, Player.Instance.GetPosition()) < targetRange)
        {
            state = State.ChaseTarget;
        }
    }

    private void StopMoving()
    {        
        Vector3 currentPosition = transform.position;
        transform.position = currentPosition;
        isWalking = false;
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCillider2D.bounds.center, boxCillider2D.bounds.size, 0f, Vector2.down, .2f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void DodgeBackward()
    {
        Vector2 dodgeDir = Vector2.zero;
        if (transform.localScale.x == 1) dodgeDir = new Vector2(-1f, .1f);
        if(transform.localScale.x == -1) dodgeDir = new Vector2(1f, .1f);
        rigidBody2D.MovePosition(rigidBody2D.position + dodgeDir * dodgeSpeed * Time.deltaTime);
    }
}
