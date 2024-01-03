using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private bool isEnemyPenetrate; // 적 관통
    [SerializeField] private bool isWallPenetrate;  // 벽 관통

    [SerializeField] private Effect dieEffect;

    private float attackDamage;

    private Rigidbody2D rigid;

    public void Init(float damage)
    {
        attackDamage = damage;

        rigid = GetComponent<Rigidbody2D>();

        rigid.velocity = transform.right * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBase>().OnDamage(attackDamage);

            // 적을 관통할수 없다면
            if (!isEnemyPenetrate)
            {
                Instantiate(dieEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (collision.CompareTag("Wall"))
        {
            // 벽을 관통할 수 없다면
            if (!isWallPenetrate)
            {
                Instantiate(dieEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}