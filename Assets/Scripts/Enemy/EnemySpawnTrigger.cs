using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public GameObject enemyPrefab; // 敌方单位的 Prefab
    public GameObject spawnPointObject; // 敌方单位生成的位置的 GameObject
    public int enemyCount = 5; // 生成的敌方单位数量

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship")) // 检查触发的对象是否为检查点
        {
            SpawnEnemies(); // 生成敌方单位
        }
    }

    private void SpawnEnemies()
    {
        if (spawnPointObject != null)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnPointObject.transform.position, spawnPointObject.transform.rotation);
                EnemyController enemyController = enemy.GetComponent<EnemyController>();

                if (enemyController != null)
                {
                    enemyController.target = GameObject.FindGameObjectWithTag("Ship")?.transform; // 动态设置目标
                }
            }
            Debug.Log($"{enemyCount} enemies spawned at: {spawnPointObject.transform.position}");
        }
        else
        {
            Debug.LogWarning("Spawn Point Object is not assigned!");
        }
    }
}
