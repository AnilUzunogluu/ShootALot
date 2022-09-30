using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    
    private const string MAIN_MENU = "Main Menu";
    private const string GAME_OVER = "Game Over";
    private const string GAME = "Game";


    private void Awake()
    {
        instance = this;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU);
    }
    public void LoadGame()
    {
        ScoreKeeper.ResetScore();
        SceneManager.LoadScene(GAME);
    }
    private void LoadGameOver()
    {
        StartCoroutine(WaitForLoad(GAME_OVER, 1));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitForLoad(string name, int duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(name);
    }

    public static void GameOver()
    {
        instance.LoadGameOver();
    }
}
