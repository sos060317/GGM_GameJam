using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    #region 기본 스탯

    [Space(10)]
    [Header("기본 스탯")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHealth;

    #endregion

    public GameObject dieEffect;
    public GameObject hitEffect;

    #region 공격 관련 스탯

    [Space(10)]
    [Header("공격 관련 스탯")]
    [SerializeField] private float attackMinRate;
    [SerializeField] private float attackMaxRate;
    [SerializeField] protected EnemyBullet bulletPrefab;

    #endregion

    private float attackTimer;
    private float attackRate;
    private float curHealth;

    protected Transform target;

    protected bool canMove = true;

    protected NavMeshAgent agent;
    Rigidbody2D rigid;
    Animator anim;

    private void Start()
    {
        target = GameManager.Instance.curPlayer.transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;

        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        curHealth = maxHealth;
        attackRate = Random.Range(attackMinRate, attackMaxRate);
    }

    private void Update()
    {
        MoveUpdate();
        AttackUpdate();
    }

    private void MoveUpdate()
    {
        if (!canMove)
        {
            return;
        }

        agent.SetDestination(target.position);
    }

    private void AttackUpdate()
    {
        if (attackTimer >= attackRate)
        {
            anim.SetTrigger("Attack");
            attackRate = Random.Range(attackMinRate, attackMaxRate);

            attackTimer = 0;
        }

        attackTimer += Time.deltaTime;
    }

    public void OnDamage(float damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            GameManager.Instance.CameraShake(20, 0.4f);
            GameManager.Instance.ShowEffectImage(0.1f, 1);
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        StartCoroutine(KnockbackRoutine());

        Instantiate(hitEffect, transform.position, Quaternion.identity);

        GameManager.Instance.CameraShake(30, 0.1f);
    }

    // 죽으면 true 안 죽으면 false를 반환
    public bool OnDamageCheck(float damage)
    {
        curHealth -= damage;

        if (curHealth <= 0)
        {
            GameManager.Instance.CameraShake(20, 0.4f);
            GameManager.Instance.ShowEffectImage(0.1f, 1);
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return true;
        }

        StartCoroutine(KnockbackRoutine());

        Instantiate(hitEffect, transform.position, Quaternion.identity);

        GameManager.Instance.CameraShake(30, 0.1f);

        return false;
    }

    private IEnumerator KnockbackRoutine()
    {
        Vector2 dir = transform.position - target.position;

        rigid.AddForce(dir.normalized * 10, ForceMode2D.Impulse);

        Debug.Log("공격받음");

        yield return new WaitForSeconds(0.1f);

        rigid.velocity = Vector2.zero;
    }

    protected abstract void ShootBullet();
}
