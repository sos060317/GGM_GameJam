using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalEnemy01 : EnemyBase
{
    public AudioClip attackSound;

    protected override void ShootBullet()
    {
        SoundManager.Instance.PlaySound(attackSound);

        float angle = Mathf.Atan2(transform.position.y - target.position.y, 
                                  transform.position.x - target.position.x) * Mathf.Rad2Deg;

        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle - 180));
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
