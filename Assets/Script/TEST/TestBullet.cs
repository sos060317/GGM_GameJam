using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        rigid.velocity = transform.up * moveSpeed;
    }
}