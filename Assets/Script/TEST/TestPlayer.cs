using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float moveSpeed;
    public Transform swordParent;

    private bool isWalk;

    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer sr;

    private Vector2 moveDirection;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveUpdate();
        SwordRotationUpdate();
        AnimationUpdate();
    }

    private void MoveUpdate()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        if (moveDirection.x < 0)
        {
            sr.flipX = true;
        }
        else if (moveDirection.x > 0)
        {
            sr.flipX = false;
        }

        if (moveDirection != Vector2.zero)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }

        rigid.velocity = moveDirection.normalized * moveSpeed;
    }

    private void SwordRotationUpdate()
    {
        var target = transform.position;
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(angle) > 90)
        {
            swordParent.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            swordParent.localScale = new Vector3(1, 1, 1);
        }
        swordParent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void AnimationUpdate()
    {
        anim.SetBool("isWalk", isWalk);
    }
}
