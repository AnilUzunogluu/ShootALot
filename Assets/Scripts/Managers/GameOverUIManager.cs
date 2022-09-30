using TMPro;
using UnityEngine;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = $"You scored:\n {ScoreKeeper.Score}";
    }
}
