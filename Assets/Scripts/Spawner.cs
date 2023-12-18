using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float DifficultyIncreaseInterval = 30f; // After certain time passed, increase the spawn interval
    public float DifficultyRate = 0.1f; // the rate where it decreases time require to spawn enemies
    public GameObject[] Cats; // Insert enemy here

    // spawn location, or distance away from camera
    public float MinX = -25; 
    public float MaxX = 25;

    public float MinY = -15;
    public float MaxY = 15;

    public Transform Top;
    public Transform Bot;
    public Transform Left;
    public Transform Right;

    public float MinSpawnCooldown = 0.5f;
    public float MaxSpawnCooldown = 2f;

    protected float _spawnTimer = 0f;
    protected float _difficultyTimer = 0f;

    void Update()
    {
        GameObject playerExist = GameObject.FindWithTag("Player"); // for spawner to check if player is killed
        if (playerExist == null) // if player is killed, basically "jamming the timer" to stop enemy from keep spawning
        {
            _spawnTimer = 1;
        }

        if (_difficultyTimer > 0) // if timer hasnt reach to 0, continue the countdown
        {
            _difficultyTimer -= Time.deltaTime;
        }
        else // once it reaches 0, reduce the minimun and maximun spawn rate, and also reset the timer as well
        {
            // if MinSpawnCooldown is smaller than 0.3, check MaxSpawnCooldown. IF BOTH CONDITIONS ARE TRUE, DIFFICULTY CAP HAS REACHED. Otherwise, lower MaxSpawnCooldown time.
            if (MinSpawnCooldown < 0.3)  
            {
                if (MaxSpawnCooldown < 0.9)
                {
                    _difficultyTimer = DifficultyIncreaseInterval;
                    return;
                }
                else
                {
                    MaxSpawnCooldown -= DifficultyRate;
                    _difficultyTimer = DifficultyIncreaseInterval; // Resets the difficulty timer when decrease cooldown time
                }
            }
            // if MaxSpawnCooldown is smaller than 0.9, check MinSpawnCooldown. IF BOTH CONDITIONS ARE TRUE, DIFFICULTY CAP HAS REACHED. Otherwise, lower MinSpawnCooldown time.
            else if (MaxSpawnCooldown < 0.9)
            {
                if (MinSpawnCooldown < 0.3)
                {
                    _difficultyTimer = DifficultyIncreaseInterval;
                    return;
                }
                else
                {
                    MinSpawnCooldown -= DifficultyRate;
                    _difficultyTimer = DifficultyIncreaseInterval; // Resets the difficulty timer when decrease cooldown time
                }
            }
            // if both conditions haven't met, lower both Min and Max SpawnCooldown time.
            else
            {
                MinSpawnCooldown -= DifficultyRate;
                MaxSpawnCooldown -= DifficultyRate;
                _difficultyTimer = DifficultyIncreaseInterval; // Resets the difficulty timer when decrease cooldown time
            }
        }

        if (_spawnTimer > 0) // if spawn timer hasnt reach 0, continue the countdown
        {
            _spawnTimer -= Time.deltaTime;
            return;
        }

        int randomSpawnPos = Random.Range(0, 3);

        int randomIndex = Random.Range(0, Cats.Length);

        switch (randomSpawnPos)
        {
            case (0):
                Instantiate(Cats[randomIndex], Top.position + new Vector3(Random.Range(MinX, MaxX), 0), transform.rotation);
                break;
            case (1):
                Instantiate(Cats[randomIndex], Bot.position + new Vector3(Random.Range(MinX, MaxX), 0), transform.rotation);
                break;
            case (2):
                Instantiate(Cats[randomIndex], Left.position + new Vector3(0, Random.Range(MinX, MaxX)), transform.rotation);
                break;
            case (3):
                Instantiate(Cats[randomIndex], Right.position + new Vector3(0, Random.Range(MinX, MaxX)), transform.rotation);
                break;
        }
        _spawnTimer = Random.Range(MinSpawnCooldown, MaxSpawnCooldown); // after finish spawning, asssign a number randomly between min and max cooldown to _spawnTimer

    }
}
