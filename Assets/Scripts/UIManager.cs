using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static bool gameOver;
    public Text scoreText;
    public GameObject gameOverPanel;
    public GameObject player;
    void Start()
    {
        gameOver = false;
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
    }
}
