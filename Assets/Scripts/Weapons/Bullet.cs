using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10; // 子弹伤害值

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("检测到碰撞，碰撞对象：" + other.name);

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("子弹成功击中了敌人：" + other.name);

            HealthController health = other.GetComponent<HealthController>();
            if (health != null)
            {
                Debug.Log("子弹对敌人造成的伤害值：" + damage);
                health.TakeDamage(damage);
            }
            else
            {
                Debug.LogWarning("击中的敌人没有 HealthController 组件！");
            }

            Destroy(gameObject); // 销毁子弹
        }
    }

}
