using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyController : MonoBehaviour
{
    public Transform target; // 目标对象
    public float moveSpeed = 3f; // 移动速度
    private float currentSpeed;
    private bool isSlowed = false;//减速效果
    public float attackRange = 2f; // 攻击范围
    public float attackCooldown = 1f; // 攻击冷却时间
    public float attackDamage = 10f; // 攻击造成的伤害
    public float groundOffset = 0.5f; // 单位与地面保持的高度偏移（可以根据需要调整）

    private bool isAttacking = false; // 判断单位是否正在攻击
    private bool isMoving = true; // 判断单位是否可以移动
    private Animator animator; // 动画控制器引用

    // Start is called before the first frame update
    void Start()
    {
        // 获取单位上的 Animator 组件
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TowardsTarget();

        // 检查单位和目标的距离
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // 如果目标在攻击范围内且不在攻击状态
        if (distanceToTarget <= attackRange && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        else if (distanceToTarget > attackRange && isMoving)
        {
            // 如果不在攻击范围内，并且单位允许移动，则向目标移动
            MoveToTarget();
            // 切换到移动动画
            animator.SetBool("isAttacking", false);
        }
    }




        // 面向目标
    private void TowardsTarget()
    {
        // 计算单位到目标的方向，并以该方向移动
        Vector3 direction = (target.position - transform.position).normalized;

        // 更新单位面朝的方向
        RotateTowardsTarget(direction);
    }

        // 向目标移动的函数
    private void MoveToTarget()
    {
        // 计算单位到目标的方向，并以该方向移动
        Vector3 direction = (target.position - transform.position).normalized;

        // 更新单位面朝的方向
        RotateTowardsTarget(direction);

        // 检测地面高度并将单位位置保持在地面上
        AdjustToGround();

        // 移动单位的位置
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    //减速
    public void ApplySlowdown(float factor, float duration)
    {
        if (!isSlowed)
        {
            StartCoroutine(SlowdownCoroutine(factor, duration));
        }
    }

    private IEnumerator SlowdownCoroutine(float factor, float duration)
    {
        isSlowed = true;
        currentSpeed *= factor;
        yield return new WaitForSeconds(duration);
        currentSpeed = moveSpeed;
        isSlowed = false;
    }



    // 使单位面朝移动方向的函数
    private void RotateTowardsTarget(Vector3 direction)
    {
        if (direction != Vector3.zero) // 确保方向不是零向量
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }
    }


    // 调整单位位置以保持其靠近地面
    private void AdjustToGround()
    {
        RaycastHit hit;

        // 从单位位置向下发射射线，检测地面位置
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            // 将单位的 y 坐标调整为地面高度，并加上偏移量（groundOffset）
            transform.position = new Vector3(transform.position.x, hit.point.y + groundOffset, transform.position.z);
        }
    }

    // 攻击目标的协程
    private IEnumerator Attack()
    {
        isAttacking = true; // 标记为攻击状态
        isMoving = false; // 攻击时不移动

        // 切换到攻击动画
        animator.SetBool("isAttacking", true);

        // 在这里实现攻击的逻辑，例如播放动画或者减少目标的血量
        Debug.Log("Attacking the target!");

        // 对目标单位造成伤害
        if (target != null)
        {
            // 获取目标的 UnitHealth 组件，并对其造成伤害
            HealthController targetHealth = target.GetComponent<HealthController>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage); // 对目标造成伤害
            }
        }

        yield return new WaitForSeconds(attackCooldown); // 等待攻击冷却时间

        isAttacking = false; // 结束攻击状态
        isMoving = true; // 允许移动

        // 切换回移动动画
        // animator.SetBool("isAttacking", false);
    }

}
