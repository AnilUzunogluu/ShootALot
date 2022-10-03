using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves;

    public WaveConfigSO currentWave { get; private set; }

    private bool isLooping = true;
    
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator  SpawnEnemyWaves()
    {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.EnemyCount; i++)
                {
                    Instantiate(currentWave.GetEnemy(i), currentWave.GetFirstWaypoint().position, Quaternion.Euler(0,0,180f), transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnDelay());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
    }
    
}
