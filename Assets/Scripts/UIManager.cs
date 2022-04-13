using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static bool showMainPage = true;
    public static GameObject mainMenuPanel, toplistPanel, gameOverlayPanel, gameOverPanel, gamePausePanel;
    public static GameObject handRight, handRightRing;
    public static Button playButton;
    public static InputField playernameInput;
    public static Transform toplistView;
    public static TextMeshProUGUI distanceText, scoreText, highScoreText, finalScoreText, framerateText;
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
        playButton = mainMenuPanel.transform.Find("ButtonGrid").transform.Find("PlayButton").gameObject.GetComponent<Button>();
        kinectInfoText = mainMenuPanel.transform.Find("KinectInfoText").GetComponent<Text>();
        distanceText = gameOverlayPanel.transform.Find("DistanceText").GetComponent<TextMeshProUGUI>();
        scoreText = gameOverlayPanel.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        highScoreText = gameOverlayPanel.transform.Find("HighScoreText").GetComponent<TextMeshProUGUI>();
        finalScoreText = gameOverPanel.transform.Find("FinalScoreText").GetComponent<TextMeshProUGUI>();
        framerateText = gameOverlayPanel.transform.Find("FramerateText").GetComponent<TextMeshProUGUI>();
        toplistView = toplistPanel.transform.Find("Scroll View").transform.Find("Viewport").transform.Find("Content");
        playernameInput = mainMenuPanel.transform.Find("PlayernameInput").gameObject.GetComponent<InputField>();
        var ev = new InputField.OnChangeEvent();
        ev.AddListener(SubmitName);
        playernameInput.onValueChanged = ev;

        Player.highscore = Toplist.getHighScore(Player.name);
        highScoreText.text = Player.highscore.ToString();

        handRight = canvas.transform.Find("HandRight").gameObject;
        handRightRing = canvas.transform.Find("HandRightRing").gameObject;

        playernameInput.text = Player.name;
        mainMenuPanel.SetActive(showMainPage);
        gameOverlayPanel.SetActive(!showMainPage);
        Game.started = !showMainPage;
        Game.over = false;
        Game.paused = false;
    }

    private void SubmitName(string playerName)
    {
        if(playerName == "")
        {
            Game.playernameInputValid = false;
            playButton.interactable = false;
        }
        else
        {
            Game.playernameInputValid = true;
            playButton.interactable = true;
        }
    }

    void Update()
    {
        int distance = Player.GetDistance();
        distanceText.text = distance + " m";

        if (Game.started && !Game.paused && Player.score > Player.highscore)
            highScoreText.text = Player.score.ToString();

        if (!playernameInput.isFocused)
        {
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

            if (Input.GetKeyDown(KeyCode.S) && Game.playernameInputValid)
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
