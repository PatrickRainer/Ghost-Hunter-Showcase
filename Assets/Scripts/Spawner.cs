﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Spawner : MonoBehaviour
{
    // Text Object for the level-label
    public Text levelText;

    // Array for the ghosts
    public GameObject[] prefs;

    // Time of a animal-wave, or level
    public float startIntervalLength = 10;

    // CurrentInterval Length
    public float currentIntervallLength;

    // Current creating-delay
    public float currentSpawnDelay = 1.0F;

    // Playing width
    float width;

    // Playing height
    float height;

    // The LevelManager
    GameObject levelManager;

    // A Timer for GUItext
    private float remTime;
    public new Text guiText;

    private void Start()
    {
        // Game Controller assign
        levelManager = GameObject.FindGameObjectWithTag("LevelManager");

        // Display width
        width = Screen.width;
        // Display height
        height = Screen.height;

        currentIntervallLength = startIntervalLength;

        // Start the first Wave
        //StartSpawnInterval();     // Obsolete: Do it over the LevelController
    }

    private void Update()
    {
        //Timer
        if (remTime>=0)
        {
            // only count while remTime is not finished
            remTime -= Time.deltaTime;
            currentIntervallLength = remTime;
        }
        
    }

    private void Spawn()
    {
        levelText.text = ""; // TODO: Should be handled by gui controller
        Vector3 pos = new Vector3();
        pos.x = 0; 
        pos.y = UnityEngine.Random.Range(50, height - 50);
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        wp.z = 0;

        Instantiate(prefs[UnityEngine.Random.Range(0, prefs.Length)], wp, Quaternion.identity);
    }

    public void StartSpawnInterval(float intervallLength)
    {
        // if the delay is bigger than 0.2 sec
        if (currentSpawnDelay > 0.2F)
        {
            // then reduce the Delay for 0.05 sec
            currentSpawnDelay -= 0.05F;
        }

        // Show Level-Text
        levelText.text = "Level " + SceneManager.GetActiveScene().buildIndex.ToString();

        // Start in x seconds and create animal with delay
        InvokeRepeating("Spawn",1, currentSpawnDelay);

        // Ends the Level by the Timer
        LevelController.Instance.Invoke("EndLevel", startIntervalLength);
        // Set the Timer to the Invoke Time
        remTime = startIntervalLength;
    }

    public void StopSpawning()
    {
        // Cancel all Methods which has been started by invoke
        CancelInvoke();
    }

    /// <summary>
    /// Pauses the Game
    /// </summary>
    public void PauseSpawning()
    {
        // Cancel all Methods which has been started by invoke
        CancelInvoke();
    }

    [Obsolete("Method is not in use anymore, instead we use TimeScale to Pause the Game, located in the LevelController")]
    public void PauseMovingAnimals()
    {
        // Search for all Animals
        GameObject[] animals = GameObject.FindGameObjectsWithTag("Enemy");

        // Go trough every Animal and stop them
        foreach (GameObject current in animals)
        {
            current.SendMessage("StopMoving");
        }
    }

    
}
