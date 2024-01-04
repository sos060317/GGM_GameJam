using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyBase
{
    public float skillMinTime;
    public float skillMaxTime;

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
        }

        skillTimer += Time.deltaTime;
    }

    protected override void AttackUpdate()
    {
         
    }

    protected override void ShootBullet()
    {

    }

    private void AttackStart()
    {
        canMove = false;
        agent.isStopped = true;
        anim.SetBool("AttackEnd", false);
    }

    private void AttackEnd()
    {
        canMove = true;
        agent.isStopped = false;
        anim.SetBool("AttackEnd", true);
    }
}