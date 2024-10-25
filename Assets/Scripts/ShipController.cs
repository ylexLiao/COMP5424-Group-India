using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Transform[] waypoints; // 路线目标点数组
    public float moveSpeed = 5f; // 初始移动速度
    public float minSpeed = 1f; // 最低移动速度
    public float stopThreshold = 3; // 停止移动的小兵数量阈值
    public float recoveryTime = 5f; // 恢复时间
    public float blockedSpeedMultiplier = 3f; // 每个小兵对速度的影响

    private int currentWaypointIndex = 0; // 当前目标点索引
    private List<GameObject> blockingEnemies = new List<GameObject>(); // 阻挡小兵列表
    private bool isStopped = false; // 是否停止前进
    private bool isRecovering = false; // 是否在恢复

    private HealthController healthController; // 引用生命值系统
    // private Animator animator; // 动画控制器
    private float originalSpeed; // 初始速度


    void Start()
    {
        // animator = GetComponent<Animator>();
        originalSpeed = moveSpeed;

        healthController = GetComponent<HealthController>();
        healthController.OnHealthChanged += UpdateHealthUI; // 注册生命值变化事件
        healthController.OnDeath += OnHorseDeath; // 注册死亡事件
    }

    // Update is called once per frame
    void Update()
    {
        // 检查马的阻挡状态和更新速度
        UpdateSpeedBasedOnBlockingEnemies();
        if (!isStopped && !isRecovering)
        {
            MoveAlongPath(); // 按照路线前进
        }
<<<<<<< HEAD

=======
        Debug.Log(blockingEnemies.Count);
>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a

    }
    // 按照固定路线移动的函数
    private void MoveAlongPath()
    {
        if (currentWaypointIndex >= waypoints.Length) return; // 如果所有目标点已到达，停止移动

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        // 更新单位面朝的方向
        RotateTowardsTarget(direction);

        // 更新马的移动位置
        transform.position += direction * moveSpeed * Time.deltaTime;

        // 检查是否到达目标点，如果到达则切换到下一个目标点
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }

    private void UpdateSpeedBasedOnBlockingEnemies()
    {
        if (blockingEnemies.Count > 0)
        {
            moveSpeed = originalSpeed - blockingEnemies.Count * blockedSpeedMultiplier;

            if (moveSpeed <= minSpeed || blockingEnemies.Count >= stopThreshold)
            {
                moveSpeed = 0f;
                isStopped = true;
                // animator.SetBool("isStopped", true);
            }
        }
        else
        {
            moveSpeed = originalSpeed;
            isStopped = false;
            // animator.SetBool("isStopped", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            blockingEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            blockingEnemies.Remove(other.gameObject);
        }
    }

<<<<<<< HEAD
    public void TakeDamage(float damage)
    {
        healthController.TakeDamage(damage); // 调用生命值系统的扣血方法
    }

    private void UpdateHealthUI(float currentHealth)
    {
        Debug.Log($"马当前生命值：{currentHealth}");
=======

    private void UpdateHealthUI(float currentHealth)
    {

>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a
    }

    private void OnHorseDeath()
    {
        Debug.Log("马死亡！");
        isStopped = true;
        isRecovering = true;
        // animator.SetTrigger("Die");

        StartCoroutine(RecoverAfterDeath());
    }

    private IEnumerator RecoverAfterDeath()
    {
        yield return new WaitForSeconds(recoveryTime);
        healthController.Heal(healthController.maxHealth); // 恢复生命值
        isStopped = false;
        isRecovering = false;
        // animator.SetTrigger("Recover");
    }

    private void RotateTowardsTarget(Vector3 direction)
    {
        if (direction != Vector3.zero) // 确保方向不是零向量
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }
    }
}
