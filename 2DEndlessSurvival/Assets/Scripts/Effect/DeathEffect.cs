using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    EnemyAnimations enemyAnimations;

    Material material;
    bool isDeath = false;
    float fade = 1f;
    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        enemyAnimations = GetComponent<EnemyAnimations>();
        enemyAnimations.OnDeathEffect += EnemyAnimations_OnDeathEffect;
    }

    private void EnemyAnimations_OnDeathEffect(object sender, System.EventArgs e)
    {
        isDeath = true;
    }

    private void Update()
    {
        if(isDeath)
        {
            fade -= Time.deltaTime;
            if(fade <= 0f)
            {
                fade = 0f;
                isDeath = false;
            }

            material.SetFloat("_Fade", fade);
        }
    }
}
