using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;  // 子弹的伤害

    void Start()
    {
        // 5秒后自动销毁子弹，防止子弹飞行过远或丢失未销毁
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // 对敌人造成伤害
                Debug.Log("Enemy hit! Dealt " + damage + " damage.");
            }
            else
            {
                Debug.LogError("Enemy does not have EnemyHealth component!");
            }
        }

        // 击中任何物体后销毁子弹
        Destroy(gameObject);
    }
}
