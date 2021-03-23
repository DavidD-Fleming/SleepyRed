using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhitePlayer : MonoBehaviour
{
    public float speed = 7;

    bool active = false;
    float screenHalfWidthInWorldUnits;
    float screenHalfHeightInWorldUnits;
    float halfPlayerWidth;
    float halfPlayerHeight;

    public float health = 2;
    public Text healthUI;
    public event System.Action OnWPlayerDeath;
    public GameObject flashWhenDamaged;

    bool isOtherPlayerDead = false;
    bool amIDead = false;

    // Start is called before the first frame update
    void Start()
    {
        halfPlayerWidth = transform.localScale.x / 2f;
        halfPlayerHeight = transform.localScale.y / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfPlayerWidth;
        screenHalfHeightInWorldUnits = Camera.main.orthographicSize - halfPlayerHeight;

        FindObjectOfType<BlackPlayer>().OnBPlayerDeath += OtherPlayerIsDead;
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
                    this.GetComponent<Renderer>().material.SetColor("_Color", new Color32(50, 50, 125, 255));
                }
                else
                {
                    active = true;
                    this.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                }
            }
        }

        // allows horizontal and vertical movement if player is currently active
        if (active == true)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");
            float velocityX = inputX * speed;
            float velocityY = inputY * speed;
            transform.Translate(Vector2.right * velocityX * Time.deltaTime);
            transform.Translate(Vector2.up * velocityY * Time.deltaTime);
        }

        // prevents movement out of camera
        if (transform.position.x < -screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
        }
        if (transform.position.x > screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
        }
        if (transform.position.y < halfPlayerHeight)
        {
            transform.position = new Vector2(transform.position.x, halfPlayerHeight);
        }
        if (transform.position.y > screenHalfHeightInWorldUnits)
        {
            transform.position = new Vector2(transform.position.x, screenHalfHeightInWorldUnits);
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

                StartCoroutine(WhiteTookDamage(0.5f));
                Destroy(triggerCollider.gameObject);
            }
        }
    }

    IEnumerator WhiteTookDamage(float redTime)
    {
        flashWhenDamaged.SetActive(true);
        yield return new WaitForSeconds(redTime);
        flashWhenDamaged.SetActive(false);

        if (OnWPlayerDeath != null && health <= 0)
        {
            OnWPlayerDeath();
            active = false;
            amIDead = true;
            this.GetComponent<Renderer>().material.SetColor("_Color", new Color32(0, 0, 0, 0));
        }
    }

    void OtherPlayerIsDead()
    {
        Debug.Log("oh no black!");
        isOtherPlayerDead = true;
        this.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        active = true;
    }
}
