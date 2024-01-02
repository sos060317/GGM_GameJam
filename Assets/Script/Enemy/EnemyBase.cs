using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    #region �⺻ ����

    [Space(10)]
    [Header("�⺻ ����")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHealth;

    #endregion

    public Effect dieEffect;

    #region ���� ���� ����

    [Space(10)]
    [Header("���� ���� ����")]
    [SerializeField] private float attackRate;
    [SerializeField] protected EnemyBullet bulletPrefab;

    #endregion

    private float attackTimer;

    protected Transform target;

    NavMeshAgent agent;

    private void Start()
    {
        target = GameManager.Instance.curPlayer.transform;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;
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

    protected abstract void ShootBullet();
}
