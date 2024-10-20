using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{
    public int killCount = 0;
    public int killsNeededForItem = 20;
    public GameObject[] itemPrefabs; // ���߶�������

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
            killCount = 0; // ���ü���
        }
    }

    private void SpawnRandomItem()
    {
        if (itemPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, itemPrefabs.Length);
            GameObject item = itemPrefabs[randomIndex];
            item.SetActive(true); // �������
            // ��������������õ��ߵ�����λ��
            item.transform.position = GetRandomSpawnPosition();
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // ����һ���������λ��
        // ����Ը�����Ҫ�Զ��������߼�
        return new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
    }
}
