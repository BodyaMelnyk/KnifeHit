using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AdsPanel : MonoBehaviour
{
    private readonly string _animationName = "Show";

    [SerializeField] private Slider _adsSlider;
    [SerializeField] private TMP_Text _finalScore;
    [SerializeField] private TMP_Text _currentLevel;
    [SerializeField] private Game _game;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _closeButton;
    [SerializeField] private HighestLevelManager _highestLevelManager;
    [SerializeField] private CanvasGroup _canvasGroup;

    private float _adsDurationTime = 8f;
    private float _timeToEnableCloseButton = 2f;

    private void Start()
    {
        _gameOverPanel.SetActive(false);
        _closeButton.SetActive(false);
    }

    public void Update()
    {
        _finalScore.text = _game.Score.ToString();
        _currentLevel.text = "STAGE " + _highestLevelManager.CurrentLevel.ToString(); 
    }


    private void OnDisable()
    {
        StopCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        float elapsedTime = 0;
        _adsSlider.value = 1;

        while (elapsedTime < _adsDurationTime)
        {
            elapsedTime += Time.deltaTime;

            _adsSlider.value = Mathf.Lerp(1f, 0f, elapsedTime / _adsDurationTime);

            yield return null;
        }

        _adsSlider.value = 0f;
        gameObject.SetActive(false);
        _gameOverPanel.SetActive(true);
    }

    private IEnumerator ShowCloseButton()
    {
        yield return new WaitForSeconds(_timeToEnableCloseButton);

        _closeButton.SetActive(true);
    }

    public void ShowPanel()
    {
        _canvasGroup.alpha = 0f;
        _animator.SetTrigger(_animationName);
        StartCoroutine(UpdateTimer());
        StartCoroutine(ShowCloseButton());
        _finalScore.text = _game.Score.ToString();
    }
}
