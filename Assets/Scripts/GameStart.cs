﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class GameStart : MonoBehaviour
{
    public GameObject startScreen;
    bool gameStart = true;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameStart = false;
                Time.timeScale = 1;
            }
            startScreen.SetActive(true);
        } else {
            startScreen.SetActive(false);
        }
    }
}
