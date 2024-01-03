using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private bool isEnemyPenetrate; // �� ����
    [SerializeField] private bool isWallPenetrate;  // �� ����

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
    }
}