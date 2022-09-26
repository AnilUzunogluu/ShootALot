using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Path")]
    [SerializeField] private Transform pathPrefab;
    
    [Header("Spawn Delay")]
    [SerializeField] private float spawnDelay;
    [SerializeField] private float spawnDelayVariance;
    [SerializeField] private float minSpawnDelay;

    public float MoveSpeed => moveSpeed;
    public int EnemyCount => enemies.Count;

    public GameObject GetEnemy(int enemyIndex)
    {
        return enemies[enemyIndex];
    }

    public Transform GetFirstWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();

        foreach (Transform child in pathPrefab)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public float GetRandomSpawnDelay()
    {
        var randomSpawnDelay = Random.Range(spawnDelay - spawnDelayVariance, spawnDelay + spawnDelayVariance);
        return Mathf.Clamp(randomSpawnDelay, minSpawnDelay, float.MaxValue);
    }
}
