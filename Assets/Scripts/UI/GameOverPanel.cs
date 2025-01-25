using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _finalScore;
    [SerializeField] private TMP_Text _currentLevel;
    [SerializeField] private Game _game;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _goToMainMenuButton;
    [SerializeField] private HighestLevelManager _highestLevelManager;

    public void Update()
    {
        _finalScore.text = _game.Score.ToString();
        _currentLevel.text = "STAGE " + _highestLevelManager.CurrentLevel.ToString();
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartFirstLevel);
        _goToMainMenuButton.onClick.AddListener(OnGoToMainMenuButton);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(RestartFirstLevel);
        _goToMainMenuButton.onClick.RemoveListener(OnGoToMainMenuButton);
    }

    private void OnGoToMainMenuButton()
    {
        StartCoroutine(WaitForSoundMainMenuButton());
    }

    private void RestartFirstLevel()
    {
        StartCoroutine(WaitForSoundRestartButton());
    }

    private IEnumerator WaitForSoundRestartButton()
    {
        yield return new WaitForSeconds(0.5f);
        _game.ResartLevel1();
    }

    private IEnumerator WaitForSoundMainMenuButton()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }




}
