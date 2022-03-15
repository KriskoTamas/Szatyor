using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool gameStarted, gameOver, gamePaused;
    public static bool showMainPage = true;
    public GameObject mainMenuPanel, gameOverlayPanel, gameOverPanel, gamePausePanel;
    public Text scoreText;
    void Start()
    {
        mainMenuPanel.SetActive(showMainPage);
        gameOverlayPanel.SetActive(!showMainPage);
        Time.timeScale = showMainPage ? 0 : 1;
        gameStarted = !showMainPage;
        gameOver = false;
        gamePaused = false;
    }

    void Update()
    {
        int distance = (int) PlayerController.playerTransform.position.z;
        scoreText.text = distance + " m";

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        gameOverlayPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameStarted = true;
        gameOver = false;
        gamePaused = false;
        Time.timeScale = 1;
    }

    public void ReplayGame()
    {
        showMainPage = false;
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenu()
    {
        showMainPage = true;
        SceneManager.LoadScene("MainScene");
    }

    public void PauseResumeGame()
    {
        if (gameStarted)
        {
            gamePaused = !gamePaused;
            if (gamePaused)
            {
                Time.timeScale = 0;
                gamePausePanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                gamePausePanel.SetActive(false);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
