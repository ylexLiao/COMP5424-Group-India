using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TwoHandSword : MonoBehaviour
{
    // �����ճֵ�������ճֵ�
    public Transform leftHandMount;
    public Transform rightHandMount;
  
    // ���ֺ����ֵĿ���������VR�ֱ���
    public Transform leftHandController;
    public Transform rightHandController;

    // �������� Transform�����ڵ���˫�ֶ������������������̬��
    public Transform swordTransform;

    // �����ֲ��ճֵ�ͽ������λ��ƫ��
    public Vector3 leftHandOffset;
    public Vector3 rightHandOffset;

    // Update ����ÿ֡ͬ�������ֱ���λ�ú���ת
    private void Update()
    {
        // ������ֿ��������ڣ�����λ�ú���ת�󶨵������ճֵ�
        if (leftHandController != null)
        {
            leftHandMount.position = leftHandController.position + leftHandOffset;
            leftHandMount.rotation = leftHandController.rotation;
        }

        // ������ֿ��������ڣ�����λ�ú���ת�󶨵������ճֵ�
        if (rightHandController != null)
        {
            rightHandMount.position = rightHandController.position + rightHandOffset;
            rightHandMount.rotation = rightHandController.rotation;
        }

        // ��ѡ��ͬ����������������ת������˫�ֵ���תƽ��ֵ��ȷ����
        if (leftHandController != null && rightHandController != null)
        {
            swordTransform.position = (leftHandController.position + rightHandController.position) / 2;
            swordTransform.rotation = Quaternion.LookRotation(rightHandController.position - leftHandController.position);
        }
    }
}

