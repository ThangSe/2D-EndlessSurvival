using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArrow : MonoBehaviour
{
    private Vector3 shootDir;
    [SerializeField] private float moveSpeed = 10f;

    private BoxCollider2D boxCillider2d;
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.localScale = new Vector3(shootDir.x, 1f, 1f);
        Destroy(gameObject, 3f);
    }

    private void Awake()
    {
        boxCillider2d = transform.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(boxCillider2d.IsTouchingLayers(1 << LayerMask.NameToLayer("Player")))
        {
            float critChance = .33f;
            int damage = 5;
            bool isCrit = false;
            Player damageable = collision.GetComponent<Player>();
            if (damageable != null)
            {
                if (Random.Range(0f, 1f) > (1 - critChance))
                {
                    damage *= 2;
                    isCrit = true;
                }
                damageable.Damage(damage, isCrit);
                Destroy(gameObject);
            }
        }        
    }
}
