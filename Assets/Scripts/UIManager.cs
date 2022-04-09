using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static bool showMainPage = true;
    public static GameObject mainMenuPanel, toplistPanel, gameOverlayPanel, gameOverPanel, gamePausePanel;
    public static GameObject handRight, handRightRing;
    public static Transform toplistView;
    public static TextMeshProUGUI distanceText, scoreText, finalScoreText, framerateText;
    public static Text kinectInfoText;

    private int frameCount = 0;
    private float timeCount = 0;
    private const float refreshTime = 0.1f;

    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        mainMenuPanel = canvas.transform.Find("MainMenuPanel").gameObject;
        toplistPanel = canvas.transform.Find("ToplistPanel").gameObject;
        gameOverlayPanel = canvas.transform.Find("GameOverlayPanel").gameObject;
        gamePausePanel = canvas.transform.Find("GamePausePanel").gameObject;
        gameOverPanel = canvas.transform.Find("GameOverPanel").gameObject;
        kinectInfoText = mainMenuPanel.transform.Find("KinectInfoText").GetComponent<Text>();
        distanceText = gameOverlayPanel.transform.Find("DistanceText").GetComponent<TextMeshProUGUI>();
        scoreText = gameOverlayPanel.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        finalScoreText = gameOverPanel.transform.Find("FinalScoreText").GetComponent<TextMeshProUGUI>();
        framerateText = gameOverlayPanel.transform.Find("FramerateText").GetComponent<TextMeshProUGUI>();
        toplistView = toplistPanel.transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content");
        //foreach(var item in toplistPanel.transform.FindChild("Content"))
        //{
        //    print(item);
        //}

        handRight = canvas.transform.Find("HandRight").gameObject;
        handRightRing = canvas.transform.Find("HandRightRing").gameObject;

        mainMenuPanel.SetActive(showMainPage);
        gameOverlayPanel.SetActive(!showMainPage);
        Game.started = !showMainPage;
        Game.over = false;
        Game.paused = false;
    }

    void Update()
    {
        int distance = Player.GetDistance();
        distanceText.text = distance + " m";

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Game.PauseResumeGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Game.started)
                Game.BackToMainMenu();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Game.QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if(Game.over)
                Game.ReplayGame();
            else
                Game.StartGame();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Game.MainMenu();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Game.ToplistMenu();
        }

        if (!Game.paused && !Game.over)
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
}
