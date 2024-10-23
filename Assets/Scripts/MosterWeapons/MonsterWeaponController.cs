using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using Valve.VR;
using YourProjectNamespace;


namespace YourProjectNamespace
{
    public interface IDamageable
    {
        void TakeDamage(float damageAmount);
    }
}
//����ӿ�

public class MonsterWeaponController : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public LayerMask targetLayer;

    public float detectionRange = 5f;
    private bool isAttacking = false;

    public AudioClip attackSound;
    public GameObject attackParticlePrefab;
    private AudioSource audioSource;

    public void Update()
    {
        // �������Ŀ���Ƿ��ڷ�Χ��
        Collider[] targetsInRange = Physics.OverlapSphere(transform.position, detectionRange, targetLayer);
        if (targetsInRange.Length > 0 && !isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
        foreach (Collider collider in hitColliders)
        {
            // ����Ŀ����һ�� IDamageable �ӿ����ڽ����˺�
            YourProjectNamespace.IDamageable damageable = collider.GetComponent<IDamageable>();
            
            if (damageable != null)
            {
                damageable.TakeDamage(attackDamage);

                // ������Ч
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
                audioSource.PlayOneShot(attackSound);

                // ���ɹ�������Ч��
                Instantiate(attackParticlePrefab, collider.transform.position, Quaternion.identity);
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        Attack();
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}