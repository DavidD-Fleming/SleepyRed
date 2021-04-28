using System.Collections;
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
    public Text highScoreUI;
    bool gameOver;

    void Start()
    {
        FindObjectOfType<WhitePlayer>().OnWPlayerDeath += OnGameOver;
        FindObjectOfType<BlackPlayer>().OnBPlayerDeath += OnGameOver;
    }

    void OnGameOver()
    {
        if (FindObjectOfType<BlackPlayer>().amIDead && FindObjectOfType<WhitePlayer>().amIDead)
        {
            ZPlayerPrefs.SetFloat("Ichor", ZPlayerPrefs.GetFloat("Ichor") + (int)Time.timeSinceLevelLoad);
            secondsSurvivedUI.text = Time.timeSinceLevelLoad.ToString("N2");
            gameOverScreen.SetActive(true);
            if (Time.timeSinceLevelLoad >= ZPlayerPrefs.GetFloat("HighScore"))
            {
                highScoreUI.text = Time.timeSinceLevelLoad.ToString("N2");
                ZPlayerPrefs.SetFloat("HighScore", Time.timeSinceLevelLoad);
            } else
            {
                highScoreUI.text = ZPlayerPrefs.GetFloat("HighScore").ToString("N2");
            }
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
