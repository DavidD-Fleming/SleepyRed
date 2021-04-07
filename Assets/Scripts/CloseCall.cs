using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCall : MonoBehaviour
{
    BlackPlayer bPlayer;
    WhitePlayer wPlayer;
    BuffSystem bSys;

    bool bothPlayersCloseCalled = false;

    void Start()
    {
        // find and identify players
        GameObject blackPlayer = GameObject.FindWithTag("BlackPlayer");
        GameObject whitePlayer = GameObject.FindWithTag("WhitePlayer");
        GameObject buffSystem = GameObject.Find("Spawner");
        bPlayer = blackPlayer.GetComponent<BlackPlayer>();
        wPlayer = whitePlayer.GetComponent<WhitePlayer>();
        bSys = buffSystem.GetComponent<BuffSystem>();
    }

    void OnTriggerExit2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "BlackPlayer" && bPlayer.amIDead == false && bPlayer.active == true)
        {
            bSys.CloseCallPoints("BlackPlayer");
            if (bothPlayersCloseCalled == false)
            {
                bothPlayersCloseCalled = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (triggerCollider.tag == "WhitePlayer" && wPlayer.amIDead == false && wPlayer.active == true)
        {
            bSys.CloseCallPoints("WhitePlayer");
            if (bothPlayersCloseCalled == false)
            {
                bothPlayersCloseCalled = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }

}
