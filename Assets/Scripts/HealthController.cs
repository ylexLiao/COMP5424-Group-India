using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthController : MonoBehaviour
{
    public float maxHealth = 100f; // �������ֵ
    public float currentHealth; // ��ǰ����ֵ
    public Text healthText;

    private Animator animator; // ����������

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // ��ʼ������ֵ
        animator = GetComponent<Animator>(); // ��ȡ�������
        UpdateHealthText();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ��Ѫ����������λ�ܵ��˺�ʱ����
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // ���ٵ�ǰ����ֵ
        UpdateHealthText();


        // �����ܻ�������Ч��
        // animator.SetTrigger("TakeDamage");

        // ��鵥λ�Ƿ�����
        if (currentHealth <= 0)
        {
            Die(); // ִ�������߼�
        }
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth.ToString(); // ������ʾ������ֵ
        }
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
