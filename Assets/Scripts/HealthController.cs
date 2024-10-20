using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class HealthController : MonoBehaviour
{
    public float maxHealth = 100f;  // �������ֵ
    private float currentHealth;

    public event Action<float> OnHealthChanged; // ����ֵ�仯�¼�
    public event Action OnDeath; // �����¼�

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // ��ʼ����ǰ����ֵ
    }

    // Update is called once per frame
    void Update()
    {

    }

    // ��Ѫ����
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ��������ֵ��Χ
        OnHealthChanged?.Invoke(currentHealth); // ��������ֵ�仯�¼�

        if (currentHealth <= 0)
        {
            OnDeath?.Invoke(); // ���������¼�
        }
    }

    // �ظ�����ֵ����
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ��������ֵ��Χ
        OnHealthChanged?.Invoke(currentHealth); // ��������ֵ�仯�¼�
    }

    // ��ȡ��ǰ����ֵ
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
