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
        public float attackCooldown = 1.7f;  // 攻击间隔（单位：秒）
        private bool canDamage = true;

        public void Update()
        {
             
        }

        private void OnTriggerEnter(Collider other)
        {
            if (canDamage && other.CompareTag("Ship"))
            {
                HealthController targetHealth = other.GetComponent<HealthController>();
                targetHealth.TakeDamage(attackDamage);//对目标造成伤害
                Debug.Log($"造成 {attackDamage} 点伤害给 {other.gameObject.name}");

                // 激活粒子系统
                if (attackParticlePrefab != null)
                {
                    // 实例化粒子系统
                    GameObject particleObject = Instantiate(attackParticlePrefab, transform.position, Quaternion.identity);
                    // 获取粒子系统组件
                    ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
                    if (particleSystem != null)
                    {
                        // 播放粒子系统
                        particleSystem.Play();
                        // 设置粒子系统自动销毁
                        Destroy(particleObject, particleSystem.main.startLifetime.constantMax);
                    }
                }

                // 播放音效
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
            canDamage = false;  // 冷却期间禁用伤害
            yield return new WaitForSeconds(attackCooldown);
            canDamage = true;
        }
    }
}