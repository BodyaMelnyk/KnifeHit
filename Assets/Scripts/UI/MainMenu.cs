using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private float _loadDelay = 0.7f;
    [SerializeField] private TMP_Text _highScore;
    [SerializeField] private TMP_Text _highLevel;
    [SerializeField] private HighestLevelManager _highestLevelManager;

    private void Start()
    {
        HighScoreManager.LoadHighScore();
        _highScore.text = "SCORE " + HighScoreManager.HighScore;

        int maxLevel = PlayerPrefs.GetInt("HighestLevel", 1);
        _highLevel.text = "STAGE " + maxLevel.ToString();
    }

    public void StartLevel1()
    {
        StartCoroutine(DelayLoad());
    }

    private IEnumerator DelayLoad()
    {
        yield return new WaitForSeconds(_loadDelay);

        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
    }


}
