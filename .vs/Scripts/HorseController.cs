using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public Transform[] waypoints; // �̶�·�ߵ�Ŀ�������
    public float moveSpeed = 5f; // ��ĳ�ʼ�ƶ��ٶ�
    public float minSpeed = 1f; // ����ƶ��ٶ�
    public float stopThreshold = 3; // ֹͣ�ƶ���С��������ֵ
    public float health = 100f; // ��ĳ�ʼ����ֵ
    public float maxHealth = 100f; // ����������ֵ
    public float recoveryTime = 5f; // ��Ļָ�ʱ��
    public float blockedSpeedMultiplier = 0.1f; // ÿ��С�������ƶ��ٶȵ�Ӱ��

    private int currentWaypointIndex = 0; // ��ǰĿ�������
    private List<GameObject> blockingEnemies = new List<GameObject>(); // �赲���С���б�
    private bool isStopped = false; // ���Ƿ�ֹͣǰ��
    private bool isRecovering = false; // ���Ƿ��ڻָ�Ѫ��

    //----------��ʱû����������--------------//
    // private Animator animator; // ����������
    private float originalSpeed; // ������ĳ�ʼ�ٶ�

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        originalSpeed = moveSpeed; // ������ĳ�ʼ�ٶ�
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStopped && !isRecovering)
        {
            MoveAlongPath(); // ����·��ǰ��
        }

        // �������赲״̬�͸����ٶ�
        UpdateSpeedBasedOnBlockingEnemies();
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

    // ���͸�������ٶȣ�����С��������
    private void UpdateSpeedBasedOnBlockingEnemies()
    {
        // ����С��������������ƶ��ٶ�
        if (blockingEnemies.Count > 0)
        {
            moveSpeed = originalSpeed - blockingEnemies.Count * blockedSpeedMultiplier;

            // �������ٶȵ�����С�ٶȣ���ֹͣ
            if (moveSpeed <= minSpeed || blockingEnemies.Count >= stopThreshold)
            {
                moveSpeed = 0f;
                isStopped = true;
                // animator.SetBool("isStopped", true); // ����ֹͣ����
            }
        }
        else
        {
            moveSpeed = originalSpeed;
            isStopped = false;
            // animator.SetBool("isStopped", false); // �ָ��ƶ�����
        }
    }

    // ����С����������赲��Χʱ�������˺���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            blockingEnemies.Add(other.gameObject);
        }
    }

    // ��С���뿪����赲��Χʱ�������˺���
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            blockingEnemies.Remove(other.gameObject);
        }
    }

    // �ܻ��������������ܵ�С������ʱ����
    public void TakeDamage(float damage)
    {
        if (isRecovering) return; // �ָ������в����ܵ��˺�

        health -= damage;
        if (health <= 0)
        {
            health = 0;
            StopAndRecover(); // ��Ѫ���ľ�ʱ��ֹͣǰ�����ָ�
        }
    }

    // ֹͣ����ʼ�ָ�Ѫ��
    private void StopAndRecover()
    {
        isStopped = true;
        isRecovering = true;
        // animator.SetTrigger("Die"); // �������ܻ����¶���

        // ͣ��ָ��ʱ���ָ�Ѫ��
        StartCoroutine(RecoverHealth());
    }

    // �ָ�Ѫ����Э��
    private IEnumerator RecoverHealth()
    {
        yield return new WaitForSeconds(recoveryTime); // ͣ���ָ�ʱ��

        health = maxHealth; // �ָ�Ѫ��
        isStopped = false;
        isRecovering = false;
        // animator.SetTrigger("Recover"); // ���Żָ�����
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
