using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackPlayer : MonoBehaviour
{
    // speed and collision
    public float speed = 7;
    public float jumpHeight = 3;
    float verticalSpeed;

    // dimensions
    float screenHalfWidthInWorldUnits;
    float screenHalfHeightInWorldUnits;
    float halfPlayerWidth;
    float halfPlayerHeight;

    // health, death
    public float health = 2;
    public Text healthUI;
    public event System.Action OnBPlayerDeath;
    public GameObject flashWhenDamaged;

    // state of players
    public bool active = true;
    bool isOtherPlayerDead = false;
    public bool amIDead = false;

    // Start is called before the first frame update
    void Start()
    {
        halfPlayerWidth = transform.localScale.x / 2f;
        halfPlayerHeight = transform.localScale.y / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfPlayerWidth;
        screenHalfHeightInWorldUnits = Camera.main.orthographicSize - halfPlayerHeight;

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

        // vertical reaction to gravity
        verticalSpeed -= 9.8f * Time.deltaTime;
        if (transform.position.y == -screenHalfHeightInWorldUnits) verticalSpeed = 0;

        // allows horizontal movement if player is currently active
        if (active == true)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float velocity = inputX * speed;
            transform.Translate(Vector2.right * velocity * Time.deltaTime);

            // enables a jump
            if (Input.GetKeyDown(KeyCode.Space) && transform.position.y == -screenHalfHeightInWorldUnits)
            {
                verticalSpeed = Mathf.Sqrt(2 * 9.8f * jumpHeight);
            }
        }

        // vertical movement
        transform.Translate(Vector2.up * verticalSpeed * Time.deltaTime);

        // teleports player to the other side of the screen if it moves out of camera and prevents falling off the world
        if (transform.position.x < -screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
        }
        if (transform.position.x > screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
        }
        if (transform.position.y < -screenHalfHeightInWorldUnits)
        {
            transform.position = new Vector2(transform.position.x, -screenHalfHeightInWorldUnits);
        }

        // update health bar
        healthUI.text = health.ToString("N0");
    }

    void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (active == true)
        {
            // FH Damage
            if (triggerCollider.tag == "FallingHazard")
            {
                // take damage
                health--;
                StartCoroutine(BlackTookDamage(0.5f));
                Destroy(triggerCollider.gameObject);
            }

            // SH Damage
            if (triggerCollider.tag == "StabbingHazard")
            {
                health--;
                StartCoroutine(BlackTookDamage(0.5f));
            }
        }
    }

    IEnumerator BlackTookDamage(float redTime)
    {
        flashWhenDamaged.SetActive(true);
        yield return new WaitForSeconds(redTime);
        flashWhenDamaged.SetActive(false);

        if (OnBPlayerDeath != null && health <= 0)
        {
            amIDead = true;
            OnBPlayerDeath();
            active = false;
            this.GetComponent<Renderer>().material.SetColor("_Color", new Color32(0, 0, 0, 0));
        }
    }

    void OtherPlayerIsDead()
    {
        Debug.Log("oh no white!");
        isOtherPlayerDead = true;
        if (amIDead == false)
        {
            this.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            active = true;
        }
    }
}
