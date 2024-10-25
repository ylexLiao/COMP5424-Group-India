using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using Valve.VR;
using YourProjectNamespace;

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

                // ������Ч
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
                audioSource.PlayOneShot(attackSound);
                StartCoroutine(ResetDamageCooldown());
            }
        }


        private IEnumerator ResetDamageCooldown()
        {
            canDamage = false;  // ��ȴ�ڼ�����˺�
            yield return new WaitForSeconds(attackCooldown);
            canDamage = true;
        }
    }
}