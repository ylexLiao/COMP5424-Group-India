using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthController : MonoBehaviour
{
    public float maxHealth = 100f; // 最大生命值
    public float currentHealth; // 当前生命值
    public Text healthText;

    private Animator animator; // 动画控制器

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // 初始化生命值
        animator = GetComponent<Animator>(); // 获取动画组件
        UpdateHealthText();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 扣血函数，当单位受到伤害时调用
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // 减少当前生命值
        UpdateHealthText();


        // 触发受击动画或效果
        // animator.SetTrigger("TakeDamage");

        // 检查单位是否死亡
        if (currentHealth <= 0)
        {
            Die(); // 执行死亡逻辑
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString(); // 更新显示的生命值
        }
    }

    // 单位死亡时调用
    private void Die()
    {
        // 设置死亡动画
        animator.SetTrigger("Die");

        // 禁用单位所有行为（移动、攻击等）
        GetComponent<EnermyController>().enabled = false;

        // 延迟销毁对象或处理死亡逻辑
        Destroy(gameObject, 2f); // 2 秒后销毁单位对象
    }
}
