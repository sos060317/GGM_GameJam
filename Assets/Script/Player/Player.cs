using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float maxHealth;
    public float rushSkillScanRange;
    public float invincibilityTime;
    public LayerMask enemyLayer;
    public Transform swordParent;
    public TextSword sword;
    public Light2D light;
    public AudioClip dashSound;

    #region ´ë½¬ °ü·Ã ½ºÅÈ

    [Space(10)]
    [Header("´ë½¬ °ü·Ã ½ºÅÈ")]
    [SerializeField] private float dashTime;
    [SerializeField] private KeyCode dashKey = KeyCode.LeftShift;
    [SerializeField] private float dashCoolTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float afterImageDistance;

    #endregion

    [SerializeField] private AfterImage afterImagePrefab;

    private float curHealth;
    private float dashTimer;
    private float baseMoveSpeed;

    private bool isWalk;
    private bool isDash;
    private bool canDash;
    private bool isInvincibility;

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
        baseMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.isStop)    
        {
            rigid.velocity = Vector2.zero;
            anim.SetBool("isWalk", false);
            return;
        }

        InputUpdate();
        MoveUpdate();
        DashUpdate();
        SwordRotationUpdate();
        HealthBarUpdate();
        AnimationUpdate();
        LightUpdate();
        HealthUpdate();
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
            sr.flipX = true;
        }
        else
        {
            swordParent.localScale = new Vector3(1, 1, 1);
            sr.flipX = false;
        }
        swordParent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private IEnumerator DashRoutine()
    {
        float timer = 0f;

        isDash = true;
        canDash = false;
        gameObject.layer = 9;

        Vector3 lastAfterImagePos = transform.position;

        Instantiate(afterImagePrefab, lastAfterImagePos, Quaternion.identity).InitAfterImage(sr.sprite, sr.flipX);

        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        if (moveDirection == Vector2.zero)
        {
            moveDirection.y = 1;
        }

        rigid.velocity = moveDirection.normalized * dashSpeed;

        SoundManager.Instance.PlaySound(dashSound);

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
        gameObject.layer = 8;

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

        gameObject.layer = 9;

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

            enemise = enemise.Distinct().ToList();

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

        gameObject.layer = 8;

        rigid.velocity = Vector3.zero;
    }

    private void HealthBarUpdate()
    {
        GameManager.Instance.SetHealthUI(curHealth, maxHealth); 
    }

    private void AnimationUpdate()
    {
        anim.SetBool("isWalk", isWalk);
    }

    private void LightUpdate()
    {
        light.intensity = curHealth / maxHealth;
    }

    private void HealthUpdate()
    {
        curHealth = Mathf.Max(curHealth - Time.deltaTime * GameManager.Instance.playerHealthMultiply, 0);

        if (curHealth <= 0)
        {
            GameManager.Instance.GameOver();

            Destroy(gameObject);
        }
    }

    public void Init(PlayerLevelData data)
    {
        maxHealth = data.health;
        moveSpeed = data.moveSpeed;
        sword.damage = data.damage;
    }

    public void OnDamege(float damage)
    {
        if (isInvincibility || isDash)
        {
            return;
        }

        curHealth = Mathf.Max(curHealth - damage, 0);

        GameManager.Instance.SetHealthUI(curHealth, maxHealth);

        if (curHealth <= 0)
        {
            // Á×´Â ·ÎÁ÷

            GameManager.Instance.GameOver();

            Destroy(gameObject);

            return;
        }

        GameManager.Instance.CameraShake(20, 0.3f);

        StartCoroutine(HitRoutine());
    }

    Coroutine slowCoroutine;
    public void OnDamageSlow(float damage, float time)
    {
        if (isInvincibility || isDash)
        {
            return;
        }

        curHealth = Mathf.Max(curHealth - damage, 0);

        GameManager.Instance.SetHealthUI(curHealth, maxHealth);

        if (curHealth <= 0)
        {
            // Á×´Â ·ÎÁ÷

            GameManager.Instance.GameOver();

            Destroy(gameObject);

            return;
        }

        GameManager.Instance.CameraShake(20, 0.3f);

        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        StartCoroutine(HitRoutine());
        slowCoroutine = StartCoroutine(SlowRoutine(time));
    }

    private IEnumerator HitRoutine()
    {
        isInvincibility = true;
        sr.color = new Color(1, 1, 1, 0.2f);

        yield return new WaitForSeconds(invincibilityTime);

        isInvincibility = false;
        sr.color = new Color(1, 1, 1, 1);
    }

    private IEnumerator SlowRoutine(float time)
    {
        moveSpeed = baseMoveSpeed * 0.3f;

        yield return new WaitForSeconds(time);

        moveSpeed = baseMoveSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, rushSkillScanRange);
    }

    public void Heal(float heal)
    {
        curHealth = Mathf.Min(maxHealth, curHealth + heal);
    }
}