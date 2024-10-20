using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    public GameObject enemyPrefab; // �з���λ�� Prefab
    public GameObject spawnPointObject; // �з���λ���ɵ�λ�õ� GameObject
    public int enemyCount = 5; // ���ɵĵз���λ����

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
        if (other.CompareTag("Ship")) // ��鴥���Ķ����Ƿ�Ϊ����
        {
            SpawnEnemies(); // ���ɵз���λ
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
                    enemyController.target = GameObject.FindGameObjectWithTag("Ship")?.transform; // ��̬����Ŀ��
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
