using UnityEngine;
using UnityEngine.SceneManagement;
using static UIManager;

public class Game : MonoBehaviour
{

    public static int score = 0;
    public static bool started, over, paused;
    public static bool kinectConnected = false;

    public static void StartGame()
    {
        if (!started)
        {
            handRight.SetActive(false);
            handRightRing.SetActive(false);
            mainMenuPanel.SetActive(false);
            gameOverlayPanel.SetActive(true);
            gameOverPanel.SetActive(false);
            started = true;
            over = false;
            paused = false;
        }
    }

    public static void GameOver()
    {
        over = true;
        started = false;
        handRight.SetActive(kinectConnected);
        handRightRing.SetActive(kinectConnected);
        gameOverPanel.SetActive(true);
        Player.SetAnimation(false);
        Player.forwardSpeed = 0;
        finalScoreText.text = "Pontszám: " + score;
        score = 0;
    }

    public static void ReplayGame()
    {
        showMainPage = false;
        SceneManager.LoadScene("MainScene");
    }

    public static void PauseResumeGame()
    {
        if (started)
        {
            paused = !paused;
            if (paused)
            {
                handRight.SetActive(kinectConnected);
                handRightRing.SetActive(kinectConnected);
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

    public static void MainMenu()
    {
        if (paused || over)
        {
            score = 0;
            showMainPage = true;
            started = false;
            over = false;
            paused = false;
            SceneManager.LoadScene("MainScene");
        }
    }

    public static void BackToMainMenu()
    {
        toplistPanel.SetActive(false);
    }

    public static void ToplistMenu()
    {
        toplistPanel.SetActive(true);
    }

    public static void QuitGame()
    {
        if (!started || over)
            Application.Quit();
    }

    public static void AddToScore(int amount)
    {
        score += amount;
        scoreText.text = "Pont: " + score;
    }

}
