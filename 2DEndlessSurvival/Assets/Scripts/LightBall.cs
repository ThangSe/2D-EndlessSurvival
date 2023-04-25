using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBall : MonoBehaviour
{
    private Vector3 shootDir;
    [SerializeField] private float moveSpeed = 10f;
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float critChance = .33f;
        int damage = 35;
        bool isCrit = false;
    IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (Random.Range(0f, 1f) > (1-critChance))
            {
                damage *= 2;
                isCrit = true;
            }
            damageable.Damage(damage, isCrit);
            Destroy(gameObject);
        }
    }
}
