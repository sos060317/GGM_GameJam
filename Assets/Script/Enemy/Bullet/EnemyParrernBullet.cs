using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParrernBullet : MonoBehaviour
{
    public float turnSpeed;
    public float moveSpeed;

    public float lifeTime;

    private float timer;

    private Vector2 dir;

    float z;

    Rigidbody2D rigid;

    public void Init(Vector2 dir)
    {
        this.dir = dir;
    }

    private void Update()
    {
        Vector3 nextPos = dir.normalized * moveSpeed * Time.deltaTime;

        transform.position += nextPos;

        z += Time.deltaTime * turnSpeed;
        transform.rotation = Quaternion.Euler(0, 0, z);

        timer += Time.deltaTime;

        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}