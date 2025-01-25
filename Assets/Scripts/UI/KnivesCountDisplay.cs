using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnivesCountDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _knivesCount;
    [SerializeField] private Trunk _trunk;

    private void OnEnable()
    {
        _trunk.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _trunk.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int value)
    {
        _knivesCount.text = value.ToString();
    }
}
