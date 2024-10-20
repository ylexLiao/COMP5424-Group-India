using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform target; // 目标对象
    public float attackRange = 2f; // 攻击范围
    public float attackCooldown = 1f; // 攻击冷却时间
    public float attackDamage = 10f; // 攻击造成的伤害
    public float groundOffset = 1f; // 单位与地面保持的高度偏移（可以根据需要调整）

    private bool isAttacking = false; // 判断单位是否正在攻击
    private NavMeshAgent agent; // NavMeshAgent 组件引用
    private HealthController healthController;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // 获取 NavMeshAgent 组件
        healthController = GetComponent<HealthController>();
        healthController.OnDeath += OnEnemyDeath;
        target = GameObject.FindGameObjectWithTag("Ship")?.transform;

        // 设置 NavMeshAgent 的参数
        if (agent != null)
        {
            agent.stoppingDistance = attackRange; // 设置停止距离
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return; // 如果目标为空，则返回

        // 检查单位和目标的距离
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // 如果目标在攻击范围内且不在攻击状态
        if (distanceToTarget <= attackRange && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        else if (distanceToTarget > attackRange && agent.enabled)
        {
            // 如果不在攻击范围内，使用 NavMeshAgent 移动到目标
            agent.SetDestination(target.position);
        }
    }

    // 攻击目标的协程
    private IEnumerator Attack()
    {
        isAttacking = true; // 标记为攻击状态
        agent.isStopped = true; // 停止移动

        // 在这里实现攻击的逻辑，例如播放动画或者减少目标的血量
        Debug.Log("Attacking the target!");

        // 对目标单位造成伤害
        if (target != null)
        {
            HealthController targetHealth = target.GetComponent<HealthController>();
            if (targetHealth != null && targetHealth.GetCurrentHealth() > 0) // 确保目标还活着
            {
                targetHealth.TakeDamage(attackDamage); // 对目标造成伤害
                Debug.Log($"造成 {attackDamage} 点伤害给 {target.name}");
            }
            else
            {
                Debug.Log($"{target.name} 已经死亡，无法攻击。");
            }
        }

        yield return new WaitForSeconds(attackCooldown); // 等待攻击冷却时间

        isAttacking = false; // 结束攻击状态
        agent.isStopped = false; // 允许移动
    }

    public void TakeDamage(float damage)
    {
        healthController.TakeDamage(damage);
    }

    private void OnEnemyDeath()
    {
        Debug.Log("小兵死亡！");
        Destroy(gameObject); // 小兵死亡时销毁自身
    }
}
