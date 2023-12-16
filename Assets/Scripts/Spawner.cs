using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float DifficultyIncreaseInterval = 30f; // After certain time passed, increase the spawn interval
    public float DifficultyRate = 0.1f; // the rate where it decreases time require to spawn enemies
    public GameObject[] Cats; // Insert enemy here

    // spawn location, or distance away from camera
    public float MinX = -19; 
    public float MaxX = 19;

    public float MinY = -10;
    public float MaxY = 10;

    public Transform Top;
    public Transform Bot;
    public Transform Left;
    public Transform Right;

    public float MinSpawnCooldown = 0.5f;
    public float MaxSpawnCooldown = 2f;

    protected float _spawnTimer = 0f;
    protected float _difficultyTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        if(_difficultyTimer > 0) // if timer hasnt reach to 0, continue the countdown
        {
            _difficultyTimer -= Time.deltaTime;
        }
        else // once it reaches 0, reduce the minimun and maximun spawn rate, and also reset the timer as well
        {
            // if player somehow survived until spawn cooldown became too small, run this perimeter to avoid game from spawning too fast and crashing it
            if (MinSpawnCooldown > 0.5) 
            {
                MaxSpawnCooldown -= DifficultyRate;
                _difficultyTimer = DifficultyIncreaseInterval;
            }
            else if (MinSpawnCooldown > 0.5 && MaxSpawnCooldown > 1)
            {
                return;
            }
            else
            {
                MinSpawnCooldown -= DifficultyRate;
                MaxSpawnCooldown -= DifficultyRate;
                _difficultyTimer = DifficultyIncreaseInterval;
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
