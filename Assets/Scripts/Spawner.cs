﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fhPrefab;
    public Vector2 fhSpawnTimeMinMax;
    public Vector2 fhSpawnSizeMinMax;
    public float fhSpawnAngleMax;
    float fhNextSpawnTime;

    Vector2 screenHalfSizeWorldUnits;

    // Start is called before the first frame update
    void Start()
    {
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        if (fhNextSpawnTime < Time.time)
        {
            float fhSpawnTime = Mathf.Lerp(fhSpawnTimeMinMax.y, fhSpawnTimeMinMax.x, Difficulty.GetDifficultyPercent());
            fhNextSpawnTime = Time.time + fhSpawnTime;

            float fhSpawnAngle = Random.Range(-fhSpawnAngleMax, fhSpawnAngleMax);
            float fhSpawnSize = Random.Range(fhSpawnSizeMinMax.x, fhSpawnSizeMinMax.y);
            Vector3 fhSpawnPosition = new Vector3(Random.Range(-screenHalfSizeWorldUnits.x, screenHalfSizeWorldUnits.x), 2 * screenHalfSizeWorldUnits.y + fhSpawnSize / 2);
            GameObject newFallingHazard = (GameObject)Instantiate(fhPrefab, fhSpawnPosition, Quaternion.Euler(Vector3.forward * fhSpawnAngle));
            newFallingHazard.transform.localScale = Vector2.one * fhSpawnSize;
        }
    }
}