using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    public static EventHandler OnAnyBlockHit;

    public static void ResetStaticData()
    {
        OnAnyBlockHit = null;
    }

    public event EventHandler OnAttacked;
    public event EventHandler OnDeath;
    ScoreManager scoreManager;

    [SerializeField] private EnemyAnimations enemyAnimations;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private EnemiesSO enemiesSO;

    public float minCoinDrop = 8f;
    public float maxCoinDrop = 13f;

    private HealthSystem healthSystem;
    private bool isDeath = false;
    private static List<Enemy> enemyList;
    public static Enemy GetClosest(Vector3 position, float maxRange)
    {
        Enemy closest = null;
        foreach (Enemy enemy in enemyList)
        {
            if(Vector3.Distance(position, enemy.GetPosition()) < maxRange){
                if(closest == null)
                {
                    closest = enemy;
                } else
                {
                    if(Vector3.Distance(position, enemy.GetPosition()) < Vector3.Distance(position, closest.GetPosition()))
                    {
                        closest = enemy;
                    }
                }
            }
        }
        return closest;
    }
    private void Start()
    {
        healthSystem = new HealthSystem(maxHealth);
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        enemyAnimations.OnDeathAction += EnemyAnimations_OnDeathAction;
    }


    private void EnemyAnimations_OnDeathAction(object sender, EventArgs e)
    {
        NormalDrop();
        AlwaysDrop();
        Destroy(gameObject);
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        
    }

    private void Awake()
    {
        if (enemyList == null) enemyList = new List<Enemy>();
        enemyList.Add(this);
    }

    public void Damage(int damage, bool isCrit)
    {       
        if (!isDeath && !enemyAnimations.IsProtect())
        {
            healthSystem.Damage(damage);
            DamagePopup.Create(transform.position, damage, isCrit);
            if (healthSystem.GetHealth() <= 0)
            {
                enemyList.Remove(this);
                OnDeath?.Invoke(this, EventArgs.Empty);
                scoreManager.IncKillCount();
                isDeath = true;                
            }
            else
            {
                OnAttacked?.Invoke(this, EventArgs.Empty);
            }
        }
        if (!isDeath && enemyAnimations.IsProtect())
        {
            OnAnyBlockHit?.Invoke(this, EventArgs.Empty);
        }
    }

    private void NormalDrop()
    {
        int amount = 1;
        if (UnityEngine.Random.Range(0f, 1f) >= (1 - CommonAssetsUsing.i.commonDropRate))
        {
            ItemWorld.EnemyDrop(DropPosition(), Item.CreateItem(Item.ItemType.HealthPotion, amount));
        }
        if (UnityEngine.Random.Range(0f, 1f) >= (1 - CommonAssetsUsing.i.commonDropRate))
        {
            ItemWorld.EnemyDrop(DropPosition(), Item.CreateItem(Item.ItemType.ManaPotion, amount));
        }       
    }

    private void AlwaysDrop()
    {
        int amount = Mathf.CeilToInt(UnityEngine.Random.Range(minCoinDrop, maxCoinDrop));
        if (UnityEngine.Random.Range(0f, 1f) >= (1 - CommonAssetsUsing.i.alwaysDropRate))
        {
            ItemWorld.EnemyDrop(DropPosition(), Item.CreateItem(Item.ItemType.CopperCoin, amount));
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    private Vector3 DropPosition()
    {
        return transform.Find("DropPoint").position;
    }
}
