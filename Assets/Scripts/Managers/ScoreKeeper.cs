using System;
using Managers;
using UnityEngine;

public class ScoreKeeper : Singleton<ScoreKeeper>
{
    
    private float _score;
    public static float Score => Instance._score;

    public event Action OnScore;

    private void UpdateScore(float value)
    {
        _score += value;
        OnScore?.Invoke();
        Mathf.Clamp(_score, 0, float.MaxValue);
    }

    private void SetScore(float value)
    {
        _score = value;
        OnScore?.Invoke();
    }

    public static void ResetScore()
    { 
        Instance.SetScore(0);
    }

    public static void ModifyScore(float value)
    {
        Instance.UpdateScore(value);
    }
}
