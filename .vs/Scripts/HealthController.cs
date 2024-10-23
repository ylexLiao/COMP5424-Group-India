using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthController : MonoBehaviour
{
    public float maxHealth = 100f; // �������ֵ
    public float currentHealth; // ��ǰ����ֵ
    public Slider healthSlider;//UI
    private Animator animator; // ����������

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // ��ʼ������ֵ
        animator = GetComponent<Animator>(); // ��ȡ�������
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;//UI�����ʼ��
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ��Ѫ����������λ�ܵ��˺�ʱ����
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // ���ٵ�ǰ����ֵ
        healthSlider.value = currentHealth;
        // �����ܻ�������Ч��
        // animator.SetTrigger("TakeDamage");

        // ��鵥λ�Ƿ�����
        if (currentHealth <= 0)
        {
            Die(); // ִ�������߼�
        }
    }




    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        Debug.Log("��ǰ����ֵ��" + currentHealth);
        healthSlider.value = currentHealth;
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
