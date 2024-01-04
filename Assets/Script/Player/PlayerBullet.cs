using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;

    [SerializeField] private bool isEnemyPenetrate; // �� ����
    [SerializeField] private bool isWallPenetrate;  // �� ����
    [SerializeField] private bool enemyBulletDestroy;   // �� �Ѿ� ������ ����

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

            // ���� �����Ҽ� ���ٸ�
            if (!isEnemyPenetrate)
            {
                Instantiate(dieEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (collision.CompareTag("Wall"))
        {
            // ���� ������ �� ���ٸ�
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