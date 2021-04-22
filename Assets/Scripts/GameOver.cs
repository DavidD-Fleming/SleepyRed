﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameOverScreen;
    public Text secondsSurvivedUI;
    bool gameOver;
    int bothPlayersDead = 0;

    void Start()
    {
        FindObjectOfType<WhitePlayer>().OnWPlayerDeath += OnGameOver;
        FindObjectOfType<BlackPlayer>().OnBPlayerDeath += OnGameOver;
    }

    void OnGameOver()
    {
        bothPlayersDead++;
        if (bothPlayersDead == 2)
        {
            gameOverScreen.SetActive(true);
            secondsSurvivedUI.text = Time.timeSinceLevelLoad.ToString("N2");
            gameOver = true;
            Time.timeScale = 0;
        }
    }

    public void StartGame()
    {
        if (gameOver)
        {
            SceneManager.LoadScene(0);
        }
    }
}
