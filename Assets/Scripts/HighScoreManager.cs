using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class HighScoreManager
{
    public static int HighScore = 0;

    public static void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
        PlayerPrefs.Save();
    }

    public static void LoadHighScore()
    {
        HighScore = PlayerPrefs.GetInt("HighScore");
    }
}
