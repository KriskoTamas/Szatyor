using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public static bool gameStarted, gameOver, gamePaused;
    public static bool showMainPage = true;
    public static GameObject mainMenuPanel, gameOverlayPanel, gameOverPanel, gamePausePanel;
    [SerializeField]
    private Text framerateText;
    public Text scoreText;

    private int frameCount = 0;
    private float timeCount = 0;
    private float refreshTime = 0.1f;

    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        mainMenuPanel = canvas.transform.Find("MainMenuPanel").gameObject;
        gameOverlayPanel = canvas.transform.Find("GameOverlayPanel").gameObject;
        gamePausePanel = canvas.transform.Find("GamePausePanel").gameObject;
        gameOverPanel = canvas.transform.Find("GameOverPanel").gameObject;
        
        showMainPage = true;

        mainMenuPanel.SetActive(showMainPage);
        gameOverlayPanel.SetActive(!showMainPage);
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

        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (gamePaused && Input.GetKeyDown(KeyCode.Space))
        {
            MainMenu();
        }

        if(!gamePaused)
            CalculateFPS();
    }

    private void CalculateFPS()
    {
        if (timeCount < refreshTime)
        {
            timeCount += Time.deltaTime;
            frameCount++;
        }
        else
        {
            float fps = frameCount / timeCount;
            frameCount = 0;
            timeCount = 0;
            framerateText.text = "FPS: " + fps.ToString("n0");
        }
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        gameOverlayPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameStarted = true;
        //gameOver = false;
        //gamePaused = false;
        //Time.timeScale = 1;
    }

    public void ReplayGame()
    {
        showMainPage = false;
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenu()
    {
        showMainPage = true;
        gameStarted = false;
        gameOver = false;
        gamePaused = false;
        SceneManager.LoadScene("MainScene");
    }

    public void PauseResumeGame()
    {
        if (gameStarted)
        {
            gamePaused = !gamePaused;
            if (gamePaused)
            {
                //Time.timeScale = 0;
                PlayerController.forwardSpeed = 0;
                print("game paused, panel: " + gamePausePanel);
                gamePausePanel.SetActive(true);
            }
            else
            {
                //Time.timeScale = 1;
                PlayerController.forwardSpeed = PlayerController.playerSpeed;
                gamePausePanel.SetActive(false);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
