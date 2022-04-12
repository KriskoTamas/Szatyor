using UnityEngine;
using UnityEngine.SceneManagement;
using static UIManager;

public class Game : MonoBehaviour
{

    public static bool started, over, paused;
    public static bool kinectConnected = false;

    public static void StartGame()
    {
        if (!started)
        {
            Player.name = playernameInput.text;
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
        gameOverPanel.SetActive(true);
        Player.SetAnimation(false);
        Player.forwardSpeed = 0;
        print("playerName: " + Player.name);
        for (int i = 0; i < Toplist.records.elements.Count; i++)
        {
            print(Toplist.records.elements[i].playerName);
        }
        RecordList.Record record = Toplist.records.elements.Find(x => x.playerName == Player.name);
        if (record != null)
        {
            print("record: " + record);
            //Toplist.records.elements
        }
        if (Player.score > Player.highscore)
        {
            
            Player.highscore = Player.score;
            PlayerPrefs.SetInt("highscore", Player.highscore);
        }
        finalScoreText.text = "Pontszám: " + Player.score + "\nRekord: " + Player.highscore;
        Player.score = 0;
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
            Player.score = 0;
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
        Player.score += amount;
        scoreText.text = Player.score.ToString();
    }

}
