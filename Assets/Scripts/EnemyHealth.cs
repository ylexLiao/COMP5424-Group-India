using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider hps;
    public TMPro.TMP_Text hplb;
    public int health = 100;  // 敌人的初始生命值
    int maxHP;
    private void Awake()
    {
        maxHP = health;
        updateUI();
    }
    // 减少敌人的生命值
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

    // 处理敌人死亡
    void Die()
    {
        Destroy(GetComponentInChildren<Hit>());
        Destroy(GetComponentInChildren<Npc>());
        Destroy(GetComponentInChildren<NavMeshAgent>());
        GetComponentInChildren<Animator>().SetTrigger("die");

        //播放死亡动画
        Destroy(gameObject,5);  // 销毁敌人对象
    }

    void updateUI()
    {
        hplb.text = health + "/" + maxHP;
        hps.value = (float)health / maxHP;
    }

    private void Update()
    {
        //测试掉血
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TakeDamage(30);
        }
    }
}
