using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Health Potion", menuName = "Items/Health Potion")]
public class HealthupPickup : Item
{
    // Start is called before the first frame update
    public int healAmount = 20;

    public override void Use()
    {
        // 实现恢复生命值的效果
        Debug.Log("使用了生命药水，恢复了 " + healAmount + " 点生命值。");


        // 假设有一个 Player 类，包含一个 IncreaseHealth 方法
        HealthController player = FindObjectOfType<HealthController>();
        if (player != null)
        {
            player.IncreaseHealth(healAmount);
        }

        // 销毁道具
   
    }
    // 在这里添加具体的逻辑，例如增加玩家的生命值


    // 假设有一个 Player 类，包含一个 IncreaseHealth 方法
    // Player.Instance.IncreaseHealth(healAmount);
}
