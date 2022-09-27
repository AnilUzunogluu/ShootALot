using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private float _score;
    public float Score => _score;

    public void ModifyScore(float value)
    {
        _score += value;
        Mathf.Clamp(_score, 0, float.MaxValue);
        Debug.Log(_score);
    }

    public void ResetScore()
    {
        _score = 0f;
    }
}
