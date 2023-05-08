using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    public event EventHandler OnAttacked;
    public event EventHandler OnDeath;
    ScoreManager scoreManager;

    [SerializeField] private EnemyAnimations enemyAnimations;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private EnemiesSO enemiesSO;

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
            SoundManager.Instance.PlaySkeletonBlock(transform.position);
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
