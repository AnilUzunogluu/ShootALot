using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private WaveConfigSO _waveConfigSo;

    private List<Transform> _waypoints;

    private int _currentIndex;

    private void Awake()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        _waveConfigSo = _enemySpawner.CurrentWave;
        _waypoints = _waveConfigSo.GetWaypoints();
    }

    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (_currentIndex < _waypoints.Count)
        {
            Vector3 targetPos = _waypoints[_currentIndex].position;
            float delta = _waveConfigSo.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, delta);
            if (transform.position == targetPos)
            {
                _currentIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
