using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider hps;
    public TMPro.TMP_Text hplb;
    public int health = 100;  // ���˵ĳ�ʼ����ֵ
    int maxHP;
    private void Awake()
    {
        maxHP = health;
        updateUI();
    }
    // ���ٵ��˵�����ֵ
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        updateUI();
    }

    // �����������
    void Die()
    {
        Destroy(GetComponentInChildren<Hit>());
        Destroy(GetComponentInChildren<Npc>());
        Destroy(GetComponentInChildren<NavMeshAgent>());
        GetComponentInChildren<Animator>().SetTrigger("die");

        //������������
        Destroy(gameObject,5);  // ���ٵ��˶���
    }

    void updateUI()
    {
        hplb.text = health + "/" + maxHP;
        hps.value = (float)health / maxHP;
    }

    private void Update()
    {
        //���Ե�Ѫ
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TakeDamage(30);
        }
    }
}
