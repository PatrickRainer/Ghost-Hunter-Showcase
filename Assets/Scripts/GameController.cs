﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameController gameController;
    public Camera mainCam;
    public GameObject pauseMenu;
    public GameObject inGameGui;
    public Spawner mySpawner;
    public enum SceneStatus { Playing, Paused }
    public SceneStatus sceneStatus;
    public GUIController myGui;
    // To temporary save the TimeScale
    private float tempTimeScale;


    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").
            GetComponent<GameController>();
        mainCam = Camera.main;
        pauseMenu = GameObject.Find("PauseMenu");
        inGameGui = GameObject.Find("InGameGui");
        mySpawner = GetComponent<Spawner>();
        myGui = GetComponent<GUIController>();
        LoadHighscore(); // TODO: Take these Variables out from the GUIController class in to to stats or a sub Class of this

        // TODO: Check which scene schould be loaded.
        StartLevel(0);
    }

    /// <summary>
    /// Start Scene
    /// </summary>
    /// <param name="level">Build Index of the scene</param>
    public void StartLevel(int level)
    {
        // TODO: Issue: Goes into a loop?! Because its in the start Method of this
        // ... script and we are still in that level?
        //SceneManager.LoadScene(level);
        mySpawner.StartSpawnInterval(mySpawner.startIntervalLength);
        // Set Scene-Status
        sceneStatus = SceneStatus.Playing;
    }

    /// <summary>
    /// Start Scene
    /// </summary>
    /// <param name="levelName">Scene Name</param>
    public void StartLevel(string levelName)
    {
        // TODO: Issue: Goes into a loop?!
        //SceneManager.LoadScene(levelName);
        mySpawner.StartSpawnInterval(mySpawner.startIntervalLength);
        // Set Scene-Status
        sceneStatus = SceneStatus.Playing;
    }

    /// <summary>
    /// Restarts the current level
    /// </summary>
    public void RestartLevel()
    {
        mySpawner.StopAllCoroutines();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = tempTimeScale;
        // Set Scene-Status
        sceneStatus = SceneStatus.Playing;
    }

    // TODO: Ends the current Level because Timer is out /// Still undone, current Work ///
    public void EndLevel()
    {
        // Pause the Game
        PauseGame();
        // Show PauseMenu
        myGui.ShowPauseMenu();
        // Disable the resume Button
        myGui.DisableResumeButton();

    }

    /// <summary>
    /// Pauses the Game, and goes into the Pause Menu
    /// </summary>
    public void PauseGame()
    {
        // TempSave the TimeScale
        tempTimeScale = Time.timeScale;
        // Set Time Scale to 0
        Time.timeScale = 0;
        // Show the GameMenu
        gameController.SendMessage("ShowPauseMenu");
        // Stopp the Touch and Mouse input reading
        gameController.SendMessage("StopInputChecking");
        // Set Scene-Status
        sceneStatus = SceneStatus.Paused;
        // Save Highscore if new is made
        SaveHighscore();
    }

    /// <summary>
    /// Resumes the Game usually from the Pause Menu
    /// </summary>
    public void ResumeGame()
    {
        // Hides the Highscore Menu
        myGui.HidePauseMenu();
        // Reloads the  temporary Saved TimeScale
        Time.timeScale = tempTimeScale;
        // Resumes the Touch and Mouse input reading
        gameController.SendMessage("ResumeInputChecking");
        sceneStatus = SceneStatus.Playing;
    }

    /// <summary>
    /// Quits the Game
    /// </summary>
    public void QuitGame()
    {
        // TODO: Add WebPlayer, Android and Iphone Quit. Example is here: https://answers.unity.com/questions/161858/startstop-playmode-from-editor-script.html
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    /// <summary>
    /// Saves the Highscore if there is a new one made
    /// </summary>
    public void SaveHighscore()
    {
        if (myGui.CurrentPoints > myGui.beginningHs)
        {
            // Then write a new highscore.
            PlayerPrefs.SetInt("Highscore", myGui.CurrentPoints);

            // Save PlayerPrefs
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// Loads the Highscore from PlayerPrefs into the GuiController class
    /// </summary>
    public void LoadHighscore()
    {
        // Is there already a Highscore?
        if (PlayerPrefs.HasKey("Highscore"))
        {
            myGui.beginningHs = PlayerPrefs.GetInt("Highscore");
        }
    }

    //TODO: SavePlayerInfo
    public void SavePlayerInfo()
    {

    }

    //TODO: LoadPlayerInfo
    public void LoadPlayerInfo()
    {

    }

    // TODO: NewPlayer
    public void NewPlayer()
    {

    }

    // TODO: DeletePlayer
    public void DeletePlayer()
    {

    }

    // TODO: ShowPlayerStats
    public void ShowPlayerStats()
    {

    }

}