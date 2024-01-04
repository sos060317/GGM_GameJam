using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEnemy : EnemyBase
{
    protected override void ShootBullet()
    {
        int count = 4;
        float central = 20;

        Vector2 dir = (GameManager.Instance.curPlayer.transform.position - transform.position).normalized;
        float tarZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float amount = central / (count - 1);
        float z = central / -2f + (int)tarZ;

        for (int i = 0; i < count; i++)
        {
            Quaternion rot = Quaternion.Euler(0, 0, z - 90);
            Instantiate(bulletPrefab, transform.position, rot);
            z += amount;
        }
    }

    private void AttackStart()
    {
        canMove = false;
        agent.isStopped = true;
    }

    private void AttackEnd()
    {
        canMove = true;
        agent.isStopped = false;
    }
}