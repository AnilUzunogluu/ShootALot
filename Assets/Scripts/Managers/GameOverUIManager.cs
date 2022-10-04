using TMPro;
using UnityEngine;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [SerializeField] private TextMeshProUGUI EndText;

    void Start()
    {
        scoreText.text = $"You scored:\n {ScoreKeeper.Score}";
        var gameState = FindObjectOfType<LevelManager>().isGameWon;
        SetEndText(gameState);
    }
    
    
    public void SetEndText(bool won)
    {
        if (won)
        {
            EndText.text = "You Won!";
        }
        else
        {
            EndText.text = "Game Over";
        }
    }
}
