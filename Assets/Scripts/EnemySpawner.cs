using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private WaveConfigSO currentWave;

    public WaveConfigSO CurrentWave => currentWave;

    private List<GameObject> _enemies = new();
    
    void Start()
    {
        GetEnemies();
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator  SpawnEnemies()
    {
        foreach (var enemy in _enemies)
        {
            Instantiate(enemy, currentWave.GetFirstWaypoint().position, quaternion.identity, transform);
            yield return new WaitForSeconds(currentWave.GetRandomSpawnDelay());
        }
    }

    private void GetEnemies()
    {
        for (var i = 0; i < currentWave.EnemyCount; i++)
        {
            _enemies.Add(currentWave.GetEnemy(i));
        }
    }
}
