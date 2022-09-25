using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private WaveConfigSO _waveConfigSo;

    private List<Transform> waypoints;

    private int currentIndex = 0;

    private void Awake()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        _waveConfigSo = _enemySpawner.CurrentWave;
        waypoints = _waveConfigSo.GetWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (currentIndex < waypoints.Count)
        {
            Vector3 targetPos = waypoints[currentIndex].position;
            float delta = _waveConfigSo.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, delta);
            if (transform.position == targetPos)
            {
                currentIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
