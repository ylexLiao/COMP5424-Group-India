using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public int killCount = 0;
    public int killsNeededForItem = 20;
    public GameObject[] itemPrefabs; // 道具对象数组

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= HandleEnemyKilled;
    }

    private void HandleEnemyKilled()
    {
        killCount++;
        if (killCount >= killsNeededForItem)
        {
            SpawnRandomItem();
            killCount = 0; // 重置计数
        }
    }

    private void SpawnRandomItem()
    {
        if (itemPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject item = itemPrefabs[randomIndex];
            item.SetActive(true); // 激活道具
            // 你可以在这里设置道具的生成位置
            item.transform.position = GetRandomSpawnPosition();
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // 返回一个随机生成位置
        // 你可以根据需要自定义生成逻辑
        return new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
    }
}
