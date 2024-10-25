using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TwoHandSword : MonoBehaviour
{
    // 左手握持点和右手握持点
    public Transform leftHandMount;
    public Transform rightHandMount;
  
    // 左手和右手的控制器（如VR手柄）
    public Transform leftHandController;
    public Transform rightHandController;

    // 剑的整体 Transform（用于调整双手动作后整体调整剑的姿态）
    public Transform swordTransform;

    // 调整手部握持点和剑的相对位置偏移
    public Vector3 leftHandOffset;
    public Vector3 rightHandOffset;

    // Update 用于每帧同步更新手柄的位置和旋转
    private void Update()
    {
        // 如果左手控制器存在，将其位置和旋转绑定到左手握持点
        if (leftHandController != null)
        {
            leftHandMount.position = leftHandController.position + leftHandOffset;
            leftHandMount.rotation = leftHandController.rotation;
        }

        // 如果右手控制器存在，将其位置和旋转绑定到右手握持点
        if (rightHandController != null)
        {
            rightHandMount.position = rightHandController.position + rightHandOffset;
            rightHandMount.rotation = rightHandController.rotation;
        }

        // 可选：同步调整剑的整体旋转（根据双手的旋转平均值来确定）
        if (leftHandController != null && rightHandController != null)
        {
            swordTransform.position = (leftHandController.position + rightHandController.position) / 2;
            swordTransform.rotation = Quaternion.LookRotation(rightHandController.position - leftHandController.position);
        }
    }
}

