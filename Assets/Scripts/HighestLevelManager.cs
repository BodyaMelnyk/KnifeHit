using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighestLevelManager : MonoBehaviour
{
    private int _currentLevel = 1;
    private int _highestLevel = 0;

    public int CurrentLevel => _currentLevel;

    private void Start()
    {
        _currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void SaveHighLevel()
    {
        _currentLevel = SceneManager.GetActiveScene().buildIndex;

        _highestLevel = PlayerPrefs.GetInt("HighestLevel", 0);

        if(_currentLevel > _highestLevel)
        {
            PlayerPrefs.SetInt("HighestLevel", _currentLevel);
            PlayerPrefs.Save();
        }
    }

    public int LoadHighLevel()
    {
        return PlayerPrefs.GetInt("HighestLevel", _highestLevel);
    }
}
