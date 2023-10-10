using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public int numberOfAsteroids = 20;
    public float spawnRadius = 50f;

    void Start()
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
            Instantiate(asteroidPrefab, randomPosition, Quaternion.identity);
        }
    }
}
