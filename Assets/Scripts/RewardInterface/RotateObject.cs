using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // 旋转速度
    [SerializeField] private float rotationSpeed = 50f;

    // 控制旋转的轴
    [SerializeField] private Vector3 rotationAxis = Vector3.right;  // 默认为 X 轴

    void Update()
    {
        // 以指定的速度和轴进行旋转
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }
}
