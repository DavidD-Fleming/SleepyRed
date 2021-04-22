using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    // Players
    BlackPlayer bPlayer;
    WhitePlayer wPlayer;
    float blackBuffState = 0;
    float whiteBuffState = 0;
    public float whiteSteadyIncrement;
    public float blackSteadyIncrement;

    // Buffs
    public GameObject[] wpBuffs;
    public GameObject[] bpBuffs;
    public int closeCallPoints;

    // Dimensions
    Vector2 screenHalfSizeWorldUnits;

    void Start()
    {
        // find and identify players
        GameObject blackPlayer = GameObject.FindWithTag("BlackPlayer");
        GameObject whitePlayer = GameObject.FindWithTag("WhitePlayer");
        bPlayer = blackPlayer.GetComponent<BlackPlayer>();
        wPlayer = whitePlayer.GetComponent<WhitePlayer>();

        // set up dimensions
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }

    void Update()
    {
        // increments the other players buff progress
        if (bPlayer.active == true && wPlayer.amIDead == false && Time.timeScale == 1)
        {
            whiteBuffState += whiteSteadyIncrement;
        }
        if (wPlayer.active == true && bPlayer.amIDead == false && Time.timeScale == 1)
        {
            blackBuffState += blackSteadyIncrement;
        }

        // time to give a buff
        if (blackBuffState >= 100)
        {
            GiveABuff(0);
            blackBuffState -= 100;
        }
        if (whiteBuffState >= 100)
        {
            GiveABuff(1);
            whiteBuffState -= 100;
        }
    }

    void GiveABuff(int playerToGiveBuff)
    {
        // give black a buff
        if (playerToGiveBuff == 0)
        {
            int randomBuff = Random.Range(0, bpBuffs.Length);
            screenHalfSizeWorldUnits.x -= bpBuffs[randomBuff].transform.localScale.x;
            screenHalfSizeWorldUnits.y += bpBuffs[randomBuff].transform.localScale.y;
            Vector3 buffSpawnLocation = new Vector3(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), screenHalfSizeWorldUnits.y, 0f);
            GameObject newBlackBuff = (GameObject)Instantiate(bpBuffs[randomBuff], buffSpawnLocation, Quaternion.identity);
        }

        // give white a buff
        if (playerToGiveBuff == 1)
        {
            int randomBuff = Random.Range(0, wpBuffs.Length);
            screenHalfSizeWorldUnits.x -= wpBuffs[randomBuff].transform.localScale.x;
            screenHalfSizeWorldUnits.y += wpBuffs[randomBuff].transform.localScale.y;
            Vector3 buffSpawnLocation = new Vector3(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), screenHalfSizeWorldUnits.y, 0f);
            GameObject newWhiteBuff = (GameObject)Instantiate(wpBuffs[randomBuff], buffSpawnLocation, Quaternion.identity);
        }
    }

    public void CloseCallPoints(string player)
    {
        if (player == "BlackPlayer")
        {
            blackBuffState += closeCallPoints;
            Debug.Log("Wow close call black!");
        }
        if (player == "WhitePlayer")
        {
            whiteBuffState += closeCallPoints;
            Debug.Log("Wow close call white!");
        }
    }
}
