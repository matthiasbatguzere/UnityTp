using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 2.0f; 
    public Vector2 spawnPositionRange; 

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            float randomY = Random.Range(spawnPositionRange.x, spawnPositionRange.y);
            Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.z);

            GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

            obstacle.transform.SetParent(transform);
        }
    }
}
