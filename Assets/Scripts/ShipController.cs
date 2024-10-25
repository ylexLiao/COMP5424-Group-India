using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public Transform[] waypoints; // ·��Ŀ�������
    public float moveSpeed = 5f; // ��ʼ�ƶ��ٶ�
    public float minSpeed = 1f; // ����ƶ��ٶ�
    public float stopThreshold = 3; // ֹͣ�ƶ���С��������ֵ
    public float recoveryTime = 5f; // �ָ�ʱ��
    public float blockedSpeedMultiplier = 3f; // ÿ��С�����ٶȵ�Ӱ��

    private int currentWaypointIndex = 0; // ��ǰĿ�������
    private List<GameObject> blockingEnemies = new List<GameObject>(); // �赲С���б�
    private bool isStopped = false; // �Ƿ�ֹͣǰ��
    private bool isRecovering = false; // �Ƿ��ڻָ�

    private HealthController healthController; // ��������ֵϵͳ
    // private Animator animator; // ����������
    private float originalSpeed; // ��ʼ�ٶ�


    void Start()
    {
        // animator = GetComponent<Animator>();
        originalSpeed = moveSpeed;

        healthController = GetComponent<HealthController>();
        healthController.OnHealthChanged += UpdateHealthUI; // ע������ֵ�仯�¼�
        healthController.OnDeath += OnHorseDeath; // ע�������¼�
    }

    // Update is called once per frame
    void Update()
    {
        // �������赲״̬�͸����ٶ�
        UpdateSpeedBasedOnBlockingEnemies();
        if (!isStopped && !isRecovering)
        {
            MoveAlongPath(); // ����·��ǰ��
        }
<<<<<<< HEAD

=======
        Debug.Log(blockingEnemies.Count);
>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a

    }
    // ���չ̶�·���ƶ��ĺ���
    private void MoveAlongPath()
    {
        if (currentWaypointIndex >= waypoints.Length) return; // �������Ŀ����ѵ��ֹͣ�ƶ�

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        // ���µ�λ�泯�ķ���
        RotateTowardsTarget(direction);

        // ��������ƶ�λ��
        transform.position += direction * moveSpeed * Time.deltaTime;

        // ����Ƿ񵽴�Ŀ��㣬����������л�����һ��Ŀ���
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }

    private void UpdateSpeedBasedOnBlockingEnemies()
    {
        if (blockingEnemies.Count > 0)
        {
            moveSpeed = originalSpeed - blockingEnemies.Count * blockedSpeedMultiplier;

            if (moveSpeed <= minSpeed || blockingEnemies.Count >= stopThreshold)
            {
                moveSpeed = 0f;
                isStopped = true;
                // animator.SetBool("isStopped", true);
            }
        }
        else
        {
            moveSpeed = originalSpeed;
            isStopped = false;
            // animator.SetBool("isStopped", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            blockingEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            blockingEnemies.Remove(other.gameObject);
        }
    }

<<<<<<< HEAD
    public void TakeDamage(float damage)
    {
        healthController.TakeDamage(damage); // ��������ֵϵͳ�Ŀ�Ѫ����
    }

    private void UpdateHealthUI(float currentHealth)
    {
        Debug.Log($"��ǰ����ֵ��{currentHealth}");
=======

    private void UpdateHealthUI(float currentHealth)
    {

>>>>>>> 6a5cf8ae133fe4fdaa4e3fc831df2e410538351a
    }

    private void OnHorseDeath()
    {
        Debug.Log("��������");
        isStopped = true;
        isRecovering = true;
        // animator.SetTrigger("Die");

        StartCoroutine(RecoverAfterDeath());
    }

    private IEnumerator RecoverAfterDeath()
    {
        yield return new WaitForSeconds(recoveryTime);
        healthController.Heal(healthController.maxHealth); // �ָ�����ֵ
        isStopped = false;
        isRecovering = false;
        // animator.SetTrigger("Recover");
    }

    private void RotateTowardsTarget(Vector3 direction)
    {
        if (direction != Vector3.zero) // ȷ��������������
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }
    }
}
