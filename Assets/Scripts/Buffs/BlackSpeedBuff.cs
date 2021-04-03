using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSpeedBuff : MonoBehaviour
{
    public float speed;
    public float speedIncrease;

    float visibleHeightThreshold;
    BlackPlayer bPlayer;
    void Start()
    {
        visibleHeightThreshold = -Camera.main.orthographicSize + transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.y > visibleHeightThreshold - transform.localScale.y / 3)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else
        {
            StartCoroutine(Dissapear(3f));
        }
    }

    void OnTriggerStay2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "BlackPlayer")
        {
            bPlayer = triggerCollider.gameObject.GetComponent<BlackPlayer>();
            if (bPlayer.active == true)
            {
                bPlayer.speed += speedIncrease;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Dissapear(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // showbiz this up
        Destroy(gameObject);
    }
}
