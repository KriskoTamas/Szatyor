using UnityEngine;
using UnityEngine.SceneManagement;
using static UIManager;

public class Game : MonoBehaviour
{

    public static bool started, over, paused;
    public static bool kinectConnected = false;
    public static bool playernameInputValid;

    public static void StartGame()
    {
        if (!started)
        {
            Player.name = playernameInput.text;
            Player.highscore = Toplist.getHighScore(Player.name);
            highScoreText.text = Player.highscore.ToString();
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

        RecordList.Record record = Toplist.records.elements.Find(x => x.playerName == Player.name);
        int idx = Toplist.records.elements.IndexOf(record);
        int highscore = 0;

        if (record != null)
        {
            highscore = record.highScore;
            if(Player.score > highscore)
            {
                highscore = Player.score;
                Toplist.records.elements[idx].highScore = highscore;
                Toplist.WriteToJson();
            }
            //print("record idx: " + idx);
            //Toplist.records.elements
        }
        else
        {
            highscore = Player.score;
            RecordList.Record newrecord = new RecordList.Record();
            newrecord.playerName = Player.name;
            newrecord.highScore = highscore;
            Toplist.records.elements.Add(newrecord);
            Toplist.WriteToJson();
        }

        finalScoreText.text = "Pontszám: " + Player.score + "\nRekord: " + highscore;
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
