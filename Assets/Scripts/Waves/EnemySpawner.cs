using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private int waveLoopsBeforeBoss;

    public WaveConfigSO currentWave { get; private set; }

    private bool isLooping = true;
    
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves(waveLoopsBeforeBoss));
    }

    IEnumerator  SpawnEnemyWaves(int count)
    {
        var bossWaveSkipped = 0;
        for (int i = 0; i < count; i++)
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                if (bossWaveSkipped < count - 1 && currentWave.name == "Boss")
                {
                    currentWave = waveConfigs.First();
                    bossWaveSkipped++;
                }
                for (int j = 0; j < currentWave.EnemyCount; j++)
                {
                    Instantiate(currentWave.GetEnemy(j), currentWave.GetFirstWaypoint().position, Quaternion.Euler(0,0,180f), transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnDelay());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
    }
    
}
