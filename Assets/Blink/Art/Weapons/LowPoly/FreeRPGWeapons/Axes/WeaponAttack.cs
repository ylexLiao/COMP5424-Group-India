using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public int damage = 10; // �������˺�ֵ

    void OnCollisionEnter(Collision collision)
    {
        // ����Ƿ���е���
        if (collision.gameObject.tag == "Enemy")
        {
            // ��ȡ���˵Ľ������
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // ����˺�
            }
        }
    }
}
