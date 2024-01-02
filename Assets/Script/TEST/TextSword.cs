using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSword : MonoBehaviour
{
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
        }
    }

    private void AttackEnd()
    {
        isAttack = false;
    }
}