using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static bool gameOver, gamePaused;
    public GameObject gameOverPanel, gamePausePanel;
    public GameObject player;
    public Text scoreText;
    void Start()
    {
        gameOver = false;
        gamePaused = false;
        Time.timeScale = 1;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        int distance = (int) player.transform.position.z;
        scoreText.text = distance + " m";
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeGame();
        }
    }

    public void PauseResumeGame()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            gamePausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            gamePausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
