using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance
    {
        get;
        private set;
    }

    public event EventHandler AttackingAction;
    public event EventHandler RangeAttackAction;
    public event EventHandler StrongAttackingAction;
    public event EventHandler CastingAttackingAction;
    public event EventHandler OnFlipAction;
    public event EventHandler<OnUseItemEventArgs> OnUseItem;
    public event EventHandler<OnCoinChangedEventArgs> OnCoinChanged;

    public class OnUseItemEventArgs : EventArgs
    {
        public int slot;
    }

    public class OnCoinChangedEventArgs: EventArgs
    {
        public int amountCoin;
        public string typeCoin;
    }

    [SerializeField] private int baseHealth = 100;
    [SerializeField] private int baseMana = 100;
    [SerializeField] private Transform playerHealthBar;
    [SerializeField] private Transform playerManaBar;
    [SerializeField] private PlayerAnimations playerAnimations;
    [SerializeField] private Transform pfLightBall;
    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private float jumpVelocity = 10f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float cooldownAttack = 1f;
    [SerializeField] private float cooldownRangeAttack = .5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private InventoryUI inventoryUI;

    //private bool isAttacking = false;
    private int normalDamage = 20;
    private float critChance = .33f;
    private float lastAttack;
    private BoxCollider2D boxCillider2d;
    private Rigidbody2D rigidbody2d;
    private bool isRunning;
    private bool isWalking;
    private bool isDeath = false;
    private bool flag = false;
    private float moveSpeedMultipler = 1f;
    private float lockPos = 0;
    private int manaCost;
    private float regenManaPerSec = .2f;
    private float regenTimePerMana;
    private float regenHealthPerSec = .1f;
    private float regenTimePerHealth;
    private HealthSystem healthSystem;
    private ManaSystem manaSystem;
    private Inventory inventory;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one player instance");
        }
        Instance = this;

        lastAttack = Time.time;

        regenTimePerMana = 1 / regenManaPerSec;
        regenTimePerHealth = 1 / regenHealthPerSec;

        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCillider2d = transform.GetComponent<BoxCollider2D>();

        healthSystem = new HealthSystem(baseHealth);
        Transform healthBarTransform = Instantiate(playerHealthBar, transform.GetChild(0).Find("HealthBarPoint").position, Quaternion.identity, transform);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        manaSystem = new ManaSystem(baseMana);
        Transform manaBarTransform = Instantiate(playerManaBar, transform.GetChild(0).Find("ManaBarPoint").position, Quaternion.identity, transform);
        ManaBar manaBar = manaBarTransform.GetComponent<ManaBar>();
        manaBar.Setup(manaSystem);

        inventory = new Inventory(UseItem);
        inventoryUI.SetInventory(inventory);
        inventoryUI.SetPlayer(this);

        LightOff(); 
    }

    private void Start()
    {
        gameInput.OnToggleShift += GameInput_OnToggleShift;
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        gameInput.OnAttackAction += GameInput_OnAttackAction;
        gameInput.OnRangeAttackAction += GameInput_OnRangeAttackAction;
        gameInput.OnStrongAttackAction += GameInput_OnStrongAttackAction;
        gameInput.OnCastingAttackAction += GameInput_OnCastingAttackAction;
        gameInput.UseItemSlot += GameInput_UseItemSlot;
        playerAnimations.OnCastingAttackDuelDamage += PlayerAnimations_OnCastingAttackDuelDamage;
        playerAnimations.OnStrongAttackDuelDamage += PlayerAnimations_OnStrongAttackDuelDamage;
        playerAnimations.OnAttackDuelDamage += PlayerAnimations_OnAttackDuelDamage;
        playerAnimations.OnRangeAttackCast += PlayerAnimations_OnRangeAttackCast;
        GameOverUI.Instance.OnRespawnAction += GameOver_OnRespawnAction;
    }

    private void GameOver_OnRespawnAction(object sender, EventArgs e)
    {
        isDeath = false;
        healthSystem.UsingPotion(baseHealth);
        manaSystem.UsingPotion(baseMana);
    }

    private void GameInput_UseItemSlot(object sender, GameInput.UseItemSlotEventArgs e)
    {
        OnUseItem?.Invoke(this, new OnUseItemEventArgs
        {
            slot = e.slot
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if(itemWorld != null)
        {
            if (itemWorld.GetItem().itemType == Item.ItemType.CopperCoin || itemWorld.GetItem().itemType == Item.ItemType.SilverCoin || itemWorld.GetItem().itemType == Item.ItemType.GoldenCoin)
            {
                OnCoinChanged?.Invoke(this, new OnCoinChangedEventArgs
                {
                    amountCoin = itemWorld.GetItem().amount,
                    typeCoin = itemWorld.GetItem().itemType.ToString()
                });
                itemWorld.DestroySelf();

            }
            int amountLeft = inventory.AddItem(itemWorld.GetItem());
            if(amountLeft == 0)
            {
                itemWorld.DestroySelf();
            } else
            {
                itemWorld.SetAmount(amountLeft);
            }
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                int healthAmount = 30;
                healthSystem.UsingPotion(healthAmount);
                bool isGainHealth = true;
                PlayerPopup.Create(transform.position, healthAmount, isGainHealth: isGainHealth);
                break;
            case Item.ItemType.ManaPotion:
                int manaAmount = 20;
                bool isGainMana = true;
                PlayerPopup.Create(transform.position, manaAmount, isGainMana: isGainMana);
                manaSystem.UsingPotion(manaAmount);
                break;
        }
    }

    private void PlayerAnimations_OnAttackDuelDamage(object sender, EventArgs e)
    {
        float attackRadius = .7f;
        Hit(attackRadius, normalDamage);
    }

    private void PlayerAnimations_OnStrongAttackDuelDamage(object sender, EventArgs e)
    {
        float attackRadius = .7f;
        Hit(attackRadius, normalDamage * 5 / 4);
    }

    private void PlayerAnimations_OnCastingAttackDuelDamage(object sender, EventArgs e)
    {
        float rayLong = 3.7f;
        Vector3 castingPoint = transform.GetChild(0).Find("FireLightBallPoint").position;
        if (transform.localScale.x == 1 && rayLong < 0) rayLong = -rayLong;
        if (transform.localScale.x == -1 && rayLong > 0) rayLong = -rayLong;
        Vector3 rayDir = new Vector3(rayLong, 0);
        Vector3 endCastingPoint = new Vector3(castingPoint.x + rayLong, castingPoint.y);
        RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(castingPoint, rayDir, Vector3.Distance(castingPoint, endCastingPoint), LayerMask.GetMask("Enemy"));
        SpellCastingHit(raycastHit2D);
    }

    private void GameInput_OnCastingAttackAction(object sender, EventArgs e)
    {
        if (!EndlessSurvivalManager.Instance.IsGamePlaying()) return;
        
        if (IsGrounded() && (Time.time - lastAttack) > cooldownRangeAttack)
        {
            manaCost = 3;
            if (manaSystem.GetMana() < manaCost) return;
            manaSystem.UsingMana(manaCost);
            CastingAttackingAction?.Invoke(this, EventArgs.Empty);
            lastAttack = Time.time;
        }
    }

    private void SpellCastingHit(RaycastHit2D[] raycastHit2D)
    {
        int spellDamage = 30;
        bool isCrit = false;
        if(UnityEngine.Random.Range(0f, 1f) > (1 - critChance))
        {
            spellDamage *= 2;
            isCrit = true;
        }
        foreach (var rch in raycastHit2D)
        {
            var collider = rch.collider;
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(spellDamage, isCrit);
            }
        }
    }

    private void GameInput_OnStrongAttackAction(object sender, EventArgs e)
    {
        if (!EndlessSurvivalManager.Instance.IsGamePlaying()) return;
        
        if (IsGrounded() && (Time.time - lastAttack) > cooldownAttack)
        {
            manaCost = 1;
            if (manaSystem.GetMana() < manaCost) return;
            manaSystem.UsingMana(manaCost);
            StrongAttackingAction?.Invoke(this, EventArgs.Empty);
            
            lastAttack = Time.time;
        }
    }
    private void Hit(float attackRadius, int normalDamage)
    {
        Collider2D[] colliderEnemies = Physics2D.OverlapCircleAll(transform.GetChild(0).Find("AttackPoint").position, attackRadius, LayerMask.GetMask("Enemy"));
        bool isCrit = false;
        if (UnityEngine.Random.Range(0f, 1f) > (1 - critChance))
        {
            normalDamage *= 2;
            isCrit = true;
        }
        foreach (Collider2D enemy in colliderEnemies)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(normalDamage, isCrit);
            }
        }
    }

    private void PlayerAnimations_OnRangeAttackCast(object sender, EventArgs e)
    {
        Transform lightBallTransform = Instantiate(pfLightBall, transform.GetChild(0).Find("FireLightBallPoint").position, Quaternion.identity);
        Vector3 shootDir = new Vector3();
        if (transform.localScale.x == 1)
        {
            shootDir = Vector3.right;
        }
        if (transform.localScale.x == -1)
        {
            shootDir = Vector3.left;
        }
        lightBallTransform.GetComponent<LightBall>().Setup(shootDir);
    }

    private void GameInput_OnRangeAttackAction(object sender, EventArgs e)
    {
        if (!EndlessSurvivalManager.Instance.IsGamePlaying()) return;
        if (IsGrounded() && (Time.time - lastAttack) > cooldownRangeAttack)
        {
            manaCost = 5;
            if (manaSystem.GetMana() < manaCost) return;
            manaSystem.UsingMana(manaCost);
            RangeAttackAction?.Invoke(this, EventArgs.Empty);           
            lastAttack = Time.time;
        }
    }

    private void GameInput_OnAttackAction(object sender, System.EventArgs e)
    {
        if (!EndlessSurvivalManager.Instance.IsGamePlaying()) return;
        if (IsGrounded() && (Time.time - lastAttack) > cooldownAttack)
        {
            manaCost = 1;
            if (manaSystem.GetMana() < manaCost) return;
            manaSystem.UsingMana(manaCost);
            AttackingAction?.Invoke(this, EventArgs.Empty);
            lastAttack = Time.time;
        }
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {
        if (!EndlessSurvivalManager.Instance.IsGamePlaying()) return;

        if (IsGrounded() && playerAnimations.canMove())
        {
            rigidbody2d.velocity = Vector2.up * jumpVelocity;
        }
    }

    private void GameInput_OnToggleShift(object sender, GameInput.OnToggleShiftEventArgs e)
    {
        if(e.isToggle == true)
        {
            isRunning = true;
        } else
        {
            isRunning = false;
        }
    }

    private void Update()
    {
        FreezeRotation();
        Regen();
        HandleMovement();
    }

    private void FreezeRotation()
    {
        transform.rotation = Quaternion.Euler(lockPos, lockPos, lockPos);
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCillider2d.bounds.center, boxCillider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    private void Flip(Vector2 moveDirection)
    {
        if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            OnFlipAction?.Invoke(this, EventArgs.Empty);
        }
        if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            OnFlipAction?.Invoke(this, EventArgs.Empty);
        }
    }

    private void HandleMovement()
    {
        if (!EndlessSurvivalManager.Instance.IsGamePlaying()) return;

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, 0f);
        bool canMove = !Physics2D.BoxCast(boxCillider2d.bounds.center, boxCillider2d.bounds.size, 0f, moveDir, .1f, wallLayerMask);
        if (isRunning && !flag)
        {
            flag = true;
            moveSpeedMultipler = 2f;
        }
        float moveDistance = moveSpeed * moveSpeedMultipler * Time.deltaTime;

        isWalking = moveDir != Vector3.zero && !isRunning;
        if(canMove && playerAnimations.canMove())
        {
            transform.position += moveDir * moveDistance;
        }

        if (!isRunning)
        {
            flag = false;
            moveSpeedMultipler = 1f;
        }
        Flip(inputVector);
    }

    private void Regen()
    {
        regenTimePerMana -= Time.deltaTime;
        regenTimePerHealth -= Time.deltaTime;
        if (regenTimePerMana <= 0)
        {
            manaSystem.ManaRegen();
            regenTimePerMana = 1 / regenManaPerSec;
        }
        if(regenTimePerHealth <= 0)
        {
            healthSystem.HealthRegen();
            regenTimePerHealth = 1 / regenHealthPerSec;
        }
    }
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public void Damage(int damage, bool isCrit)
    {        
        if (healthSystem.GetHealth() <= 0)
        {
            isDeath = true;
        }
        else
        {
            healthSystem.Damage(damage);
            bool isDamage = true;
            PlayerPopup.Create(transform.position, damage, isDamage: isDamage);
            //OnAttacked?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsDeath()
    {
        return isDeath;
    }
    
    public void LightOff()
    {
        transform.Find("NightLight").gameObject.SetActive(false);
    }

    public void LightOn()
    {
        transform.Find("NightLight").gameObject.SetActive(true);
    }
}
