using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRecorderManager : MonoBehaviour
{
    // score display handler
    public Text timerText;
    public Text highestTimeText;

    // player died sound
    public AudioClip killSound;
    private AudioSource audioSource;

    public GameObject gameOver; // game over handler

    // timer variables
    private float timer = 0f;
    private float highestTime = 0f; // Record player highest survived time

    private const string highScoreKey = "HighScore"; // probably like a Dictionary method in C#
    bool playerDiedSound = false;

    void Start()
    {
        LoadHighScore();
        audioSource = GetComponent<AudioSource>();
        UpdateUI();
    }

    void Update()
    {
        if (IsPlayerAlive())
        {
            // Player is present, continue updating the timer
            timer += Time.deltaTime;
            gameOver.SetActive(false);
            UpdateUI();
        }
        else
        {
            // Player is killed, stop the timer and record highest time
            RecordHighestTime();
        }
    }
    bool IsPlayerAlive() // Check if player is alive or not
    {
        return GameObject.FindWithTag("Player") != null;
    }
    private void UpdateUI()
    {
        // Update UI Text elements
        timerText.text = "Time survived: " + timer.ToString("F2") + "s";
        highestTimeText.text = "Highest Survived Time: " + highestTime.ToString("F2") + "s";
    }
    public void RecordHighestTime()
    {


        if(timer > highestTime)
        {
            highestTime = timer;
            SaveHighScore();
            UpdateUI();
            GameOverScene();
            Cursor.visible = true; // renable mouse cursor after death
        }
        else
        {
            GameOverScene();
            Cursor.visible = true; // renable mouse cursor after death
        }
        timer = 0f;

        if (playerDiedSound == false) // to avoid sound being replayed
        {
            PlayerDied();
            playerDiedSound = true;
        }
        
    }
    public void GameOverScene() // display game over scene when player dies
    {
        gameOver.SetActive(true);
    }
    void SaveHighScore() // saves highscore even after player left the game
    {
        PlayerPrefs.SetFloat(highScoreKey, highestTime);
        PlayerPrefs.Save();
    }
    void LoadHighScore() // reload the highscore when player returns to play the game
    {
        highestTime = PlayerPrefs.GetFloat(highScoreKey, 0f);
    }
    void PlayerDied()
    {
        audioSource.PlayOneShot(killSound); // plays death sound after player killed
    }
}
