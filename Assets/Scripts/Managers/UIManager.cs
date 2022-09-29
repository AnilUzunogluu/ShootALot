using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthDisplay;
    [SerializeField] private TextMeshProUGUI scoreText;

    private Health _health;
    private ScoreKeeper _scoreKeeper;

    private void Awake()
    {
        _health = FindObjectOfType<Health>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        SetMaxHealth();
        UpdateHealth();
        UpdateScore();

        _health.OnDamage += UpdateHealth;
        _scoreKeeper.OnScore += UpdateScore;
    }

    private void UpdateHealth()
    {
        healthDisplay.value = _health.GetHealth;
    }

    private void UpdateScore()
    {
        scoreText.text = _scoreKeeper.Score.ToString("000000000");
    }

    private void SetMaxHealth()
    {
        healthDisplay.maxValue = _health.GetHealth;
    }
}
