using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;

    [SerializeField] private bool isEnemyPenetrate; // 적 관통
    [SerializeField] private bool isWallPenetrate;  // 벽 관통
    [SerializeField] private bool enemyBulletDestroy;   // 적 총알 닿으면 삭제

    [SerializeField] private Effect dieEffect;

    private float attackDamage;

    private Rigidbody2D rigid;

    public void Init(float damage)
    {
        attackDamage = damage;

        rigid = GetComponent<Rigidbody2D>();

        rigid.velocity = transform.right * moveSpeed;

        StartCoroutine(DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(lifeTime);

        Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
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

        if (collision.CompareTag("Enemy Bullet"))
        {
            if (enemyBulletDestroy)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}