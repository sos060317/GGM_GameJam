using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Effect hitEffect;

    [SerializeField] private float damage;

    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        rigid.velocity = transform.up * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 플레이어 데미지 입히기

            GameManager.Instance.curPlayer.OnDamege(damage);

            Instantiate(hitEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
