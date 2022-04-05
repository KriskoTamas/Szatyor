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
        //Text obj = canvas.transform.Find("KinectInfoText").GetComponent<Text>();
        kinectInfoText = mainMenuPanel.transform.Find("KinectInfoText").GetComponent<Text>();

        print("kinectInfoText: " + kinectInfoText);

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
        int distance = (int) PlayerController.playerTransform.position.z;
        scoreText.text = distance + " m";

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }

        if (!Game.started && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (Game.paused && Input.GetKeyDown(KeyCode.Space))
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
        handRight.SetActive(false);
        handRightRing.SetActive(false);
        mainMenuPanel.SetActive(false);
        gameOverlayPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        Game.started = true;
        Game.over = false;
        Game.paused = false;
    }

    public void ReplayGame()
    {
        showMainPage = false;
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenu()
    {
        showMainPage = true;
        Game.started = false;
        Game.over = false;
        Game.paused = false;
        SceneManager.LoadScene("MainScene");
    }

    public void PauseResumeGame()
    {
        if (Game.started)
        {
            Game.paused = !Game.paused;
            if (Game.paused)
            {
                //Time.timeScale = 0;
                handRight.SetActive(true);
                handRightRing.SetActive(true);
                PlayerController.player.gameObject.GetComponent<Animator>().enabled = false;
                PlayerController.forwardSpeed = 0;
                gamePausePanel.SetActive(true);
            }
            else
            {
                //Time.timeScale = 1;
                handRight.SetActive(false);
                handRightRing.SetActive(false);
                PlayerController.player.gameObject.GetComponent<Animator>().enabled = true;
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
