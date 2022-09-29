using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : MonoBehaviour
{
    private const string MAIN_MENU = "Main Menu";
    private const string GAME_OVER = "Game Over";
    private const string GAME = "Game";

    private ScoreKeeper _scoreKeeper;

    private void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU);
    }
    public void LoadGame()
    {
        _scoreKeeper.ResetScore();
        SceneManager.LoadScene(GAME);
    }
    public void LoadGameOver()
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
}
