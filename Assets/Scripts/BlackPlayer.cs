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

    public float health = 2;
    public Text healthUI;
    public event System.Action OnBPlayerDeath;
    public GameObject flashWhenDamaged;

    bool isOtherPlayerDead = false;
    bool amIDead = false;

    // Start is called before the first frame update
    void Start()
    {
        halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;

        FindObjectOfType<WhitePlayer>().OnWPlayerDeath += OtherPlayerIsDead;
    }

    // Update is called once per frame
    void Update()
    {
        // switches between active and inactive if other player is still alive
        if (isOtherPlayerDead == false && amIDead == false)
        {
            if (Input.GetKeyDown("z"))
            {
                if (active == true)
                {
                    active = false;
                    // changes the player to a "sleepy" blue
                    this.GetComponent<Renderer>().material.SetColor("_Color", new Color32(125, 50, 50, 255));
                }
                else
                {
                    active = true;
                    this.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                }
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

    void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (active == true)
        {
            if (triggerCollider.tag == "FH")
            {
                // take damage
                health--;
                healthUI.text = health.ToString("N0");

                StartCoroutine(BlackTookDamage(0.5f));
                Destroy(triggerCollider.gameObject);
            }
        }
    }

    IEnumerator BlackTookDamage(float redTime)
    {
        flashWhenDamaged.SetActive(true);
        yield return new WaitForSeconds(redTime);
        flashWhenDamaged.SetActive(false);

        if (OnBPlayerDeath != null && health == 0)
        {
            OnBPlayerDeath();
            active = false;
            amIDead = true;
            this.GetComponent<Renderer>().material.SetColor("_Color", new Color32(0, 0, 0, 0));
        }
    }

    void OtherPlayerIsDead()
    {
        Debug.Log("oh no white!");
        isOtherPlayerDead = true;
        this.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        active = true;
    }
}
