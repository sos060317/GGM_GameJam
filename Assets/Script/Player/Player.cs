using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float maxHealth;
    public float rushSkillScanRange;
    public LayerMask enemyLayer;
    public Transform swordParent;
    public TextSword sword;

    #region �뽬 ���� ����

    [Space(10)]
    [Header("�뽬 ���� ����")]
    [SerializeField] private float dashTime;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] private float dashCoolTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float afterImageDistance;

    #endregion

    [SerializeField] private AfterImage afterImagePrefab;

    private float curHealth;
    private float dashTimer;

    private bool isWalk;
    private bool isDash;
    private bool canDash;

    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer sr;
    private TrailRenderer tr;

    private Vector2 moveDirection;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();

        curHealth = maxHealth;
    }

    private void Update()
    {
        InputUpdate();
        MoveUpdate();
        DashUpdate();
        SwordRotationUpdate();
        AnimationUpdate();
    }

    private void InputUpdate()
    {
        // Dash
        if (Input.GetKeyDown(dashKey) && canDash && !isDash)
        {
            StartCoroutine(DashRoutine());
        }
    }

    private void DashUpdate()
    {
        if (isDash)
        {
            return;
        }

        dashTimer -= Time.deltaTime;

        if (dashTimer <= 0)
        {
            canDash = true;
        }
    }

    private void MoveUpdate()
    {
        if (isDash)
        {
            return;
        }

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

    private IEnumerator DashRoutine()
    {
        float timer = 0f;

        isDash = true;
        canDash = false;

        Vector3 lastAfterImagePos = transform.position;

        Instantiate(afterImagePrefab, lastAfterImagePos, Quaternion.identity).InitAfterImage(sr.sprite, sr.flipX);

        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        if (moveDirection == Vector2.zero)
        {
            moveDirection.y = 1;
        }

        rigid.velocity = moveDirection.normalized * dashSpeed;

        while (timer <= dashTime)
        {
            if (Vector3.Distance(transform.position, lastAfterImagePos) >= afterImageDistance)
            {
                lastAfterImagePos = transform.position;

                var afterImage = Instantiate(afterImagePrefab, lastAfterImagePos, Quaternion.identity);
                afterImage.InitAfterImage(sr.sprite, sr.flipX);
            }

            timer += Time.deltaTime;

            yield return null;
        }

        isDash = false;

        dashTimer = dashCoolTime;

        rigid.velocity = Vector3.zero;
    }

    public void RushSkill(SkillBase skillBase, float damage)
    {
        StartCoroutine(RushSkillRoutine(skillBase, damage));
    }

    private IEnumerator RushSkillRoutine(SkillBase skillBase, float damage)
    {
        isDash = true;
        tr.emitting = true;

        GameManager.Instance.ShowEffectImage(0.1f, 1);

        float timer = 0f;

        List<Collider2D> enemise = new List<Collider2D>();

        Vector2 mousePos = Input.mousePosition;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 dir = mouseWorldPos - transform.position;

        rigid.velocity = dir.normalized * dashSpeed;

        while (timer <= dashTime)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, rushSkillScanRange, enemyLayer);

            foreach (var target in targets)
            {
                enemise.Add(target);
            }

            enemise.Distinct();

            timer += Time.deltaTime;

            yield return null;
        }

        isDash = false;
        tr.emitting = false;

        foreach (var enemy in enemise)
        {
            if (enemy.GetComponent<EnemyBase>().OnDamageCheck(damage))
            {
                GameManager.Instance.SkillCooltimeReset(skillBase);
            }
        }

        rigid.velocity = Vector3.zero;
    }

    private void AnimationUpdate()
    {
        anim.SetBool("isWalk", isWalk);
    }

    public void OnDamge(float damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, rushSkillScanRange);
    }
}