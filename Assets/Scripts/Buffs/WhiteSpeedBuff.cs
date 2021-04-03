using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteSpeedBuff : MonoBehaviour
{
    public float speed;
    public float speedIncrease;

    float visibleHeightThreshold;
    WhitePlayer wPlayer;
    void Start()
    {
        visibleHeightThreshold = -Camera.main.orthographicSize + transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y > 0)
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
        if (triggerCollider.tag == "WhitePlayer")
        {
            wPlayer = triggerCollider.gameObject.GetComponent<WhitePlayer>();
            if (wPlayer.active == true)
            {
                wPlayer.speed += speedIncrease;
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
