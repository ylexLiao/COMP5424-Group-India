using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyController : MonoBehaviour
{
    public Transform target; // Ŀ�����
    public float moveSpeed = 3f; // �ƶ��ٶ�
    private float currentSpeed;
    private bool isSlowed = false;//����Ч��
    public float attackRange = 2f; // ������Χ
    public float attackCooldown = 1f; // ������ȴʱ��
    public float attackDamage = 10f; // ������ɵ��˺�
    public float groundOffset = 0.5f; // ��λ����汣�ֵĸ߶�ƫ�ƣ����Ը�����Ҫ������

    private bool isAttacking = false; // �жϵ�λ�Ƿ����ڹ���
    private bool isMoving = true; // �жϵ�λ�Ƿ�����ƶ�
    private Animator animator; // ��������������

    // Start is called before the first frame update
    void Start()
    {
        // ��ȡ��λ�ϵ� Animator ���
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TowardsTarget();

        // ��鵥λ��Ŀ��ľ���
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // ���Ŀ���ڹ�����Χ���Ҳ��ڹ���״̬
        if (distanceToTarget <= attackRange && !isAttacking)
        {
            StartCoroutine(Attack());
        }
        else if (distanceToTarget > attackRange && isMoving)
        {
            // ������ڹ�����Χ�ڣ����ҵ�λ�����ƶ�������Ŀ���ƶ�
            MoveToTarget();
            // �л����ƶ�����
            animator.SetBool("isAttacking", false);
        }
    }




        // ����Ŀ��
    private void TowardsTarget()
    {
        // ���㵥λ��Ŀ��ķ��򣬲��Ը÷����ƶ�
        Vector3 direction = (target.position - transform.position).normalized;

        // ���µ�λ�泯�ķ���
        RotateTowardsTarget(direction);
    }

        // ��Ŀ���ƶ��ĺ���
    private void MoveToTarget()
    {
        // ���㵥λ��Ŀ��ķ��򣬲��Ը÷����ƶ�
        Vector3 direction = (target.position - transform.position).normalized;

        // ���µ�λ�泯�ķ���
        RotateTowardsTarget(direction);

        // ������߶Ȳ�����λλ�ñ����ڵ�����
        AdjustToGround();

        // �ƶ���λ��λ��
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    //����
    public void ApplySlowdown(float factor, float duration)
    {
        if (!isSlowed)
        {
            StartCoroutine(SlowdownCoroutine(factor, duration));
        }
    }

    private IEnumerator SlowdownCoroutine(float factor, float duration)
    {
        isSlowed = true;
        currentSpeed *= factor;
        yield return new WaitForSeconds(duration);
        currentSpeed = moveSpeed;
        isSlowed = false;
    }



    // ʹ��λ�泯�ƶ�����ĺ���
    private void RotateTowardsTarget(Vector3 direction)
    {
        if (direction != Vector3.zero) // ȷ��������������
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }
    }


    // ������λλ���Ա����俿������
    private void AdjustToGround()
    {
        RaycastHit hit;

        // �ӵ�λλ�����·������ߣ�������λ��
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            // ����λ�� y �������Ϊ����߶ȣ�������ƫ������groundOffset��
            transform.position = new Vector3(transform.position.x, hit.point.y + groundOffset, transform.position.z);
        }
    }

    // ����Ŀ���Э��
    private IEnumerator Attack()
    {
        isAttacking = true; // ���Ϊ����״̬
        isMoving = false; // ����ʱ���ƶ�

        // �л�����������
        animator.SetBool("isAttacking", true);

        // ������ʵ�ֹ������߼������粥�Ŷ������߼���Ŀ���Ѫ��
        Debug.Log("Attacking the target!");

        // ��Ŀ�굥λ����˺�
        if (target != null)
        {
            // ��ȡĿ��� UnitHealth ���������������˺�
            HealthController targetHealth = target.GetComponent<HealthController>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(attackDamage); // ��Ŀ������˺�
            }
        }

        yield return new WaitForSeconds(attackCooldown); // �ȴ�������ȴʱ��

        isAttacking = false; // ��������״̬
        isMoving = true; // �����ƶ�

        // �л����ƶ�����
        // animator.SetBool("isAttacking", false);
    }

}
