using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FallingHazard : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 speedMinMax;
    float speed;

    float visibleHeightThreshold;
    void Start()
    {
        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.GetDifficultyPercent());

        visibleHeightThreshold = -Camera.main.orthographicSize + transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < visibleHeightThreshold - 10)
        {
            Destroy(gameObject);
        }
    }
}
