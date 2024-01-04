using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    public float skillMinTime;
    public float skillMaxTime;

    public EnemyParrernBullet[] patternBullets;

    public AudioClip readySound;
    public AudioClip attackSound;

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
            SoundManager.Instance.PlaySound(readySound);
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

    // �Ϲ� �Ѿ� ��ü ���� ���� ���ư�
    private IEnumerator Pattern01()
    {
        SoundManager.Instance.PlaySound(attackSound);

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

    public override void OnDamage(float damage)
    {
        if (isDie)
        {
            return;
        }

        curHealth -= damage;

        if (curHealth <= 0)
        {
            GameManager.Instance.CameraShake(20, 0.4f);
            GameManager.Instance.ShowEffectImage(0.1f, 1);
            SoundManager.Instance.PlaySound(dieSound);
            GameManager.Instance.GameClear();
            GameManager.Instance.gold += Random.Range(minGold, maxGold);
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            isDie = true;
            MapManager.Instance.EnemyDeathCountPlus();
            return;
        }

        SoundManager.Instance.PlaySound(hitSound, Random.Range(0.8f, 1.1f));

        StartCoroutine(KnockbackRoutine());

        Instantiate(hitEffect, transform.position, Quaternion.identity);

        GameManager.Instance.CameraShake(15, 0.1f);
    }

    public override bool OnDamageCheck(float damage)
    {
        if (isDie)
        {
            return true;
        }

        curHealth -= damage;

        if (curHealth <= 0)
        {
            GameManager.Instance.CameraShake(20, 0.4f);
            GameManager.Instance.ShowEffectImage(0.1f, 1);
            SoundManager.Instance.PlaySound(dieSound);
            GameManager.Instance.gold += Random.Range(minGold, maxGold);
            GameManager.Instance.GameClear();
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            isDie = true;
            MapManager.Instance.EnemyDeathCountPlus();
            return true;
        }

        SoundManager.Instance.PlaySound(hitSound, Random.Range(0.8f, 1.1f));

        StartCoroutine(KnockbackRoutine());

        Instantiate(hitEffect, transform.position, Quaternion.identity);

        GameManager.Instance.CameraShake(30, 0.1f);

        return false;
    }
}