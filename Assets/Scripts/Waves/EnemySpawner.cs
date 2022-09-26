using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves;

    public WaveConfigSO CurrentWave { get; private set; }

    private List<GameObject> _enemies = new();

    private bool isLooping = true;
    
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    IEnumerator  SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                CurrentWave = wave;
                for (int i = 0; i < CurrentWave.EnemyCount; i++)
                {
                    Instantiate(CurrentWave.GetEnemy(i), CurrentWave.GetFirstWaypoint().position, Quaternion.Euler(0,0,180), transform);
                    yield return new WaitForSeconds(CurrentWave.GetRandomSpawnDelay());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);
       
    }

    // private void GetEnemies()
    // {
    //     for (var i = 0; i < currentWave.EnemyCount; i++)
    //     {
    //         _enemies.Add(currentWave.GetEnemy(i));
    //     }
    // }
    //
    // private void ClearEnemies()
    // {
    //     _enemies.Clear();
    // }
    //
    // IEnumerator SpawnWaves()
    // {
    //     foreach (var wave in waveConfigs)
    //     {
    //         if (isWaveEnded)
    //         {
    //             currentWave = wave;
    //             ClearEnemies();
    //             GetEnemies();
    //             StartCoroutine(SpawnEnemies());
    //             isWaveEnded = false;
    //         }
    //         yield return new WaitForSeconds(timeBetweenWaves);
    //     }
    // }
    
}
