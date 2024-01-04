using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    #region ±âº» ½ºÅÈ

    [Space(10)]
    [Header("±âº» ½ºÅÈ")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHealth;

    #endregion

    public GameObject dieEffect;
    public GameObject hitEffect;

    #region °ø°Ý °ü·Ã ½ºÅÈ

    [Space(10)]
    [Header("°ø°Ý °ü·Ã ½ºÅÈ")]
    [SerializeField] protected float attackMinRate;
    [SerializeField] protected float attackMaxRate;
    [SerializeField] protected GameObject bulletPrefab;

    #endregion

    protected float attackTimer;
    protected float attackRate;
    private float curHealth;

    protected Transform target;

    protected bool canMove = true;

    protected NavMeshAgent agent;
    Rigidbody2D rigid;
    protected Animator anim;

    protected virtual void Start()
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

    protected virtual void Update()
    {
        if (GameManager.Instance.isStop)
        {
            return;
        }

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

    protected virtual void AttackUpdate()
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

        GameManager.Instance.CameraShake(15, 0.1f);
    }

    // Á×À¸¸é true ¾È Á×À¸¸é false¸¦ ¹ÝÈ¯
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

        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        rigid.AddForce(dir.normalized * 10, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f);

        rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        rigid.velocity = Vector2.zero;
    }

    protected abstract void ShootBullet();
}
