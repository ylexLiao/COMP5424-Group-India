using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class HealthController : MonoBehaviour
{
    public float maxHealth = 100f;  // 最大生命值
    private float currentHealth;

    public event Action<float> OnHealthChanged; // 生命值变化事件
    public event Action OnDeath; // 死亡事件

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // 初始化当前生命值
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 扣血方法
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 限制生命值范围
        OnHealthChanged?.Invoke(currentHealth); // 触发生命值变化事件

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke(); // 触发死亡事件
        }
    }

    // 回复生命值方法
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 限制生命值范围
        OnHealthChanged?.Invoke(currentHealth); // 触发生命值变化事件
    }

    // 获取当前生命值
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
