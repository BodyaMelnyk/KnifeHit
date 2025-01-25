using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private AudioSource _soundEffect;
    [SerializeField] private TMP_Text _toggleButtonText;

    private bool _isSoundOn = true;

    private void Start()
    {
        _isSoundOn = PlayerPrefs.GetInt("SoundOn", 1) == 1;
        UpdateSoundState();
    }

    public void ToggleSound()
    {
        StartCoroutine(PlaySoundEffect());

        _isSoundOn = !_isSoundOn;

        PlayerPrefs.SetInt("SoundOn", _isSoundOn ? 1 : 0);
        PlayerPrefs.Save();

        UpdateSoundState();
    }


    private void UpdateSoundState()
    {
        AudioListener.pause = !_isSoundOn;
        _toggleButtonText.text = _isSoundOn ? "ON" : "OFF";
    }

    private IEnumerator PlaySoundEffect()
    {
        _soundEffect.Play();

        yield return new WaitForSeconds(0.2f);
    }


}
