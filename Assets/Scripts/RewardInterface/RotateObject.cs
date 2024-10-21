using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // ��ת�ٶ�
    [SerializeField] private float rotationSpeed = 50f;

    // ������ת����
    [SerializeField] private Vector3 rotationAxis = Vector3.right;  // Ĭ��Ϊ X ��

    void Update()
    {
        // ��ָ�����ٶȺ��������ת
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
