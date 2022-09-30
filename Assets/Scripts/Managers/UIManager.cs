using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthDisplay;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        UpdateScore();
    }

    private void OnEnable()
    {
        Debug.Log("onenable");
        Health.OnPlayerHealthInitialized += SetMaxHealth;
        Health.OnPlayerHit += UpdateHealth;
        ScoreKeeper.Instance.OnScore += UpdateScore;
    }

    private void OnDisable()
    {
        ScoreKeeper.Instance.OnScore -= UpdateScore;
        Health.OnPlayerHit -= UpdateHealth;
    }

    private void UpdateHealth(float value)
    {
        healthDisplay.value = value;
    }

    private void UpdateScore()
    {
        scoreText.text = ScoreKeeper.Score.ToString("000000000");
    }

    private void SetMaxHealth(float value)
    {
        healthDisplay.maxValue = value;
        Health.OnPlayerHealthInitialized -= SetMaxHealth;
        UpdateHealth(value);

    }
}
