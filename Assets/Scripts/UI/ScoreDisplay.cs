using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private Game _gameScore;
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        _gameScore.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _gameScore.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int value)
    {
        _score.text = value.ToString(); 
    }

    

}
