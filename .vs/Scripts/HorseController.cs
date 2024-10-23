using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public Transform[] waypoints; // 固定路线的目标点数组
    public float moveSpeed = 5f; // 马的初始移动速度
    public float minSpeed = 1f; // 最低移动速度
    public float stopThreshold = 3; // 停止移动的小兵数量阈值
    public float health = 100f; // 马的初始生命值
    public float maxHealth = 100f; // 马的最大生命值
    public float recoveryTime = 5f; // 马的恢复时间
    public float blockedSpeedMultiplier = 0.1f; // 每个小兵对马移动速度的影响

    private int currentWaypointIndex = 0; // 当前目标点索引
    private List<GameObject> blockingEnemies = new List<GameObject>(); // 阻挡马的小兵列表
    private bool isStopped = false; // 马是否停止前进
    private bool isRecovering = false; // 马是否在恢复血量

    //----------暂时没给马做动画--------------//
    // private Animator animator; // 动画控制器
    private float originalSpeed; // 保存马的初始速度

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        originalSpeed = moveSpeed; // 保存马的初始速度
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped && !isRecovering)
        {
            MoveAlongPath(); // 按照路线前进
        }

        // 检查马的阻挡状态和更新速度
        UpdateSpeedBasedOnBlockingEnemies();
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

    // 检查和更新马的速度，基于小兵的数量
    private void UpdateSpeedBasedOnBlockingEnemies()
    {
        // 根据小兵数量调整马的移动速度
        if (blockingEnemies.Count > 0)
        {
            moveSpeed = originalSpeed - blockingEnemies.Count * blockedSpeedMultiplier;

            // 如果马的速度低于最小速度，则停止
            if (moveSpeed <= minSpeed || blockingEnemies.Count >= stopThreshold)
            {
                moveSpeed = 0f;
                isStopped = true;
                // animator.SetBool("isStopped", true); // 播放停止动画
            }
        }
        else
        {
            moveSpeed = originalSpeed;
            isStopped = false;
            // animator.SetBool("isStopped", false); // 恢复移动动画
        }
    }

    // 当有小兵进入马的阻挡范围时，触发此函数
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            blockingEnemies.Add(other.gameObject);
        }
    }

    // 当小兵离开马的阻挡范围时，触发此函数
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            blockingEnemies.Remove(other.gameObject);
        }
    }

    // 受击处理函数，当马受到小兵攻击时调用
    public void TakeDamage(float damage)
    {
        if (isRecovering) return; // 恢复过程中不再受到伤害

        health -= damage;
        if (health <= 0)
        {
            health = 0;
            StopAndRecover(); // 当血量耗尽时，停止前进并恢复
        }
    }

    // 停止并开始恢复血量
    private void StopAndRecover()
    {
        isStopped = true;
        isRecovering = true;
        // animator.SetTrigger("Die"); // 播放马受击或倒下动画

        // 停留指定时间后恢复血量
        StartCoroutine(RecoverHealth());
    }

    // 恢复血量的协程
    private IEnumerator RecoverHealth()
    {
        yield return new WaitForSeconds(recoveryTime); // 停留恢复时间

        health = maxHealth; // 恢复血量
        isStopped = false;
        isRecovering = false;
        // animator.SetTrigger("Recover"); // 播放恢复动画
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
