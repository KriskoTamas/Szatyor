using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class UIManager : MonoBehaviour
{
    public static bool showMainPage = true;
    public static GameObject mainMenuPanel, gameOverlayPanel, gameOverPanel, gamePausePanel;
    public static GameObject handRight, handRightRing;
    public static Text kinectInfoText;
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
        kinectInfoText = mainMenuPanel.transform.Find("KinectInfoText").GetComponent<Text>();

        handRight = canvas.transform.Find("HandRight").gameObject;
        handRightRing = canvas.transform.Find("HandRightRing").gameObject;

        showMainPage = true;

        mainMenuPanel.SetActive(showMainPage);
        gameOverlayPanel.SetActive(!showMainPage);
        Game.started = !showMainPage;
        Game.over = false;
        Game.paused = false;
    }

    void Update()
    {
        int distance = (int) Player.GetPos().z;
        scoreText.text = distance + " m";

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            MainMenu();
        }

        if(!Game.paused)
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
        if (!Game.started)
        {
            handRight.SetActive(false);
            handRightRing.SetActive(false);
            mainMenuPanel.SetActive(false);
            gameOverlayPanel.SetActive(true);
            gameOverPanel.SetActive(false);
            Game.started = true;
            Game.over = false;
            Game.paused = false;
        }
    }

    public void ReplayGame()
    {
        showMainPage = false;
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenu()
    {
        if (Game.paused)
        {
            showMainPage = true;
            Game.started = false;
            Game.over = false;
            Game.paused = false;
            SceneManager.LoadScene("MainScene");
        }
    }

    public void PauseResumeGame()
    {
        if (Game.started)
        {
            Game.paused = !Game.paused;
            if (Game.paused)
            {
                handRight.SetActive(true);
                handRightRing.SetActive(true);
                Player.SetAnimation(false);
                Player.forwardSpeed = 0;
                gamePausePanel.SetActive(true);
            }
            else
            {
                handRight.SetActive(false);
                handRightRing.SetActive(false);
                Player.SetAnimation(true);
                Player.forwardSpeed = Player.defaultSpeed;
                gamePausePanel.SetActive(false);
            }
        }
    }

    public void QuitGame()
    {
        if(!Game.started)
            Application.Quit();
    }
}
