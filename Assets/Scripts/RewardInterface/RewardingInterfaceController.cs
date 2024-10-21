using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardingInterfaceController : MonoBehaviour
{
    public GameObject rewardingInterface; // 绑定奖励界面的 GameObject
    private bool hasTriggered = false; // 确保只触发一次

    private void OnTriggerEnter(Collider other)
    {
        // 检查是否是玩家
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // 设置为已触发
            ShowRewardingInterface();
        }
    }

    private void ShowRewardingInterface()
    {
        // 显示奖励界面
        if (rewardingInterface != null)
        {
            rewardingInterface.SetActive(true);
        }
    }
}
