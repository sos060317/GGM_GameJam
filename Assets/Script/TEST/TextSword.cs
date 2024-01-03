using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TextSword : MonoBehaviour
{
    public float scanRange;

    public LayerMask enemyLayer;

    public Action attackEvent;

    private bool isAttack;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        AttackUpdate();
    }

    private void AttackUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !isAttack)
        {
            isAttack = true;
            anim.SetTrigger("Attack");
            attackEvent?.Invoke();
        }
    }

    private void AttackDamage()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, scanRange, enemyLayer);

        foreach (var target in targets)
        {
            target.GetComponent<EnemyBase>().OnDamage(10);
        }
    }

    private void AttackEnd()
    {
        isAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, scanRange);
    }
}