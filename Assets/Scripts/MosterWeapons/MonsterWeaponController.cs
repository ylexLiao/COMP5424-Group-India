using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using Valve.VR;
using YourProjectNamespace;

<<<<<<< HEAD

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
=======
namespace YourProjectNamespace
{
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
        public float attackCooldown = 1.7f;  // �����������λ���룩
        private bool canDamage = true;

        public void Update()
        {
             
        }

        private void OnTriggerEnter(Collider other)
        {
            if (canDamage && other.CompareTag("Ship"))
            {
                HealthController targetHealth = other.GetComponent<HealthController>();
                targetHealth.TakeDamage(attackDamage);//��Ŀ������˺�
                Debug.Log($"��� {attackDamage} ���˺��� {other.gameObject.name}");

                // ��������ϵͳ
                if (attackParticlePrefab != null)
                {
                    // ʵ��������ϵͳ
                    GameObject particleObject = Instantiate(attackParticlePrefab, transform.position, Quaternion.identity);
                    // ��ȡ����ϵͳ���
                    ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
                    if (particleSystem != null)
                    {
                        // ��������ϵͳ
                        particleSystem.Play();
                        // ��������ϵͳ�Զ�����
                        Destroy(particleObject, particleSystem.main.startLifetime.constantMax);
                    }
                }
>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a

                // ������Ч
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
                audioSource.PlayOneShot(attackSound);
<<<<<<< HEAD

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
=======
                StartCoroutine(ResetDamageCooldown());
            }
        }


        private IEnumerator ResetDamageCooldown()
        {
            canDamage = false;  // ��ȴ�ڼ�����˺�
            yield return new WaitForSeconds(attackCooldown);
            canDamage = true;
        }
>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a
    }
}