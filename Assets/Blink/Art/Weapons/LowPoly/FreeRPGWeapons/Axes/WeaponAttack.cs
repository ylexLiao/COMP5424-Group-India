using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public int damage = 10; // 武器的伤害值

    void OnCollisionEnter(Collision collision)
    {
        // 检查是否击中敌人
        if (collision.gameObject.tag == "Enemy")
        {
            // 获取敌人的健康组件
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // 造成伤害
            }
        }
    }
}
