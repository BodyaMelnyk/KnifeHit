using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private AudioSource _startLevelSound;
    [SerializeField] private TMP_Text _currentLevelIndex;
    [SerializeField] private GameObject _finishGamePanel;
    [SerializeField] private Canvas _panelParent;

    public event UnityAction<int> ScoreChanged;

    private static int _currentScore = 0;
    private int _currentSceneIndex;

    public int Score => _currentScore;

    private void Start()
    {
        _currentLevelIndex.text = "STAGE " + SceneManager.GetActiveScene().buildIndex.ToString();
        ScoreChanged?.Invoke(TotalScore.TotalScoreValue);
        _startLevelSound.Play();
    }

    public void AddScore(int value)
    {
        _currentScore += value;
        TotalScore.TotalScoreValue += value;
        ScoreChanged?.Invoke(TotalScore.TotalScoreValue);

        if(_currentScore > HighScoreManager.HighScore)
        {
            HighScoreManager.HighScore = _currentScore;
            HighScoreManager.SaveHighScore();
        }
    }

    public void GetTotalScore()
    {
        _currentScore = PlayerPrefs.GetInt("TotalScore", _currentScore );
    }

    public void ResetScore()
    {
        _currentScore = 0;
        TotalScore.TotalScoreValue = 0;
    }

    public void ResartLevel1()
    {
        ResetScore();
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowFinishScreen()
    {
         GameObject finishGamePanel = Instantiate(_finishGamePanel, _panelParent.transform);
         Button mainMenu = finishGamePanel.GetComponentInChildren<Button>();
         mainMenu.onClick.AddListener(() => LoadMainMenu());

    }



}
