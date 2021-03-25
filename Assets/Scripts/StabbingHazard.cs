using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabbingHazard : MonoBehaviour
{
    public Vector2 warningTimeMinMax;
    float warningTime;
    public float stayingTime = 2;
    public float stabSpeed = 25;
    public float returningSpeed = -15;

    public GameObject warningPrefab;
    Vector3 warningSpawnLocation;

    float visibleWidthThreshold;
    // Start is called before the first frame update
    void Start()
    {
        warningTime = Mathf.Lerp(warningTimeMinMax.y, warningTimeMinMax.x, Difficulty.GetDifficultyPercent());

        visibleWidthThreshold = Camera.main.aspect * Camera.main.orthographicSize;
        warningSpawnLocation = new Vector3(-visibleWidthThreshold + warningPrefab.transform.localScale.x * 3 / 4, transform.position.y, 0f);
        StartCoroutine(Stabbing());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Stabbing()
    {
        // warning
        GameObject temporaryWarning = (GameObject)Instantiate(warningPrefab, warningSpawnLocation, Quaternion.identity);
        yield return new WaitForSeconds(warningTime);
        Destroy(temporaryWarning);

        // stabbing
        while (transform.position.x < visibleWidthThreshold * 4 / 5 - transform.localScale.x * 2)
        {
            transform.Translate(Vector2.right * stabSpeed * Time.deltaTime);

            // probably inefficient method of slowly moving
            yield return new WaitForSeconds(0.0001f);
        }

        // pausing
        yield return new WaitForSeconds(stayingTime);

        // returning and destroying
        while (transform.position.x > -visibleWidthThreshold - transform.localScale.x * 2)
        {
            transform.Translate(Vector2.right * returningSpeed * Time.deltaTime);
            yield return new WaitForSeconds(0.0001f);
        }
        Destroy(gameObject);
    }
}
