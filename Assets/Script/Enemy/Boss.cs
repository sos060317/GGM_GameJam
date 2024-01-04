using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    public float skillMinTime;
    public float skillMaxTime;

    public EnemyParrernBullet[] patternBullets;

    private float skillRate;
    private float skillTimer;

    private bool isSkilling;

    protected override void Start()
    {
        base.Start();

        skillRate = Random.Range(skillMinTime, skillMaxTime);
    }

    protected override void Update()
    {
        base.Update();

        SkillUpdate();
    }

    private void SkillUpdate()
    {
        if (isSkilling)
        {
            return;
        }

        if (skillTimer >= skillRate && !isSkilling)
        {
            anim.SetTrigger("AttackReady");
            skillRate = Random.Range(skillMinTime, skillMaxTime);
            skillTimer = 0;
            isSkilling = true;
            canMove = false;
            agent.isStopped = true;
        }

        skillTimer += Time.deltaTime;
    }

    protected override void AttackUpdate()
    {
        if (attackTimer >= attackRate)
        {
            ShootBullet();
            attackRate = Random.Range(attackMinRate, attackMaxRate);

            attackTimer = 0;
        }

        attackTimer += Time.deltaTime;
    }

    protected override void ShootBullet()
    {
        Vector2 dir = target.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Instantiate(patternBullets[Random.Range(0, patternBullets.Length)], transform.position, Quaternion.Euler(0, 0, angle - 180)).Init(dir);
    }

    private void ThinkPattern()
    {
        StartCoroutine(Pattern01());
    }

    // 일반 총알 전체 공격 점점 돌아감
    private IEnumerator Pattern01()
    {
        int shotCount = 10;
        int bulletCount = 36;

        float offset = .0f;

        GameManager.Instance.CameraShake(20, 2f);

        for (int i = 0; i < shotCount; i++)
        {
            for (int j = 0; j < bulletCount; j++)
            {
                Vector2 dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * j / bulletCount + offset),
                                          Mathf.Sin(Mathf.PI * 2 * j / bulletCount + offset));

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
            }

            offset += 1;

            yield return new WaitForSeconds(0.3f);
        }

        isSkilling = false;
        AttackEnd();
    }

    private void AttackStart()
    {
        ThinkPattern();
        anim.SetBool("AttackEnd", false);
    }

    private void AttackEnd()
    {
        canMove = true;
        agent.isStopped = false;
        anim.SetBool("AttackEnd", true);
    }
}