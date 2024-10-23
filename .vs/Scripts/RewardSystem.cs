using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardSystem : MonoBehaviour
{
    public Vector3 targetPosition;
    public float reachThreshold = 1f;

    private Item rewardedItem;

    void Start()
    {
        rewardedItem = null;
    }

    void Update()
    {
        if (rewardedItem == null && Vector3.Distance(transform.position, targetPosition) <= reachThreshold)
        {
            RewardPlayer();

            // 假设玩家获得了A道具

           

        if (rewardedItem != null && OVRInput.GetDown(OVRInput.Button.Four))

        {
            UseItem();
        }
    }

    void RewardPlayer()
    {
        Item[] items = new Item[]
        {
            new HealthupPickup(),
            new SlowdownPickup(),
            new AreaDamageItem()
        };

        int randomIndex = Random.Range(0, items.Length);
        rewardedItem = items[randomIndex];
        Debug.Log($"Player rewarded with: {rewardedItem.itemName}");


        }
    }

    void UseItem()
    {
        if (rewardedItem is AreaDamageItem areaDamageItem)
        {
            areaDamageItem.Use(transform.position);
        }
        else if (rewardedItem is HealthupPickup healthPotion)
        {
            healthPotion.Use();
        }
        else if (rewardedItem is SlowdownPickup manaPotion)
        {
            manaPotion.Use();
        }

        rewardedItem = null; // Clear the item after use
    }
}