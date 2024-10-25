using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    public Transform target;           // Ŀ�����
    public float attackRange = 2f;     // ������Χ
    public float attackCooldown = 2f;  // ������ȴʱ��
    public Animator animator;          // ����������
    private NavMeshAgent agent;        // �������
    private HealthController targetHealth;     // Ŀ���HealthController���
    private HealthController Health;

    private bool isAttacking = false;  // �Ƿ��ڹ���״̬
    private bool isDead = false;       // �Ƿ�����
    private bool isCooldown = false;       // �Ƿ�����
    private Coroutine attackCoroutine; // ��¼����Э�̵�����

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // ��ȡNavMeshAgent���
        animator = GetComponent<Animator>();   // ��ȡAnimator���
        target = GameObject.FindGameObjectWithTag("Ship")?.transform;
        targetHealth = target.GetComponent<HealthController>();  // ��ȡĿ���HealthController
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
            StopAttack();  // ֹͣ�������ȴ�Ŀ�긴��
            return;
        }

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange && !isAttacking)
        {
            // Ŀ���ڹ�����Χ�ڲ��Ҳ��ڹ���״̬
            agent.isStopped = true;
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);

            // ��������Э�̣�������Э������
            attackCoroutine = StartCoroutine(Attack());
        }
        else if (distanceToTarget > attackRange)
        {
            // ���Ŀ���뿪������Χ��ֹͣ����״̬
            if (isAttacking && attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine); // ֹͣ����Э��
                isAttacking = false;
            }

            // ���� Run ״̬
            agent.isStopped = false;
            animator.SetBool("isAttacking", false);
            animator.SetBool("isRunning", true);
            agent.SetDestination(target.position);
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;


        // �ȴ�1��ģ�⹥�������ĳ���ʱ��
        yield return new WaitForSeconds(1.13f);

        // ���ù���״̬�����Ŀ����Ȼ�ڷ�Χ�ڣ�������½��빥����
        StartCoroutine(Cooldown());
        isAttacking = false;
    }

    private IEnumerator Cooldown()
    {
        isCooldown = true;
        animator.SetBool("isCooldown", true); // ���Ź�������
        animator.SetBool("isAttacking", false); // ���Ź�������
        animator.SetBool("isRunning", false);
        yield return new WaitForSeconds(attackCooldown); // �ȴ���ȴʱ��
        animator.SetBool("isCooldown", false); // ���ش���״̬
        animator.SetBool("isAttacking", true); // ���Ź�������
        animator.SetBool("isRunning", false);
        isCooldown = false;
    }

    private void StopAttack()
    {
        if (isAttacking && attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);  // ֹͣ����Э��
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
        Destroy(gameObject, 3f);  // �ӳ�����
    }
}
