using System;
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance; 
    
    private const string MAIN_MENU = "Main Menu";
    private const string GAME_OVER = "Game Over";
    private const string GAME = "Game";


    public event Action<string> SwitchGameMusic;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        var instances = FindObjectsOfType<LevelManager>();
        if (instances.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU);
        SwitchGameMusic?.Invoke("Main Menu");
    }
    public void LoadGame()
    {
        ScoreKeeper.ResetScore();
        SceneManager.LoadScene(GAME);
        SwitchGameMusic?.Invoke("Game");
    }
    private void LoadGameOver()
    {
        StartCoroutine(WaitForLoad(GAME_OVER, 1f));
        SwitchGameMusic?.Invoke("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitForLoad(string name, float duration)
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(name);
    }

    public static void GameOver()
    {
        Instance.LoadGameOver();
    }
}
