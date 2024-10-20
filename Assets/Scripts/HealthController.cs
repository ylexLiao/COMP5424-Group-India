using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthController : MonoBehaviour
{
    public float maxHealth = 100f; // �������ֵ
    public float currentHealth; // ��ǰ����ֵ

    private Animator animator; // ����������

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // ��ʼ������ֵ
        animator = GetComponent<Animator>(); // ��ȡ�������
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ��Ѫ����������λ�ܵ��˺�ʱ����
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // ���ٵ�ǰ����ֵ

        // �����ܻ�������Ч��
        // animator.SetTrigger("TakeDamage");

        // ��鵥λ�Ƿ�����
        if (currentHealth <= 0)
        {
            Die(); // ִ�������߼�
        }
    }
    //�ָ�����
    public void AddHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Current Health: " + currentHealth);
    }


// ��λ����ʱ����
private void Die()
    {
        // ������������
        animator.SetTrigger("Die");

        // ���õ�λ������Ϊ���ƶ��������ȣ�
        GetComponent<EnermyController>().enabled = false;

        // �ӳ����ٶ�����������߼�
        Destroy(gameObject, 2f); // 2 ������ٵ�λ����
    }
}
