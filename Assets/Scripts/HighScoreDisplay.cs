using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    public Text highestTimeDisplayText;

    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        // Check if the PlayerPrefs from GameRecorderManager is available
        if (PlayerPrefs.HasKey("HighScore"))
        {
            float highestTime = PlayerPrefs.GetFloat("HighScore", 0f);
            highestTimeDisplayText.text = "Player's Highest Surviving Time: " + highestTime.ToString("F2") + "s";
        }
        else
        {
            highestTimeDisplayText.text = "No Record Found";
        }
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();
        UpdateUI();
    }
}
