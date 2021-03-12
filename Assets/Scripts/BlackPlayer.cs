using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackPlayer : MonoBehaviour
{
    public float speed = 7;

    bool active = true;
    float screenHalfWidthInWorldUnits;
    float halfPlayerWidth;

    // Start is called before the first frame update
    void Start()
    {
        halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
    }

    // Update is called once per frame
    void Update()
    {
        // switches between active and inactive
        if (Input.GetKeyDown("z"))
        {
            if (active == true)
            {
                active = false;
                this.GetComponent<Renderer>().material.SetColor("_Color", new Color32(125, 25, 25, 255));
            } else
            {
                active = true;
                this.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            }
        }

        // allows horizontal movement if player is currently active
        if (active == true)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float velocity = inputX * speed;
            transform.Translate(Vector2.right * velocity * Time.deltaTime);
        }

        // teleports player to the other side of the screen if it moves out of camera
        if (transform.position.x < -screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
        }
        if (transform.position.x > screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
        }
    }
}
