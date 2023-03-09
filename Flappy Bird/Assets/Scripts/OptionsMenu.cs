using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider, _sfxSlider;
    [SerializeField] private GameObject optionsPanel;

    public void ShowPanel(){
        optionsPanel.SetActive(true);
    }

    public void MusicVolume(){
        AudioManager.instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume(){
        AudioManager.instance.SFXVolume(_sfxSlider.value);
    }

    public void HidePanel(){
        optionsPanel.SetActive(false);
    }
}
