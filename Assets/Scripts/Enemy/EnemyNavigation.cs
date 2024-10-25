using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public Transform target;           // 目标对象
    public float attackRange = 2f;     // 攻击范围
    public float attackCooldown = 2f;  // 攻击冷却时间
    public Animator animator;          // 动画控制器
    private NavMeshAgent agent;        // 导航组件
    private HealthController targetHealth;     // 目标的HealthController组件
    private HealthController Health;

    private bool isAttacking = false;  // 是否在攻击状态
    private bool isDead = false;       // 是否死亡
    private bool isCooldown = false;       // 是否死亡
    private Coroutine attackCoroutine; // 记录攻击协程的引用

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // 获取NavMeshAgent组件
        animator = GetComponent<Animator>();   // 获取Animator组件
        target = GameObject.FindGameObjectWithTag("Ship")?.transform;
        targetHealth = target.GetComponent<HealthController>();  // 获取目标的HealthController
        Health = GetComponent<HealthController>();
    }

    void Update()
    {
        if (Health.currentHealth <= 0)
        {
            Die();
        }

        if (isDead) return;

        if (isCooldown) return;

        if (targetHealth.currentHealth <= 0)
        {
            StopAttack();  // 停止攻击并等待目标复活
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange && !isAttacking)
        {
            // 目标在攻击范围内并且不在攻击状态
            agent.isStopped = true;
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);

            // 启动攻击协程，并保存协程引用
            attackCoroutine = StartCoroutine(Attack());
        }
        else if (distanceToTarget > attackRange)
        {
            // 如果目标离开攻击范围，停止攻击状态
            if (isAttacking && attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine); // 停止攻击协程
                isAttacking = false;
            }

            // 进入 Run 状态
            agent.isStopped = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isRunning", true);
            agent.SetDestination(target.position);
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;


        // 等待1秒模拟攻击动画的持续时间
        yield return new WaitForSeconds(1.13f);

        // 重置攻击状态（如果目标依然在范围内，则会重新进入攻击）
        StartCoroutine(Cooldown());
        isAttacking = false;
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        animator.SetBool("isCooldown", true); // 播放攻击动画
        animator.SetBool("isAttacking", false); // 播放攻击动画
        animator.SetBool("isRunning", false);
        yield return new WaitForSeconds(attackCooldown); // 等待冷却时间
        animator.SetBool("isCooldown", false); // 返回待机状态
        animator.SetBool("isAttacking", true); // 播放攻击动画
        animator.SetBool("isRunning", false);
        isCooldown = false;
    }

    private void StopAttack()
    {
        if (isAttacking && attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);  // 停止攻击协程
            attackCoroutine = null;
        }

        isAttacking = false;
        animator.SetBool("isAttacking", false);
        animator.SetBool("isCooldown", true);
    }

    private void Die()
    {
        isDead = true;
        agent.isStopped = true;
        animator.SetTrigger("Die");
        Destroy(gameObject, 3f);  // 延迟销毁
    }
}
