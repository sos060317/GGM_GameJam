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

    public Effect dieEffect;

    #region 공격 관련 스탯

    [Space(10)]
    [Header("공격 관련 스탯")]
    [SerializeField] private float attackRate;
    [SerializeField] protected EnemyBullet bulletPrefab;

    #endregion

    private float attackTimer;

    protected Transform target;

    NavMeshAgent agent;
    Rigidbody2D rigid;

    private void Start()
    {
        target = GameManager.Instance.curPlayer.transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;

        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveUpdate();
        AttackUpdate();
    }

    private void MoveUpdate()
    {
        agent.SetDestination(target.position);
    }

    private void AttackUpdate()
    {
        if (attackTimer >= attackRate)
        {
            ShootBullet();

            attackTimer = 0;
        }

        attackTimer += Time.deltaTime;
    }

    public void OnDamage(float damage)
    {
        StartCoroutine(KnockbackRoutine());

        GameManager.Instance.CameraShake(15, 0.1f);
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
