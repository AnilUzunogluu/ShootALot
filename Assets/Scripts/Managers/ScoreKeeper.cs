using System;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private static ScoreKeeper instance;
    
    private float _score;
    public float Score => _score;

    public event Action OnScore;

    private void Awake()
    {
        ManageSingleton();
    }
    
    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ModifyScore(float value)
    {
        _score += value;
        OnScore?.Invoke();
        Mathf.Clamp(_score, 0, float.MaxValue);
        Debug.Log(_score);
    }

    public void ResetScore()
    {
        _score = 0f;
        OnScore?.Invoke();
    }
}
